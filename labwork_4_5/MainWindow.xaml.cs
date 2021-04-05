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
using System.Windows.Threading;
using System.Threading;

namespace labwork_4_5
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private int MAX_HEIGHT = 400;
        private const int MAX_WIDTH = 800;
        private int[] newArray = new int[10];
        private int[] numbers = new int[10];
        private Rectangle[] rectangles;
        private Thread sortingThread;

        public MainWindow()
        {
            InitializeComponent();
        }
        

        private void CreateArray()
        {
            string basicArray = "";
            Random random = new Random();
            for (int i = 0; i<newArray.Length;i++)
            {
                newArray[i] = random.Next(-20, 21);
                numbers[i] = newArray[i];
                basicArray += newArray[i] + "  ";
            }
            TextBoxArray.Text = basicArray;
        }

        private void RunAscSorting(object sender, MouseButtonEventArgs e)
        {
            RunAscSortingButton.IsEnabled = false;

            sortingThread = new Thread(delegate ()
            {
                numbers = InsertionSortAsc(numbers);
                PrintArrayToAscField(numbers);
            });
            sortingThread.Start();
        }
        private void RunDescSorting(object sender, MouseButtonEventArgs e)
        {
            RunDescSortingButton.IsEnabled = false;

            sortingThread = new Thread(delegate ()
            {
                numbers = InsertionSortDesc(numbers);
                PrintArrayToDescField(numbers);
            });
            sortingThread.Start(); 
        }


        private void StartThreads(object sender, RoutedEventArgs e)
        {
            MAX_HEIGHT = 400;
            AscendingOutputTextBox.Text = "";
            DescendingOutputTextBox.Text = "";
            CreateArray();
            InitializeRectangles(newArray);
            RunAscSortingButton.IsEnabled = true;
            RunDescSortingButton.IsEnabled = true;
        }

        public int[] InsertionSortAsc(int[] array)
        {
            int[] result = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                int j = i;
                while (j > 0 && result[j - 1] > array[i])
                {
                    SetOrangeColor(i, j - 1);
                    result[j] = result[j - 1];
                    SwapRectangles(j, j - 1);
                    Thread.Sleep(200);
                    SetRedColor(i, j - 1);
                    j--;
                }
                result[j] = array[i];
            }
            return result;
        }
        public int[] InsertionSortDesc(int[] array)
        {
            int[] result = new int[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                int j = i;
                while (j > 0 && result[j - 1] < array[i])
                {
                    SetOrangeColor(i, j - 1);
                    result[j] = result[j - 1];
                    SwapRectangles(j, j - 1);
                    Thread.Sleep(200);
                    SetRedColor(i, j - 1);
                    j--;
                }
                result[j] = array[i];
            }
            return result;
        }


        private void PrintArrayToAscField(int[] array)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                AscendingOutputTextBox.Text = "";
                foreach (int n in array)
                {
                    AscendingOutputTextBox.Text += n + " ";
                }
            });
        }
        private void PrintArrayToDescField(int[] array)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                DescendingOutputTextBox.Text = "";
                foreach (int n in array)
                {
                    DescendingOutputTextBox.Text += n + " ";
                }
            });
        }

        private void InitializeRectangles(int[] array)
        {
            if (HasNegative(array)) MAX_HEIGHT = MAX_HEIGHT / 2;

            int space = MAX_WIDTH / array.Length;
            int maxNumber = GetMax(array);
            int pixelsPerNumber = MAX_HEIGHT / maxNumber;
            DrawingCanvas.Children.Clear();
            rectangles = new Rectangle[array.Length];
            for (int i = 0; i < array.Length; i++)
            {
                int width = 25;
                int height = 0;
                int top = 0;
                if (array[i] < 0)
                {
                    height = (-1) * array[i] * pixelsPerNumber;
                    top = MAX_HEIGHT;
                }
                else
                {
                    height = array[i] * pixelsPerNumber;
                    top = MAX_HEIGHT - height;
                }
                int left = space * i;
                Rectangle rectangle = new Rectangle();
                SolidColorBrush myBrush = new SolidColorBrush(Colors.Red);
                rectangle.Fill = myBrush;
                rectangle.Height = height;
                rectangle.Width = width;
                rectangle.Margin = new Thickness(left, top, 0, 0);
                rectangles[i] = rectangle;
                DrawingCanvas.Children.Add(rectangle);
            }
        }

        private void SwapRectangles(int index1, int index2)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                int leftFirst = (int)rectangles[index1].Margin.Left;
                int leftSecond = (int)rectangles[index2].Margin.Left;
                int topFirst = (int)rectangles[index1].Margin.Top;
                int topSecond = (int)rectangles[index2].Margin.Top;
                rectangles[index1].Margin = new Thickness(leftSecond, topFirst, 0, 0);
                rectangles[index2].Margin = new Thickness(leftFirst, topSecond, 0, 0);
            });
            Rectangle temp = rectangles[index1];
            rectangles[index1] = rectangles[index2];
            rectangles[index2] = temp;
        }

        private void SetOrangeColor(int index1, int index2)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                rectangles[index1].Fill = new SolidColorBrush(Colors.Orange);
                rectangles[index2].Fill = new SolidColorBrush(Colors.Orange);
            });
        }

        private void SetRedColor(int index1, int index2)
        {
            this.Dispatcher.BeginInvoke(DispatcherPriority.Normal, (ThreadStart)delegate ()
            {
                rectangles[index1].Fill = new SolidColorBrush(Colors.Red);
                rectangles[index2].Fill = new SolidColorBrush(Colors.Red);
            });
        }

        private int GetMax(int[] array)
        {
            int temp = array[0];
            foreach (int n in array)
            {
                if (n < 0)
                {
                    if (-1 * n > temp) temp = -1 * n;
                }
                if (n > temp) temp = n;
            }
            return temp;
        }

        private bool HasNegative(int[] array)
        {
            foreach (int n in array)
            {
                if (n < 0) return true;
            }
            return false;
        }

    }
}
