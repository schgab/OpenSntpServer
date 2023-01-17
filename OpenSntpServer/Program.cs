using OpenSntpServer.NtpBase;
using System;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace OpenSntpServer;

class Program
{
    static async Task Main(string[] args)
    {
        await RunSntpServer();        
    }

    static async Task RunSntpServer()
    {
        Console.WriteLine("Server running: Press Ctrl+c to shutdown");
        UdpClient udpServer = new UdpClient(123);
        while (true)
        {
            var received = await udpServer.ReceiveAsync();
            var ntpServerPacket = new NtpServerPacket(received.Buffer); //Create the server packet
            await udpServer.SendAsync(ntpServerPacket.Bytes, NtpServerPacket.RESPONSE_SIZE, received.RemoteEndPoint); //Respond to the client request
            Console.WriteLine($"Responded to {received.RemoteEndPoint.Address} with {ntpServerPacket.UTCTime.ToLocalTime()}"); //Log the transmitted time 
        }
    }
}

