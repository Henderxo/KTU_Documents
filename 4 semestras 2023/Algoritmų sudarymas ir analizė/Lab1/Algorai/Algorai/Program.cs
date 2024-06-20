using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Algorai
{
    class Program
    {

        static void Main(string[] args)
        {
            Utils.Execute(200);
            Console.ReadKey();
            Console.ReadKey();
        }

    }

    
    


    static class Utils
    {
        
        public static void Execute(int size)
        {
            int[] n = { size, size * 2, size * 4, size * 8, size * 16 , size * 32, size * 64 };
            int[] n3 = { size, size * 2, size * 4, size * 6, size * 8, size * 12, size * 13, size * 14, size * 15 };
            int[] A = new int[size * 64 * 64 * size];
            var watch = new System.Diagnostics.Stopwatch();

            for (int i = 0; i < n.Count(); i++)
            {

                watch.Start();
                T1(A, n[i]);
                watch.Stop();
                Console.WriteLine($"Formula T1 | Unit count: {n[i]} | Execution Time: {watch.ElapsedMilliseconds} ms");

                
            }
            
            Console.WriteLine();
            watch.Reset();
            for (int i = 0; i < n.Count(); i++)
            {

                watch.Start();
                T2(A, n[i]);
                watch.Stop();
                Console.WriteLine($"Formula T2 | Unit count: {n[i]} | Execution Time: {watch.ElapsedMilliseconds} ms");


            }
            Console.WriteLine();
            watch.Reset();
            for (int i = 0; i < n3.Count(); i++)
            {

                watch.Start();
                T3(A, n3[i]/20);
                watch.Stop();
                Console.WriteLine($"Formula T3 | Unit count: {n3[i]/20} | Execution Time: {watch.ElapsedMilliseconds} ms");


            }
        }

        

        //T(n) = 2 ∗ T(n/10) + n^2
        public static void T1(int[] A, int n)
        {

            

            if (n < 11)                            // c1 | 1
                return;                            // c2 | 1
            T1(A, n / 10);                         // T(n/10) | 1
            T1(A, n / 10);                         // T(n/10) | 1

            for (int i = 0; i < n*n; i++)          // c3 + c4 | 1
            {
                A[i] = 0;                          // c5 + c4 | n^2
            }

        }
        //        { c1 + c2 , n < 10
        // T(k) = {
        //        { T(n/10) + T(n/10) + c1 + c3 + c4 + (c5 + c4) * n^2, n >= 10



        //T(n)=T(n/7)+ T(n/8)+ n^2
        public static void T2(int[] A, int n)
        {
            if (n == 0) return;                // c1 + c2 | 1
            T2(A, n / 7);                      // T(n/7) | 1
            T2(A, n / 8);                      // T(n/8) | 1

            for (int i = 0; i < n*n; i++)      // c3 + c4 | 1
            {
                A[i] = 0;                      // c4 + c5 | n^2
            }
        }
        //        { c1 + c2 , n < 1
        // T(k) = {
        //        { T(n/7) + T(n/8) + c1 + c3 + c4 + (c5 + c4) * n^2, n >= 1

        //T(n)=T(n−7)+ T(n−6) + n
        public static void T3(int[] A, int n)
        {
            if (n < 7) return;                      // c1 + c2 | 1
            T3(A, n - 7);                           // T(n - 7) | 1
            T3(A, n - 6);                           // T(n - 6) | 1

            for (int i = 0; i < n; i++)             // c3 + c4 | 1
            {
                A[i] = 0;                           // c4 + c5 | n
            }
        }

        //        { c1 + c2 , n <= 7
        // T(k) = {
        //        { T(n - 7) + T(n - 6) + c1 + c3 + c4 + (c5 + c4) * n, n > 7
    }
}
