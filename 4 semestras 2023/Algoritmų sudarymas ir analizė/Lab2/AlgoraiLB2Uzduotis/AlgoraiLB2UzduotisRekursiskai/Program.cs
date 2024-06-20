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
            const int n = 10;
            const int m = 10;
            int Max = 0;
            int[,] board = new int[n, m];
            var rand = new Random();

            for (int i = 0; i < n; i++)
            {
                Console.Write("\n");
                for (int j = 0; j < m; j++)
                {
                    board[i, j] = rand.Next(0, 9);
                    Console.Write(board[i, j]);
                }
            }


            void MaxValue(int max,int x,int y)
            {
                max = max + board[x, y];
                if (Max < max)
                {
                    Max = max;
                }
                
                if(n-1 > x)
                {
                    MaxValue(max, x + 1, y);
                }
                if (m-1 > y)
                {
                    MaxValue(max, x, y + 1 );
                }
                if (n-1 > x && m-1 > y)
                {
                    MaxValue(max, x + 1, y + 1);
                }

                
            }



            MaxValue(Max, 0, 0);
            Console.WriteLine();
            Console.WriteLine(Max);
        }
    }
}