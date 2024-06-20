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
        static void Main(string[] args)
        {
            Utils.Execute(100);
            Console.ReadKey();
            Console.ReadKey();

        }

    }



    static class Utils
    {
        

        public static void Execute(int size)
        {
            int[] n = {size, size, size * 2, size * 4, size * 8, size * 16, size * 32, size * 64 };

         
            var watch = new System.Diagnostics.Stopwatch();

            
            for (int i = 0; i < n.Count(); i++)
            {
                int[] A2 = FillOnes(n[i]/2);
                watch.Start();
                long result = methodToAnalysis(A2);
                watch.Stop();
                Console.WriteLine($"Formula T1 | Unit count: {n[i]/2} | Result: {result} | Execution Time: {watch.ElapsedMilliseconds} ms");
                watch.Reset();

            }

            Console.WriteLine();
            watch.Reset();

            

            for (int i = 0; i < n.Count(); i++)
            {
                int[] A2 = Fill(n[i]);
                watch.Start();
                long result = methodToAnalysis2(n[i], A2);
                watch.Stop();
                Console.WriteLine($"Formula T2 | Unit count: {n[i]} | Result: {result} | Execution Time: {watch.ElapsedMilliseconds} ms");
                watch.Reset();
        
            }

           
        }


        private static int[] Fill(int m)
        {
            int[] p = new int[m];
            for(int i = 0; i < m; i++)
            {
                p[i] = 0;
            }
            return p;
        }

        private static int[] FillOnes(int m)
        {
            int[] p = new int[m];
            for (int i = 0; i < m; i++)
            {
                p[i] = 1;
            }
            return p;
        }


        static long methodToAnalysis(int[] arr)
        {
            long n = arr.Length;                                // c1 | 1
            long k = n;                                         // c2 | 1
            for (int i = 0; i < n; i++)                         // c3 | 1
            {                                                   // c4 | n + 1
                if (arr[i] > 0)                                 // c5 | n
                {
                    for (int j = 0; j < n * n / 2; j++)         // c6 | n
                    {                                           // c7 | (n * n / 2 + 1) * n ((n^2 / 2 + 1) * n) -> (n^3/2 + n)

                        k -= 2;                                 // c8 | n * n * n / 2

                    }
                    for (int j = 0; j < n * n / 2; j++)         // c9 | n
                    {                                           // c10 | n * n * n / 2 + 1 -> (n^3/2 + n)

                        k += 3;                                 // c11 | n * n * n / 2
                    }
                }
            }
            return k;                                           // c12 | 1
        }


        //
        static long methodToAnalysis2(int n, int[] arr)
        {
            long k = 0;                                         // c1 | 1
            for (int i = 0; i < n; i++)                         // c2 | 1
            {                                                   // c3 | n + 1
                k += k;                                         // c4 | n

                k += FF6(i * 2, arr);                           // sum(i = 0; n = n; T(i*2) ) | n
            }
            k += FF6(n, arr);                                   // T(n) | 1
            return k;                                           // c5 | 1

        }

        static long FF6(int n, int[] arr)
        {
            if (n > 0 && arr.Length > n)                        // c6 | 1

            {
                
                return FF6(n - 1, arr);                         // T(n - 1) | 1

            }
            return n;                                           // c7 | 1
        }
    }
}
