using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace labwork_4_3
{
    class PrimeNumbers : Numbers
    {
        int number;
        public PrimeNumbers(int n)
        {
            number = n;
        }
        public override void ProcessNumbers()
        {
            Mutex mutex = new Mutex();
            mutex.WaitOne();

            string path = @"primeNumbers.txt";
            for (int i = 2; i < number; i++)
            {
                if (IsPrime(i))
                {
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(path, true))
                        {
                            sw.WriteLine(i);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            }
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Простые числа записаны в файл");
            ReadFile(path);
            mutex.ReleaseMutex();
            
        }
        bool IsPrime(int number)
        {
            if (number <= 1) return false;
            if (number == 2) return true;
            if (number % 2 == 0) return false;

            var boundary = (int)Math.Floor(Math.Sqrt(number));

            for (int i = 3; i <= boundary; i += 2)
                if (number % i == 0)
                    return false;

            return true;
        }
    }
}
