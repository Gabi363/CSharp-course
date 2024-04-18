using System.Net;
using System.Net.Sockets;
using System.Text;


public class Server
{
    public static void Main()
    {
        IPHostEntry host = Dns.GetHostEntry("localhost");
        IPAddress ipAddress = host.AddressList[0];
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

        Socket socketServer = new(
            localEndPoint.AddressFamily,
            SocketType.Stream,
            ProtocolType.Tcp);
        socketServer.Bind(localEndPoint);
        socketServer.Listen(100);
        Console.WriteLine("Nasłuchiwanie");

        Socket socketClient = socketServer.Accept();
        Console.WriteLine("Połączenie");

        var sizeBufor = new byte[4];
        socketClient.Receive(sizeBufor, 4, SocketFlags.None);
        int bytes = BitConverter.ToInt32(sizeBufor);
        byte []bufor = new byte[bytes];
        int received = socketClient.Receive(bufor, SocketFlags.None);
        String messageClient = Encoding.UTF8.GetString(bufor, 0, received);
        Console.WriteLine("Klient: " + messageClient);

        string answer = "odczytałem: " + messageClient;
        var echoBytes = Encoding.UTF8.GetBytes(answer);
        byte[] sizeBytes = BitConverter.GetBytes(echoBytes.Length);
        socketClient.Send(sizeBytes, SocketFlags.None);
        socketClient.Send(echoBytes, 0);

        try {
            socketServer.Shutdown(SocketShutdown.Both);
            socketServer.Close();
        }
        catch{}
    }
}
