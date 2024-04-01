// See https://aka.ms/new-console-template for more information
using Garnet.client;
using Microsoft.Extensions.Logging;

Console.WriteLine("Hello, World From Garnet Client Console App");

var address = Environment.GetEnvironmentVariable("SERVER_GARNET_HOST") ?? "127.0.0.1";
int portNumber = int.TryParse(Environment.GetEnvironmentVariable("SERVER_GARNET_HOST_PORT")!, out portNumber) ? portNumber : 3278;

using ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
var client = new GarnetClient(address, portNumber, logger: loggerFactory.CreateLogger<Program>());
client.Connect();

var pingResponse = await client.PingAsync();
Console.WriteLine($"Ping: {pingResponse}");

var stringKey = "KEY_STRING";
var stringGet = await client.StringGetAsync(stringKey);
if (string.IsNullOrEmpty(stringGet))
{
    Console.WriteLine($"Could not find key of {stringKey}");
    var stringSetResponse = await client.StringSetAsync(stringKey, "This is a string");

    if (!stringSetResponse)
    {
        Console.WriteLine(stringSetResponse);
        throw new Exception($"Could not set key of {stringKey}");
    }

    Console.WriteLine($"Sucessfuly Set key of {stringKey}");
}

var expiryResult = await client.StringGetAsync("ExpiryKey");
if (string.IsNullOrEmpty(expiryResult))
{
    Console.WriteLine("The key ExpiryKey does not exist anymore");
}
else
{
    Console.WriteLine("Find key ExpiryKey, value is:");
    Console.WriteLine(expiryResult);
}

Console.WriteLine("Bye");
