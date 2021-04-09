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

namespace labwork_4_8
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

        private void Button_SearchPrimeNumbers(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Convert.ToInt32(TextBoxMinValue.Text) >=0 && Convert.ToInt32(TextBoxMaxValue.Text) >=0)
                {
                    if (Convert.ToInt32(TextBoxMinValue.Text)<= Convert.ToInt32(TextBoxMaxValue.Text))
                    {
                        (DataContext as PrimeSearcher).min = Convert.ToInt32(TextBoxMinValue.Text);
                        (DataContext as PrimeSearcher).max = Convert.ToInt32(TextBoxMaxValue.Text);

                        (DataContext as PrimeSearcher).StartSearching();

                        (DataContext as PrimeSearcher).JoinThreads();

                        TextBoxResult.Text = (DataContext as PrimeSearcher).result;
                    }
                    else
                    {
                        MessageBox.Show("Начальное число не может быть больше последнего");
                    }

                }
                else
                {
                    MessageBox.Show("Простые числа больше или равны 0");
                }
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
    }
}
