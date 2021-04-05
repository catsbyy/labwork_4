using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;
using System.Threading;


namespace labwork_4_4
{
    class Graphic
    {
        static Mutex mutex = new Mutex();
        public IList<DataPoint> Points { get; private set; }
        public string Title { get; private set; }
        public Graphic()
        {
            new Thread(
                delegate ()
                {
                    Title = "23*x^2–33";
                }).Start();

            mutex.WaitOne();
            new Thread(
                delegate ()
                {
                    this.Points = new List<DataPoint>();
                    for (double i = -200; i <= 200; i += 0.01)
                    {
                        DataPoint dataPoint = new DataPoint(i, 23 * i * i - 33);
                        Points.Add(dataPoint);
                    }    
                }).Start();
        }
        public double CreateGraphic(double x)
        {
            return 23 * x * x - 33;
        }
    }
}
