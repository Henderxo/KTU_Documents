using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Program
{
    class Program
    {
        static void Main()
        {
            int W = 10; // Maximum power
            int n = 3; // Number of items
            int[] m = { 3, 3, 3, 3, 3 }; // Number of items of each name
            int[] r = { 5, 4, 3, 2, 1 }; // Profit per item
            int[] w = { 5, 4, 3, 2 ,1 }; // Power per item

            Calculator Results = new Calculator(n, W);
            var watch = new System.Diagnostics.Stopwatch();

            watch.Start();
            Results.CalculateRec(W, m, r, w, n);
            watch.Stop();
            Console.WriteLine($"\nRekursinio sprendimo skaičiavimų laikas: {watch.ElapsedMilliseconds} ms");
            Console.WriteLine($"\nRekurentinio sprendimo rekurentinių ciklų skaičius: {Results.Counter}");
            watch.Reset();

            

            watch.Start();
            Results.CalculateDin(W, m, r, w, n);
            watch.Stop();
            Console.WriteLine($"\nDinaminio sprendimo skaičiavimų laikas: {watch.ElapsedMilliseconds} ms");
            watch.Reset();
            Console.WriteLine("");

            Results.TotalItemCalculator(n, m);
            Console.WriteLine("Maksimalus daiktų skaičius: " + Results.TotalItemCounter);
            Console.WriteLine("");
            Results.Printer();

        }
        
    }

    /// <summary>
    /// Class used to calculate all the answers
    /// </summary>
    class Calculator
    {
        /// <summary>
        /// Maximum profit using recursive solution
        /// </summary>
        int MaxProfitRec;
        /// <summary>
        /// Maximum profit using recursive dynamic solution
        /// </summary>
        int MaxProfitDyn;
        /// <summary>
        /// Profit made on each item
        /// </summary>
        int[,] Profit;
        /// <summary>
        /// Counts the number of recursions made
        /// </summary>
        public int Counter;

        public int TotalItemCounter;

        /// <summary>
        /// Constructor class for Calculator
        /// </summary>
        /// <param name="n"> Number of items </param>
        /// <param name="W"> Maximum weight </param>
        public Calculator(int n, int W)
        {
             this.MaxProfitRec = 0;
             this.MaxProfitDyn = 0;
             this.Profit = new int[n + 1, W + 1];
             this.Counter = 0;
            this.TotalItemCounter = 0;
        }

        /// <summary>
        /// Calculates the answer recursively, finds the amount of profit that can be made, maximum
        /// </summary>
        /// <param name="W"> Maximum power </param>
        /// <param name="m"> Number of items of each name </param>
        /// <param name="r"> Profit per item </param>
        /// <param name="w"> Power per item </param>
        /// <param name="n"> Number of item </param>
        /// <returns></returns>
        private int Recursivly(int W, int[] m, int[] r, int[] w, int n)
        {
            Counter++;                                                                                     // c1 | 1
            if (n == 0 || W == 0)                                                                          // c2 | 1
            {
                return 0;                                                                                  // c3 | 1
            }
            //Calculates profit with outh the item
            int maxProfitWithoutNthItem = Recursivly(W, m, r, w, n - 1);                                   // T(n - 1) | 1

            int maxProfit = 0;                                                                             // c4 | 1
            for (int k = 1; k <= m[n - 1] && k * w[n - 1] <= W; k++)                                       // c5 | m +1
            {
                int profitWithKItems = k * r[n - 1] + Recursivly(W - k * w[n - 1], m, r, w, n - 1);        // T(n - 1) | m
                maxProfit = Math.Max(maxProfit, profitWithKItems);                                         // c6 | m
            }

            return Math.Max(maxProfitWithoutNthItem, maxProfit);                                           // c7 | 1
        }


        /// <summary>
        /// Calculates the answer dynamically, finds the amount of profit that can be made, maximum
        /// </summary>
        /// <param name="W"> Maximum power </param>
        /// <param name="m"> Number of items of each name </param>
        /// <param name="r"> Profit per item </param>
        /// <param name="w"> Power per item </param>
        /// <param name="n"> Number of item </param>
        /// <returns></returns>
        private void CalculateDyn(int W, int[] m, int[] r, int[] w, int n)
        {
            int[,] P = new int[n + 1, W + 1];

            for (int i = 0; i <= n; i++)
            {
                Console.Write(i + ": ");
                for (int j = 0; j <= W; j++)
                {
                    if (i == 0 || j == 0)
                    {
                        P[i, j] = 0;
                    }
                    else if (w[i - 1] > j)
                    {
                        P[i, j] = P[i - 1, j];
                    }
                    else
                    {
                        int maxProfit = 0;
                        for (int k = 0; k <= m[i - 1] && k * w[i - 1] <= j; k++)
                        {

                            //When k = 0; We look for the maximum profit from other field,
                            //below P[i-1, j] and set that amount to max, the next iteration,
                            //we compare the max with one item that is i nad so on, and add max
                            //items on top of it from other maximums we calculated before.
                            maxProfit = Math.Max(maxProfit, k * r[i - 1] + P[i - 1, j - k * w[i - 1]]);
                        }
                        P[i, j] = maxProfit;
                    }
                    Console.Write(P[i, j] + " ");
                }
                Console.Write("\n");
            }
            MaxProfitDyn = P[n, W];


            for(int j = 0; j <= W; j++)
            {
                if(j == 0)
                {
                    Console.Write("   " + j);
                }
                else
                {
                    Console.Write(" " + j);
                }
                
            }
        }

        /// <summary>
        /// Calculates the answer recursively, finds the amount of profit that can be made, maximum
        /// </summary>
        /// <param name="W"> Maximum power </param>
        /// <param name="m"> Number of items of each name </param>
        /// <param name="r"> Profit per item </param>
        /// <param name="w"> POwer per item </param>
        /// <param name="n"> Number of item </param>
        /// <returns></returns>
        public void CalculateRec(int W, int[] m, int[] r, int[] w, int n)
        {
            MaxProfitRec = Recursivly(W, m, r, w, n);
        }

        /// <summary>
        /// Calculates the answer dynamically, finds the amount of profit that can be made, maximum
        /// </summary>
        /// <param name="W"> Maximum weight </param>
        /// <param name="m"> Number of items of each name </param>
        /// <param name="r"> Profit per item </param>
        /// <param name="w"> Power per item </param>
        /// <param name="n"> Number of item </param>
        /// <returns></returns>
        public void CalculateDin(int W, int[] m, int[] r, int[] w, int n)
        {

            CalculateDyn(W, m, r, w, n);
        }

        public void TotalItemCalculator(int n, int[] m)
        {
            for(int i = 0; i <= n; i ++)
            {
                TotalItemCounter+=m[i];
            }
        }
      

        /// <summary>
        /// Prints the answers
        /// </summary>
        public void Printer()
        {
            Console.WriteLine("Maksimalus pelnas naudojant rekursinį skaičiavimo metodą: " + MaxProfitRec);
            Console.WriteLine("Maksimalus pelnas naudojant dinaminį skaičiavimo metodą: " + MaxProfitDyn);

        }

    }


}





