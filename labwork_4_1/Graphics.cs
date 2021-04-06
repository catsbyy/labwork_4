using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using OxyPlot;
using OxyPlot.Series;

namespace labwork_4_1
{
    class Graphics
    {
        public PlotModel Sin { get; set; }
        public PlotModel Parabola { get; set; }
        public PlotModel NaturalLogarithm { get; set; }
        public Graphics()
        {
            Parallel.Invoke(
                () =>
                {
                    Sin = new PlotModel { Title = "Sin" };
                    Sin.Series.Add(new FunctionSeries(Math.Sin, -10, 20, 0.1, "sin(x)"));
                },

                () =>
                {
                    Parabola = new PlotModel { Title = "Parabola" };
                    Parabola.Series.Add(new FunctionSeries(CreateParabola, -40, 40, 0.1, "4x^2-2x–22"));
                },

                () =>
                {
                    NaturalLogarithm = new PlotModel { Title = "Natural logarithm" };
                    NaturalLogarithm.Series.Add(new FunctionSeries(CreateLn, 0, 50, 0.1, "ln(x^2)/x^3"));
                    NaturalLogarithm.Series.Add(new FunctionSeries(CreateLn, -50, -0.1, 0.1, "ln(x^2)/x^3"));
                }
                );
        }
        public double CreateParabola(double x)
        {
            return 4 * x * x - 2 * x - 22;
        }
        public double CreateLn(double x)
        {
            return Math.Log(Math.Pow(x, 2))/ Math.Pow(x, 3);
        }

    }
}
