
// See https://aka.ms/new-console-template for more information
using System.Net;
using System.Net.Sockets;
using Garnet;
using Garnet.common;
using Garnet.server;
using Microsoft.Extensions.Logging;

Console.WriteLine("Hello, World! From Garnet Server");

using ILoggerFactory loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
var useArgs = Environment.GetEnvironmentVariable("SETUP_GARNET_SERVER_WITH_CLI_ARGS") == "true";

var logger = loggerFactory.CreateLogger<Program>();
GarnetServer server;

if (useArgs)
{
    Console.WriteLine("Setup Garnet Server with args");
    server = new GarnetServer(args, loggerFactory);
}
else
{
    Console.WriteLine("Setup Garnet server with Options");
var options = new GarnetServerOptions()
{
    // This is needed since when we using the GarnetServerOptions directly, 
    // we need to specify the Address otherwise when ran it on docker it will always have the addres 127.0.0.1 (localhost)
    Address = GetContainerIpAddress(),
    IndexSize = "128m",
    LogLevel = LogLevel.Debug,
    Port = 6380,
    logger = logger
};
    server = new GarnetServer(options, loggerFactory);
}

// Start the server
server.Start();

Thread.Sleep(Timeout.Infinite);


static string GetContainerIpAddress()
{
    var host = Dns.GetHostEntry(Dns.GetHostName());
    foreach (var ip in host.AddressList.Where(x => x.AddressFamily == AddressFamily.InterNetwork))
    {
        Console.WriteLine($"Container IP Address: {ip}");
        return ip.ToString();
    }

    return string.Empty;
}