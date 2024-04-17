using System.Net;
using System.Net.Sockets;
using System.Text;


public class Client
{
    public static void Main()
    {
        IPHostEntry host = Dns.GetHostEntry("localhost");
        IPAddress ipAddress = host.AddressList[0];
        IPEndPoint localEndPoint = new IPEndPoint(ipAddress, 11000);

        Socket socket = new(
            localEndPoint.AddressFamily, 
            SocketType.Stream, 
            ProtocolType.Tcp);
        socket.Connect(localEndPoint);
        Console.WriteLine("Połączenie");

        Console.WriteLine("Podaj wiadomość dla serwera:");
        string wiadomosc = Console.ReadLine();
        byte[] wiadomoscBajty = Encoding.UTF8.GetBytes(wiadomosc);
        socket.Send(wiadomoscBajty, SocketFlags.None);

        var bufor = new byte[1_024];
        int liczbaBajtów = socket.Receive(bufor, SocketFlags.None);
        String odpowiedzSerwera = Encoding.UTF8.GetString(bufor, 0, liczbaBajtów);
        Console.WriteLine("Serwer: " + odpowiedzSerwera);

        try {
            socket.Shutdown(SocketShutdown.Both);
            socket.Close();
        }
        catch{}
    }
}