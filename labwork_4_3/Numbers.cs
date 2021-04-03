using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace labwork_4_3
{
    abstract class Numbers
    {
        public void ReadFile(string path)
        {
            try
            {
                using (StreamReader sr = new StreamReader(path, System.Text.Encoding.Default))
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
        }
        public virtual void ProcessNumbers() { }
    }
}
