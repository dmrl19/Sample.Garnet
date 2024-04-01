// See https://aka.ms/new-console-template for more information
using StackExchange.Redis;

Console.WriteLine("Hello, World From Redis Multiplexer Console App!1");

var address = Environment.GetEnvironmentVariable("SERVER_GARNET_HOST") ?? "127.0.0.1";
int portNumber = int.TryParse(Environment.GetEnvironmentVariable("SERVER_GARNET_HOST_PORT")!, out portNumber) ? portNumber : 3278;
var channel = "sample-channel";

using var redis = await ConnectionMultiplexer.ConnectAsync($"{address}:{portNumber},connectTimeout=30,syncTimeout=30,resolveDns=true");
var db = redis.GetDatabase(0);
await db.PingAsync();

var stringKey = "KEY_STRING";

var getString = await db.StringGetAsync(stringKey);
Console.WriteLine(getString);
db.StringSet("ExpiryKey", "This element will be deleted in some seconds", TimeSpan.FromSeconds(3));

var subscriber = redis.GetSubscriber();
while ((await db.StringGetAsync("ExpiryKey")).HasValue)
{
    Console.WriteLine("Key ExpiryKey still exist in the cache");
    await subscriber.PublishAsync(RedisChannel.Literal(channel), "This is a message to be read by a subscriber");
    await Task.Delay(1000);
}

Console.WriteLine("Key ExpiryKey expired");
Console.WriteLine("Bye");
