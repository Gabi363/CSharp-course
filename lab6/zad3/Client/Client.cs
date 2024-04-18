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
        Console.WriteLine("Connection");


        while(true){
            Console.WriteLine("Enter message for server:");
            string message = Console.ReadLine();
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            byte[] sizeBytes = BitConverter.GetBytes(messageBytes.Length);
            socket.Send(sizeBytes, SocketFlags.None);
            socket.Send(messageBytes, SocketFlags.None);
            if(message == "!end") break;

            var sizeBufor = new byte[4];
            socket.Receive(sizeBufor, 4, SocketFlags.None);
            int bytes = BitConverter.ToInt32(sizeBufor);
            var bufor = new byte[bytes];
            int amountBytes = socket.Receive(bufor, SocketFlags.None);
            String messageServer = Encoding.UTF8.GetString(bufor, 0, amountBytes);
            Console.WriteLine("SERVER: " + messageServer);
        

        }
        try {
                socket.Shutdown(SocketShutdown.Both);
                socket.Close();
            }
            catch{}
    }
}