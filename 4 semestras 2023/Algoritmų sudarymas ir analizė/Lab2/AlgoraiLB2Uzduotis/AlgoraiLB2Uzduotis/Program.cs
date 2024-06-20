
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    /// <summary>
    /// Board class
    /// </summary>
    public class Board
    {
        List<int> Path = new List<int>();
        List<int> PathR = new List<int>();
        int n = 0;
        int m = 0;
        int[,] board;
        int[,] pboard;
        int MaxD = 0;
        int MaxR = 0;
        public int CountT = 0;

        /// <summary>
        /// A constructor that creates an empty nxm matrix, that holds all 0 and another matrix with random point values
        /// </summary>
        /// <param name="x"> size of the matrix in length</param>
        /// <param name="y"> size of the matrix in hight </param>
        public Board(int x, int y)
        {
            n = x;
            m = y;
            board = new int[n,m];
            pboard = new int[n, m];
            
            var rand = new Random();

            for (int i = 0; i < n; i++)
            {

                for (int j = 0; j < m; j++)
                {
                    board[i, j] = rand.Next(0, 9);
                    pboard[i, j] = 0;


                }
            }
        }
        /// <summary>
        /// Calculates the answer dynamically
        /// </summary>
        public void CalculateD()
        {
            CalculateMax();
            FindPath();
            PrintBoardD();

        }
        /// <summary>
        /// Calculates the answer c
        /// </summary>
        public void CalculateR()
        {
            PathR.Add(board[0, 0]);
            MaxValue(board[0, 0], 0, 0, PathR);
            PrintBoardR();

        }

        /// <summary>
        /// A function used to print out the answer dynamically
        /// </summary>
        public void PrintBoardD()
        {
            Console.WriteLine("");
            Console.WriteLine("\nStarting Board: ");
            for(int i = n - 1; i >= 0; i --)
            {
                
                for (int j = 0; j < m; j++)
                {
                    Console.Write(board[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("\nMax points on each block: ");
            for (int i = n - 1; i >= 0; i--)
            {

                for (int j = 0; j < m; j++)
                {
                    Console.Write(pboard[i, j] + " ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("\nMax points: " + MaxD);
            Console.WriteLine("Path :");
            foreach(int i in Path)
            {
                Console.Write(i + " ");
            }
        }
        /// <summary>
        /// A function used to print out the answer recursively
        /// </summary>
        public void PrintBoardR()
        {
            Console.WriteLine("");
            Console.WriteLine("\nStarting Board: ");
            for (int i = n - 1; i >= 0; i--)
            {

                for (int j = 0; j < m; j++)
                {
                    Console.Write(board[i, j] + " ");
                }
                Console.WriteLine();
            } 
            Console.WriteLine("\nMax points: " + MaxR);
            Console.WriteLine("\nPath :");
            foreach (int i in Path)
            {
                Console.Write(i + " ");
            }
        }
        /// <summary>
        /// Finds all max points values in each block of the matrix board
        /// </summary>
        private void CalculateMax()
        {

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < m; j++)
                {

                    int maxPoints = board[i, j];

                    if (i > 0)
                    {
                        
                        maxPoints = Math.Max(maxPoints, pboard[i - 1, j] + board[i, j]);
                    }


                    if (j > 0)
                    {
                        maxPoints = Math.Max(maxPoints, pboard[i, j - 1] + board[i, j]);
                    }


                    if (i > 0 && j > 0)
                    {
                        maxPoints = Math.Max(maxPoints, pboard[i - 1, j - 1] + board[i, j] * 2);
                    }

                    pboard[i, j] = maxPoints;

                }
            }
            MaxD = pboard[n - 1, m - 1];
        }

        /// <summary>
        /// Finds the path that the player has to take in order to get the max amount of points
        /// </summary>
        private void FindPath()
        {
            int x = n - 1;
            int y = m - 1;
            Path.Add(board[x, y]);
            while (x > 0 || y > 0)
            {
                if (x == 0)
                {
                    y--;
                }
                else if (y == 0)
                {
                    x--;
                }
                else if (pboard[x, y] - board[x, y] == pboard[x - 1, y])
                {
                    x--;
                }
                else if (pboard[x, y] - board[x, y] == pboard[x, y - 1])
                {
                    y--;
                }
                else if (pboard[x, y] - board[x, y] * 2 == pboard[x - 1, y - 1])
                {
                    x--;
                    y--;
                }
                Path.Add(board[x, y]);
            }
            Path.Reverse();
        }

        /// <summary>
        /// Recursively finds the path that leads to the most points
        /// </summary>
        /// <param name="max"> point values that have accumulated up to this point </param>
        /// <param name="x"> current x position </param>
        /// <param name="y"> current y position </param>
        /// <param name="path"> the path that gets generated up to the certain point in the recursion </param>
        private void MaxValue(int max, int x, int y, List<int> path)
        {
            CountT++;
            if (MaxR < max)
            {
                MaxR = max;
                PathR = path;
            }
            if (n - 1 > x)
            {
                List<int> p = Fill(path);
                p.Add(board[x + 1, y]);
                MaxValue(max + board[x + 1, y], x + 1, y, p);
            }
            if (m - 1 > y)
            {
                List<int> p = Fill(path);
                p.Add(board[x, y + 1]);
                MaxValue(max + board[x, y + 1], x, y + 1, p);
            }
            if (n - 1 > x && m - 1 > y)
            {
                List<int> p = Fill(path);
                p.Add(board[x + 1, y + 1]);
                MaxValue(max + board[x + 1, y + 1] * 2, x + 1, y + 1, p);
            }
        }



        
        /// <summary>
        /// Fills a list with one more position of the path that the player has to take
        /// </summary>
        /// <param name="list"> positions up to this point </param>
        /// <returns> position list with the new position in the end of it </returns>
        private List<int> Fill(List<int> list)
        {
            List<int> path = new List<int>();
            foreach (int i in list)
            {
                path.Add(i);
            }
            return path;
        }


    }
    

    class Program
    {
        static void Main(string[] args)
        {           
            const int n = 7;
            const int m = 7;
            Board Dyn = new Board(n, m);
            var watch = new System.Diagnostics.Stopwatch();
            watch.Start();
            Dyn.CalculateD();
            watch.Stop();
            Console.WriteLine($"\nDinaminio sprendimo skaičiavimų laikas: {watch.ElapsedMilliseconds} ms");
            watch.Reset();

            watch.Start();
            Dyn.CalculateR();
            watch.Stop();
            Console.WriteLine($"\nRekurentinio sprendimo skaičiavimų laikas: {watch.ElapsedMilliseconds} ms");
            Console.WriteLine($"\nRekurentinio sprendimo rekurentinių ciklų skaičius: {Dyn.CountT}");

        }
    }

    


}