using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;


public class Server
{
    static string path = "C:\\Users\\Gabi\\OneDrive\\Dokumenty\\studia\\4 semestr\\PZ2\\lab6";
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
        Console.WriteLine("Listening");

        Socket socketClient = socketServer.Accept();
        Console.WriteLine("Connection");

        while(true) {
            var sizeBufor = new byte[4];
            socketClient.Receive(sizeBufor, 4, SocketFlags.None);
            int bytes = BitConverter.ToInt32(sizeBufor);
            byte []bufor = new byte[bytes];
            int received = socketClient.Receive(bufor, SocketFlags.None);
            String messageClient = Encoding.UTF8.GetString(bufor, 0, received);
            Console.WriteLine("CLIENT: " + messageClient);

            string answer = "";
            // Regex rg = new Regex("in [a-zA-Z0-9_]+");
            string[] arr = messageClient.Split(null);

            if(messageClient == "!end") break;

            else if(messageClient == "list"){
                Console.WriteLine("Sending list of files");
                answer += "\n";
                foreach(string file in Directory.GetFiles(path, "*.*")){
                    answer += Path.GetFileName(file) + "\n";
                }
                foreach(string file in Directory.GetDirectories(path, "*.*")){
                    answer += Path.GetFileName(file) + "\n";
                }
            }
            
            // else if(rg.IsMatch(messageClient)){
            //     Console.WriteLine("działa");
            //     answer = "działa";

            // }
            else if(arr[0] == "in"){
                if(arr[1] == "..") {
                    path = Path.GetFullPath(Path.Combine(path, ".."));
                    Console.WriteLine("Changing directory to " + path);
                    Console.WriteLine("Sending list of files");
                    answer += "\n";
                    answer += path;
                    answer += "\n";
                        foreach(string file in Directory.GetFiles(path, "*.*")){
                            answer += Path.GetFileName(file) + "\n";
                        }
                        foreach(string file in Directory.GetDirectories(path, "*.*")){
                            answer += Path.GetFileName(file) + "\n";
                        }
                }
                foreach(string dir in Directory.GetDirectories(path, "*.*")){
                    if(Path.GetFileName(dir) == arr[1]){
                        Console.WriteLine("Changing directory to " + dir);
                        path = dir;
                        Console.WriteLine("Sending list of files");
                        answer += "\n";
                        answer += path;
                        answer += "\n";
                        foreach(string file in Directory.GetFiles(path, "*.*")){
                            answer += Path.GetFileName(file) + "\n";
                        }
                        foreach(string file in Directory.GetDirectories(path, "*.*")){
                            answer += Path.GetFileName(file) + "\n";
                        }
                        break;
                    }
                }
                if(answer == "") {
                    answer = "directory does not exist";
                }
            }

            else{
                answer = "unknown command";
            }

            var echoBytes = Encoding.UTF8.GetBytes(answer);
            byte[] sizeBytes = BitConverter.GetBytes(echoBytes.Length);
            socketClient.Send(sizeBytes, SocketFlags.None);
            socketClient.Send(echoBytes, 0);
        }

        try {
            socketServer.Shutdown(SocketShutdown.Both);
            socketServer.Close();
        }
        catch{}
    }
}
