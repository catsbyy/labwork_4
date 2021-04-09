using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace labwork_4_2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Start(object sender, RoutedEventArgs e)
        {
            try
            {
                (DataContext as Calculation).x = Convert.ToDouble(TextBoxValueX.Text);
                (DataContext as Calculation).StartCalculation();
            }
            catch
            {
                MessageBox.Show("Проверьте корректность данных");
            }
        }

        private void Button_NewValues(object sender, RoutedEventArgs e)
        {
            System.Diagnostics.Process.Start(Application.ResourceAssembly.Location);
            Application.Current.Shutdown();
        }

        private void Button_Stop(object sender, RoutedEventArgs e)
        {
            (DataContext as Calculation).StopCalculation();
        }
    }
    class Calculation : INotifyPropertyChanged
    {
        static Mutex mutex = new Mutex();

        static ulong j = 1;
        static int p = 0;
        public double w;
        public double x { get; set; }
        private bool IsRunning = true;
        Thread thread;

        public Calculation()
        {
            thread = new Thread(Calculate);
        }
        public double W
        {
            get { return w; }
            set
            {
                w = value;
                OnPropertyChanged("W");
            }
        }

        public void Calculate()
        {
            mutex.WaitOne();
            W = 1;
            
            while(true)
            {
                p++;
                W += j * Math.Pow(Math.Sin(x), p);
                j *= 2;

                W -= j * Math.Pow(Math.Cos(x), p);

                j *= 2;
                Thread.Sleep(1000);

                if (!IsRunning)
                    break;
            }
            mutex.ReleaseMutex();
        }
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void StartCalculation()
        {
            IsRunning = true;
            thread.Start();
        }
        public void StopCalculation()
        {
            IsRunning = false;
        }
    }
    
}
