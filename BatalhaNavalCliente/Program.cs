using BatalhaNaval;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

internal class Program
{
    static NetworkStream stream = null;
    

    private static async Task Main(string[] args)
    {
        int port = 1020;
        string host = "localhost";

        Console.WriteLine("Cliente");

        ConnectServer(port, host);

        Board tabuleiro = new Board(await RecebeGrid());

        Board.Print(true);

        Console.WriteLine("Tabuleiro recebido com sucesso");


    }

    public static void ConnectServer(int port, string host)
    {
        var cliente = new TcpClient(host, port);
        stream = cliente.GetStream();

        Console.WriteLine("Cliente conectado com sucesso!");
    }

    public async static Task<string> Recieve()
    {
        var buffer = new byte[1024];
        int lidos = await stream.ReadAsync(buffer, 0, buffer.Length);
        return Encoding.UTF8.GetString(buffer, 0, lidos);
    }

    public async static Task<char[,]> RecebeGrid()
    {
        char[,] grid = new char[10, 10];

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {

                Console.WriteLine(await Recieve());
               //grid[i, j] = Convert.ToChar(await Recieve());
            }
        }

        return grid;
    }
}
