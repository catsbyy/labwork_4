using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace labwork_4_3
{
    class FibonacciNumbers : Numbers
    {
        int number;
        public FibonacciNumbers(int n)
        {
            number = n;
        }
        public override void ProcessNumbers()
        {
            Mutex mutex = new Mutex();
            mutex.WaitOne();

            string path = @"fibonacci.txt";
            for (int i = 0; i < number; i++)
            {
                try
                {
                    using (StreamWriter sw = new StreamWriter(path, true))
                    {
                        sw.WriteLine(FibonacciNumber(i));
                    }

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Числа Фибоначчи записаны в файл");
            ReadFile(path);
            mutex.ReleaseMutex();
            
        }
        static int FibonacciNumber(int number)
        {
            if (number == 0)
                return 0;
            if (number == 1)
                return 1;
            return FibonacciNumber(number - 1) + FibonacciNumber(number - 2);
        }
    }
}
