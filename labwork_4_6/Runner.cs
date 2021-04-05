using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace labwork_4_6
{
    class Runner
    {
        static Semaphore sem = new Semaphore(Program.runners, Program.runners);
        Thread thread;

        public Runner(int i)
        {
            thread = new Thread(Run);
            thread.Name = $"{i.ToString()}";
            thread.Start();
        }

        public void Run()
        {
            sem.WaitOne();

            for (int k = 1; k <= Program.runners; k++)
            {
                if (Program.map[Convert.ToInt32(Thread.CurrentThread.Name), k] == 1)
                {
                    Thread.Sleep(1000);
                }
                else
                {
                    continue;
                }
            }
            Console.WriteLine(Thread.CurrentThread.Name + " бегун финишировал");
            sem.Release();
        }
    }
}
