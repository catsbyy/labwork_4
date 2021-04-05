using System;

namespace labwork_4_6
{
    class Program
    {
        public static int runners;
        public static int[,] map;
        static void Main(string[] args)
        {
            EnterRunners();
            CreateMap();

            Console.WriteLine();

            for (int i = 1; i <= runners; i++)
            {
                Runner runner = new Runner(i);
            }
        }
        private static void EnterRunners()
        {
            do
            {
                Console.Write("Введите количество бегунов: ");
                runners = int.Parse(Console.ReadLine());
                if (runners <= 0)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Количество бегунов не может быть отрицательным или 0\n");
                    Console.ResetColor();
                }
            }
            while (runners <= 0);
        }
        private static void CreateMap()
        {
            Random random = new Random();
            map = new int[runners + 1, runners + 1];

            Console.WriteLine("Трассы:");
            for (int i = 1; i <= runners; i++)
            {
                for (int j = 1; j <= runners; j++)
                {
                    map[i, j] = random.Next(0, 2);
                    Console.Write(map[i, j] + "  ");
                }
                Console.WriteLine();
            }
        }
    }
}
