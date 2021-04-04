using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Threading;

namespace labwork_4_3
{
    abstract class Numbers
    {
        public void ReadFile(string path)
        {
            Mutex mutex = new Mutex();
            mutex.WaitOne();
            try
            {
                using (StreamReader sr = new StreamReader(path, Encoding.Default))
                {
                    int counter = 0;
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Console.Write(line + ", ");
                        counter++;
                    }
                    Console.WriteLine($"\nКоличество элементов: {counter}\n");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.ResetColor();
            mutex.ReleaseMutex();
        }
        public virtual void ProcessNumbers() { }
    }
}
