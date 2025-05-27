using System.Net;
using System.Net.Sockets;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {

        int port = 1020;
        string host = "localhost";


        Console.WriteLine("Cliente");

        ConnectServer(port, host);

    }

    public static void ConnectServer(int port, string host)
    {
        var cliente = new TcpClient(host, port);
        using NetworkStream stream = cliente.GetStream();

        Console.WriteLine("Cliente conectado com sucesso!");

    }
}