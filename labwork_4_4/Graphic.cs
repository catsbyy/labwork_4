using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using OxyPlot.Series;
using System.Threading;


namespace labwork_4_4
{
    class Graphic
    {
        static Mutex mutex = new Mutex();
        private List<double> coordinates = new List<double>();
        public PlotModel graphic { get; set; }
        public Graphic()
        {
            new Thread(
                delegate ()
                {
                    graphic = new PlotModel { Title = "23*x^2–33" };
                    graphic.Series.Add(new FunctionSeries(CreateGraphic, -200, 200, 0.01, "23*x^2–33"));
                }).Start();

            mutex.WaitOne();
            new Thread(
                delegate ()
                {
                    for (double i = -200; i <= 200; i += 0.01)
                        coordinates.Add(23 * i * i - 33);
                }).Start();
        }
        public double CreateGraphic(double x)
        {
            return 23 * x * x - 33;
        }
    }
}
