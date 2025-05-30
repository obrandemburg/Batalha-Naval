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
        bool jogoRolando = true;
        Console.WriteLine("Servidor, iniciando mapa");
        Board.PlaceShipsRandomly(10);

        StartServer(port);
        await SendMap();

        while (jogoRolando)
        {


            string ataque = await RecebeAtaques();
            string resultado = await Board.RecebeAtaque(ataque);

            if (resultado.Contains("Todos os navios foram afundados!"))
            {
                jogoRolando = false;
                await Send("Fim de jogo! Todos os navios foram afundados!\n");
            }

            await Send(resultado + "\n");

            await SendMap();


        }

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

    public async static Task<string> RecebeAtaques()
    {
        var buffer = new byte[3];

        int bytesLidos = 0;

        while (bytesLidos < buffer.Length)
        {
            bytesLidos += await stream.ReadAsync(buffer, bytesLidos, buffer.Length - bytesLidos);
        }

        if (bytesLidos == 0)
        {
            throw new Exception("Conexão encerrada pelo servidor.");
        }

        string mensagem = Encoding.ASCII.GetString(buffer, 0, bytesLidos);

        return mensagem;
    }

}
