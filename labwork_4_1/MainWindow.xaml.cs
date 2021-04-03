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
using OxyPlot;
using OxyPlot.Series;


namespace labwork_4_1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DrawGraphics();

        }

        static Semaphore sem = new Semaphore(3, 3);
        public PlotModel Sin { get; set; }
        public PlotModel Parabola { get; set; }
        public PlotModel NaturalLogarithm { get; set; }


        public void DrawGraphics()
        {
            sem.WaitOne();
            Sin = new PlotModel { Title = "Sin" };
            Sin.Series.Add(new FunctionSeries(Math.Sin, -10, 20, 0.1, "sin(x)"));
            SinField.Model = Sin;

            Parabola = new PlotModel { Title = "Parabola" };
            Parabola.Series.Add(new FunctionSeries(CreateParabola, -40, 40, 0.1, "4x^2-2x–22"));
            ParabolaField.Model = Parabola;

            NaturalLogarithm = new PlotModel { Title = "Natural logarithm" };
            NaturalLogarithm.Series.Add(new FunctionSeries(CreateLn, 0, 50, 0.1, "ln(x^2)/x^3"));
            LnField.Model = NaturalLogarithm;
            sem.Release();
        }

        public double CreateParabola(double x)
        {
            return 4 * x*x - 2 * x - 22;
        }
        public double CreateLn(double x)
        {
            return Math.Log(Math.Pow(x, 2) / Math.Pow(x, 3));
        }

    }
}
