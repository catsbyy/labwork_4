using System;
using System.Threading;
using System.Threading.Tasks;

//Бег с препятствиями. Создается условная карта трассы в виде матрицы, ширина которой соответствует количеству бегунов, а высота –фиксирована,
//содержащей произвольное количество единиц (препятствий) в произвольных ячейках.
//Стартующие бегуны (потоки) перемещаются по трассе и при встрече с препятствием задерживаются на фиксированное время.
//По достижении финиша бегуны сообщают свой номер.

namespace labwork_4_6
{
    class Program
    {
        static void Main(string[] args)
        {
            int runners;
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
            while (runners <=0);

            Random random = new Random();
            int[,] map = new int[runners,runners];
            for (int i=0;i<runners;i++)
            {
                for (int j=0;j<runners;j++)
                {
                    map[i, j] = random.Next(0, 2);
                    Console.Write(map[i, j] + "  ");
                }
                Console.WriteLine();
            }
        }
    }
}
