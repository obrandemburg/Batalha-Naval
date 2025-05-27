using System.Net.Sockets;
using System.Net;
using System.Text;
using BatalhaNaval;
internal class Program
{
    
    private static void Main(string[] args)
    {

        int port = 1020;

        Console.WriteLine("Servidor");

        StartServer(port);

        

    }

    public static void StartServer(int port)
    {
        var listener = new TcpListener(IPAddress.Any, port);
        listener.Start();
        Console.WriteLine($"Aguardando Player2 na porta {port}...");
        var client = listener.AcceptTcpClient();
        using var stream = client.GetStream();
        Console.WriteLine("Player2 conectado!");
    }

    public static void Send(string msg, NetworkStream stream)
    {
        var data = Encoding.ASCII.GetBytes(msg);
        stream.Write(data, 0, data.Length);
    }
    public static string Receive(NetworkStream stream)
    {
        var buf = new byte[32];
        int len = stream.Read(buf, 0, buf.Length);
        return Encoding.ASCII.GetString(buf, 0, len);
    }
  
}
