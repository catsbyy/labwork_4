using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace labwork_4_8
{
    class PrimeSearcher
    {
        public int min;
        public int max;
        int average;

        public string result { get; set; }
        public string result1 { get; set; }
        public string result2 { get; set; }
        public Thread thread1;
        public Thread thread2;

        public PrimeSearcher()
        {
            if (min == max)
            {
                thread1 = new Thread(
                    () =>
                    {
                        SearchPrime(min, max);
                    });
                thread2 = new Thread(
                    ()=> { });
            }
            else if ((min + max) % 2 == 0)
            {
                average = (min + max) / 2;

                thread1 = new Thread(
                    () =>
                    {
                        SearchPrime(min, average);
                    });
                thread2 = new Thread(
                    () =>
                    {
                        SearchPrime(average, max);
                    });
            }
        }
        public void StartSearching()
        {
            thread1.Start();
            thread2.Start();
        }
       
        public void SearchPrime(int firstvalue, int secondvalue)
        {
            Mutex mutex = new Mutex();
            mutex.WaitOne();

            for (int i = firstvalue; i <= secondvalue; i++)
            {
                if (IsPrime(i))
                {
                    result += i + "  ";
                }
                else
                {
                    continue;
                }    
            }
            mutex.ReleaseMutex();
        }
        public bool IsPrime(int number)
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

        public void JoinThreads()
        {
            result = result1 + result2;
            thread1.Join();
            thread2.Join(); 
        }
      
    }
}
