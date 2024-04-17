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

        Socket socketSerwera = new(
            localEndPoint.AddressFamily,
            SocketType.Stream,
            ProtocolType.Tcp);
        socketSerwera.Bind(localEndPoint);
        socketSerwera.Listen(100);
        Console.WriteLine("Nasłuchiwanie");

        Socket socketKlienta = socketSerwera.Accept();
        Console.WriteLine("Połączenie");

        byte []bufor = new byte[1_024];
        int received = socketKlienta.Receive(bufor, SocketFlags.None);
        String wiadomoscKlienta = Encoding.UTF8.GetString(bufor, 0, received);
        Console.WriteLine("Klient: " + wiadomoscKlienta);

        string odpowiedz = "odczytałem: " + wiadomoscKlienta;
        var echoBytes = Encoding.UTF8.GetBytes(odpowiedz);
        socketKlienta.Send(echoBytes, 0);

        try {
            socketSerwera.Shutdown(SocketShutdown.Both);
            socketSerwera.Close();
        }
        catch{}
    }
}
