using System;
using System.Threading;

namespace labwork_4_7
{
//Авиаразведка.Создается условная карта в виде матрицы, размерность которой определяет размер карты, содержащей произвольное количество единиц(целей) в произвольных ячейках.
//Из произвольной точки карты стартуют несколько разведчиков(потоков), курсы которых выбираются так, чтобы покрыть максимальную площадь карты.
//Каждый разведчик фиксирует цели, чьи координаты совпадают с его координатами и по достижении границ карты сообщает количество обнаруженных целей.
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
    class Scout
    {
        static Semaphore sem = new Semaphore(Program.mapSize, Program.mapSize);
        Thread thread;
        int startPlaceX;
        int startPlaceY;

        string[] discoveredPlaces = new string[0];

        public Scout(int i)
        {
            thread = new Thread(StartScouting);
            thread.Name = $"{i.ToString()}";
            thread.Start();
        }
        public void addPlace(int row, int column)
        {
            Array.Resize(ref discoveredPlaces, discoveredPlaces.Length + 1);
            discoveredPlaces[discoveredPlaces.Length - 1] = $"({row},{column})";
        }

        public void StartScouting()
        {
            sem.WaitOne();

            startPlaceX = Program.random.Next(1, Program.mapSize + 1);
            startPlaceY = Program.random.Next(1, Program.mapSize + 1);

            Console.WriteLine($"Точка старта {Thread.CurrentThread.Name} разведчика: ({startPlaceX}, {startPlaceY})");

            for (int k = 1; k <= Program.mapSize; k++)
            {
                if (k<startPlaceX)
                {
                    continue;
                }
                else
                {
                    for (int j = 1; j <= Program.mapSize; j++)
                    {
                        if (k == startPlaceX && j < startPlaceY)
                        {
                            continue;
                        }
                        else
                        {
                            if (Program.map[k, j] == 1)
                            {
                                addPlace(k, j);
                            }
                            else
                            {
                                continue;
                            }
                        }
                    }
                }
            }
            Console.WriteLine($"Разведчик {Thread.CurrentThread.Name} обнаружил объектов: {discoveredPlaces.Length}");
            sem.Release();
            Thread.Sleep(1000);
        }
    }
}
