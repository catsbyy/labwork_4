using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace labwork_4_8
{
    class PrimeSearcher : INotifyPropertyChanged
    {
        public int min;
        public int max;
        public string result { get; set; }
        public Thread thread;

        public PrimeSearcher()
        {
            thread = new Thread(SearchPrime);
        }
        public void StartSearching()
        {
            thread.Start();
        }
        public string Result
        {
            get { return result; }
            set
            {
                result = value;
                OnPropertyChanged("Result");
            }
        }
        public void SearchPrime()
        {
            Mutex mutex = new Mutex();
            mutex.WaitOne();
            result = "";

            for (int i = min; i <= max; i++)
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

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
