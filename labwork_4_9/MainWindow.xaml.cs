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

namespace labwork_4_9
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

        public int x { get; set; }
        public int y { get; set; }
        public int average;
        public int nod { get; set; }
        
        public void EuclidianAlgorithm()
        {
            while (x != y)
            {
                if (x > y)
                {
                    x = x - y;
                }
                else
                {
                    y = y - x;
                }
            }
            nod = y;
        }
        private void Button_SearchNOD(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(TextBoxValueX.Text) > 0 && Convert.ToInt32(TextBoxValueY.Text) > 0)
                {
                    x = Convert.ToInt32(TextBoxValueX.Text);
                    y = Convert.ToInt32(TextBoxValueY.Text);

                    Thread thread = new Thread(EuclidianAlgorithm);
                    thread.Start();
                    thread.Join();

                    TextBoxResult.Text = nod.ToString();
                }
                else
                {
                    MessageBox.Show("Чтобы использовать алгоритм Евклида, числа должны быть неотрицательными и не равняться 0");
                }
            }
            catch
            {
                MessageBox.Show("Проверьте корректность данных");
            }
        }
    }

}
