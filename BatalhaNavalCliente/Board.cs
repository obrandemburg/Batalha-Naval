using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BatalhaNaval
{
    class Board
    {

        public static char[,] grid = new char[10, 10];
        public Board(char[,] Grid) {
            grid = Grid;
        }

        public static void setGrid(char[,] Grid)
        {
            grid = Grid;
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
        public static bool MarkMiss(int r, int c)
        {
            if(grid[r, c] != '*')
            {
                return true;
            }
            return false;
        }

        /* IsShip(int r, int c), MarkHit(int r, int c), MarkMiss(int r, int c), AreAllShipsSunk().*/
    }
}