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
        try
        {
            int port = 1020;
            string host = "localhost";
            bool jogoRolando = true;
            Console.WriteLine("Cliente");

            ConnectServer(port, host);

            Board tabuleiro = new Board(await RecebeGrid());

            Board.Print(false);

            while (jogoRolando)
            {
                Console.WriteLine("Digite as coordenadas para atacar: ");
                Console.WriteLine("primeiro escolha a linha horizontal (0 - 9)");
                int x = int.Parse(Console.ReadLine());

                Console.WriteLine("Agora escolha a coluna (0 - 9)");
                int y = int.Parse(Console.ReadLine());

                await Enviar(x, y);
                string resultado = await ReceberMensagemComQuebraDeLinha();

                Console.WriteLine(resultado);
                if (resultado.Contains("Todos os navios foram afundados!"))
                {
                    jogoRolando = false;
                }
                Board.setGrid(await RecebeGrid());

                Board.Print(false);

            }



        }
        catch (Exception ex)
        {
            Console.WriteLine("Erro fatal no cliente: " + ex.Message);
            Console.WriteLine("StackTrace: " + ex.StackTrace);
            Console.ReadLine(); // impede que o terminal feche imediatamente
        }
        Console.WriteLine("Pressione ENTER para sair...");
        Console.ReadLine();

    }


    public static void ConnectServer(int port, string host)
    {
        var cliente = new TcpClient(host, port);
        stream = cliente.GetStream();

        Console.WriteLine("Cliente conectado com sucesso!");
    }

    public async static Task Enviar(int x, int y)
    {
        string msg = $"{x},{y}";

        var data = Encoding.ASCII.GetBytes(msg);
        await stream.WriteAsync(data, 0, data.Length);
    }

    public async static Task<string> Recieve()
    {
        var buffer = new byte[1];

        int lidos = await stream.ReadAsync(buffer, 0, 1);

        if (lidos == 0)
            throw new Exception("Conexão encerrada pelo servidor.");

        return Encoding.ASCII.GetString(buffer, 0, lidos);
    }

    public async static Task<string> ReceberMensagemComQuebraDeLinha()
    {
        List<byte> dados = new List<byte>();
        var buffer = new byte[1];

        while (true)
        {
            int lidos = await stream.ReadAsync(buffer, 0, 1);
            if (lidos == 0)
                throw new Exception("Conexão encerrada.");

            if (buffer[0] == '\n') 
                break;

            dados.Add(buffer[0]);
        }

        return Encoding.ASCII.GetString(dados.ToArray());
    }


    public async static Task<char[,]> RecebeGrid()
    {
        char[,] grid = new char[10, 10];

        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < 10; j++)
            {
                string receivedData = await Recieve();


                grid[i, j] = receivedData[0];
            }
        }

        return grid;
    }
}
