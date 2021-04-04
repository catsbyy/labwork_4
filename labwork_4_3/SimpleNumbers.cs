using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.IO;

namespace labwork_4_3
{

    class SimpleNumbers : Numbers
    {
        int number;
        public SimpleNumbers(int n)
        {
            number = n;
        }
        public override void ProcessNumbers()
        {
            Mutex mutex = new Mutex();
            mutex.WaitOne();

            string path = @"simpleNumbers.txt";
            for (int i = 2; i < number; i++)
            {
                if (isSimple(i))
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
        static bool isSimple(int n)
        {
            for (int i = 2; i < (int)(n / 2); i++)
            {
                if (n % i == 0)
                    return false;
            }
            return true;
        }
    }
}
