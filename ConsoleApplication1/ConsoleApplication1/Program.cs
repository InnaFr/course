using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static int ind = 0;
        static string p = "";
        static int N = 0;
        static string[] masStr = new string[2];
        static int[] masInt = new int[2];
        public static int metk = 0;
        public static void Gamers()
        {
            p = "";
            if (ind % 2 == 0)
            {
                Console.WriteLine("1 игрок");
                metk = 1;
            }
            else
            {
                Console.WriteLine("2 игрок");
                metk = 2;
            }
        }

        public static void Print(int[,] matrix)
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    Console.Write(matrix[i, j] + " ");
                }
                Console.WriteLine("");
            }
        }

        public static void MatrixFull(int[,] matrix, int N)
        {
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    matrix[i, j] = 0;
                }
            }
        }
        
      
                   
        public static int HopeFindZero(int[,] matrix, int N, int Hope)
        {
            Hope = 0;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (matrix[i, j] == 0) Hope++;
                }
            }
            if (Hope == 0)
            {
                Console.WriteLine("Ничья");
                Console.ReadKey();
                Environment.Exit(0);
            }
            return Hope;
        }

        public static void Koord()
        {
            Gamers();
            p = "";
            for (int i = 0; i < 2; i++)
            {
                masStr[i] = "";
                masInt[i] = 0;
            }
            Console.WriteLine("Введите координаты в формате (X Y)");
            p = Console.ReadLine();
            p = p.Remove(0, 1);
            p = p.Remove(p.Length - 1, 1);
            masStr = p.Split(' ');
            for (int i = 0; i < 2; i++)
            {
                masInt[i] = int.Parse(masStr[i]);
                if (masInt[i]>=N)
                {
                    Console.WriteLine("Введенные координаты вне поля!");
                    Console.ReadKey();
                    Environment.Exit(0);
                    
                }
            }

        }
        static void Main(string[] args)
        {
            int buf = 0;
            Console.WriteLine("Введите размерность поля");
            p = Console.ReadLine();
            N = int.Parse(p);

            if (N < 0)
            {
                Console.WriteLine("Недопустимое значение размерности поля!");
                Environment.Exit(0);
            }

            int[,] matrix = new int[N, N];
            Koord();
            ind++;
            int schet_1 = 0;
            int schet_2 = 0;
            MatrixFull(matrix, N);

            for (int i = 0; i < 2; i++)
            {
                if (masInt[i] >= N || masInt[i] < 0)
                {
                    buf = -1;
                }
            }

            int hope = -1;
            if (buf != -1)
            {
                while (hope != 0)
                {
                    schet_1 = 0;
                    schet_2 = 0;

                    hope = HopeFindZero(matrix, N, hope);

                    if (matrix[masInt[0], masInt[1]] == 0)
                    {
                        if (metk == 1)
                        {
                            matrix[masInt[0], masInt[1]] = 1;
                        }
                        else
                        {
                            matrix[masInt[0], masInt[1]] = 2;
                        }
                    }
                    else
                    {
                        Console.WriteLine("Ячейка занята");
                        ind--;
                        Koord();
                        ind++;
                    }
                    Print(matrix);

                    for (int i = 0; i < N; i++)
                    {
                        if (matrix[i, i] != matrix[1, 1] || matrix[i, i] == 0)
                        {
                            schet_1++;
                        }

                        if (matrix[i, N - i - 1] != matrix[0, N - 1] || matrix[i, N - i - 1] == 0)
                        {
                            schet_2++;
                        }
                    }

                    if (schet_1 == 0 || schet_2 == 0)
                    {
                        Console.WriteLine("Побеждает " + metk + " игрок");
                        Console.ReadKey();
                        Environment.Exit(0);
                    }

                    schet_1 = 0;
                    schet_2 = 0;
                    for (int i = 0; i < N; i++)
                    {
                        for (int j = 0; j < N; j++)
                        {
                            if (matrix[i, 0] != matrix[i, j] || matrix[i, j] == 0)
                            {
                                schet_1++;
                            }

                            if (matrix[0, i] != matrix[j, i] || matrix[i, j] == 0)
                            {
                                schet_2++;
                            }

                        }
                        if (schet_1 == 0 || schet_2 == 0)
                        {
                            Console.WriteLine("Побеждает " + metk + " игрок");
                            Console.ReadKey();
                            Environment.Exit(0);
                        }
                    }
                    Koord();
                    ind++;
                }
            }
            else
            {
                Console.WriteLine("Координаты за пределами поля");
            }

        }

    }
}
