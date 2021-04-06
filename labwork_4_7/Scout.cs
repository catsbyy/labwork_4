using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace labwork_4_7
{
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
                if (k < startPlaceX)
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
