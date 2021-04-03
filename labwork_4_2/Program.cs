using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace labwork_4_2
{
    class Program
    {
        static Mutex mutexObj = new Mutex();

        static ulong j = 1;
        static int p = 0;
        static decimal w = 1;

        static void Main(string[] args)
        {
            
            for (int i = 0; i < 40; i++)
            {
                Thread newThread = new Thread(Result);
                
                newThread.Start();
                
            }

            Console.ReadLine();
        }

        public static void Result()
        {
            
            mutexObj.WaitOne();
            Console.WriteLine(w);
            double x = 1;
            p++;
            w += Convert.ToDecimal(j*Math.Pow(Math.Sin(x),p));
            j *= 2;

            w -= Convert.ToDecimal(j *Math.Pow(Math.Cos(x), p));

            j *= 2;

            Thread.Sleep(700);
            mutexObj.ReleaseMutex();
        }
       
    }
}
