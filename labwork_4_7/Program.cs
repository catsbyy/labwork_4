using System;

namespace labwork_4_7
{
    class Program
    {
        public static int mapSize;
        public static int[,] map;

        public static Random random = new Random();
        static void Main(string[] args)
        {
            map = new int[10, 10];
            CreateMap();

            Console.WriteLine();

            for (int i = 1; i <= random.Next(2,6); i++)
            {
                Scout scout = new Scout(i);
            }
        }

        private static void CreateMap()
        {
            
            mapSize = random.Next(5, 11);
            map = new int[mapSize+1, mapSize + 1];

            Console.WriteLine("Область разведки:");
            for (int i = 1; i <= mapSize; i++)
            {
                for (int j = 1; j <= mapSize; j++)
                {
                    map[i, j] = random.Next(0, 2);
                    Console.Write(map[i, j] + "  ");
                }
                Console.WriteLine();
            }
        }
    }  
}
