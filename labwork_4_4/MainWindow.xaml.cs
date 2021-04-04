﻿using System;
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
using OxyPlot;
using OxyPlot.Series;
using System.Threading;

namespace labwork_4_4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            new Thread(
                delegate()
                {
                    Graphic.Series.Add(new FunctionSeries(CreateGraphic, -200, 200, 0.01, "23*x^2–33"));
                }).Start();

            DrawGraphic();
        }

        PlotModel Graphic = new PlotModel();

        public double CreateGraphic(double x)
        {
            return 23 * x * x - 33;
        }
        public void DrawGraphic()
        {
            GraphicField.Model = Graphic;
        }
    }
}