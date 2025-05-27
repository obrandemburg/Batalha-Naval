using System.Net.Sockets;
using System.Net;
using System.Text;
using BatalhaNaval;
internal class Program
{

    static NetworkStream stream = null;

    private async static Task Main(string[] args)
    {

        int port = 1020;

        Console.WriteLine("Servidor");

        StartServer(port);

        await SendMap();



    }

    public static void StartServer(int port)
    {
        var listener = new TcpListener(IPAddress.Any, port);
        listener.Start();
        Console.WriteLine($"Aguardando Player2 na porta {port}...");
        var client = listener.AcceptTcpClient();
        stream = client.GetStream();
        Console.WriteLine("Player2 conectado!");
    }

    public async static Task Send(string msg)
    {
        var data = Encoding.ASCII.GetBytes(msg);
        Console.WriteLine(data);
        await stream.WriteAsync(data, 0, data.Length);
    }

    public async static Task SendMap()
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                await Send(Board.trancreveGrid(i, j));
            }
        }
    }

}
