// See https://aka.ms/new-console-template for more information
using StackExchange.Redis;

Console.WriteLine("Hello, World!");

var address = Environment.GetEnvironmentVariable("SERVER_GARNET_HOST") ?? "127.0.0.1";
int portNumber = int.TryParse(Environment.GetEnvironmentVariable("SERVER_GARNET_HOST_PORT")!, out portNumber) ? portNumber : 3278;
var channel = "sample-channel";

using var redis = await ConnectionMultiplexer.ConnectAsync($"{address}:{portNumber},connectTimeout=30,syncTimeout=30,resolveDns=true");
var subscriber = redis.GetSubscriber();

subscriber.Subscribe(RedisChannel.Literal(channel), (channel, value) =>
    Console.WriteLine($"Message received from channel ({channel}), content of message is: \n\t{value}"));

Thread.Sleep(Timeout.Infinite);