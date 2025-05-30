using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatalhaNaval
{
    class Board
    {
        public Board() { }

        public static char[,] grid = new char[10, 10];

        public static string trancreveGrid(int x, int y)
        {
            return grid[x, y].ToString();
        }

        public static void Print(bool showShips)
        {
            Console.Write("   ");
            for (int c = 0; c < 10; c++) Console.Write($"{c} ");
            Console.WriteLine();
            for (int r = 0; r < 10; r++)
            {
                Console.Write($"{(char)('A' + r)}  ");
                for (int c = 0; c < 10; c++)
                {
                    char cell = grid[r, c];
                    Console.Write(!showShips && cell == '*' ? "~ " : $"{cell} ");
                }
                Console.WriteLine();
            }
        }

        public static void PlaceShipsRandomly(int n)
        {
            var rnd = new Random();
            int placed = 0;
            while (placed < n)
            {
                int r = rnd.Next(10), c = rnd.Next(10);
                if (grid[r, c] == '\0') // '\0' represents an uninitialized cell in a char array  
                {
                    grid[r, c] = '*';
                    placed++;
                }
            }
        }
        public async static Task<string> RecebeAtaque(string ataque)
        {
            string[] vetor = ataque.Split(',');

            int x = int.Parse(vetor[0]);
            int y = int.Parse(vetor[1]);

            if (IsShip(x, y))
            {
                if (AreAllShipsSunk()) { 
                    return "Todos os navios foram afundados!";
                }

                await MarkHit(x, y);
                return $"Acertou o navio na posição {x}, {y}!";
            }
            else
            {
                await MarkMiss(x, y);
                return $"Errou o ataque na posição {x}, {y}!";
            }

        }

        public async static Task<bool> MarkHit(int r, int c)
        {
            if (grid[r, c] == '*')
            {
                grid[r, c] = 'X'; // Marcar como atingido
                return true;
            }
            return false;
        }

        public async static Task<bool> MarkMiss(int r, int c)
        {
            if (grid[r, c] != '*')
            {
                grid[r, c] = 'O'; // Marcar como erro
                return true;
            }
            return false;
        }

        public static bool IsShip(int r, int c)
        {
            if (grid[r, c] == '*')
            {
                return true;
            }
            return false;
        }

        public static bool AreAllShipsSunk()
        {
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    if (grid[i, j] == '*')
                    {
                        return false;
                    }
                }
            }
            return true;

        }

        /* IsShip(int r, int c), MarkHit(int r, int c), MarkMiss(int r, int c), AreAllShipsSunk().*/
    }
}