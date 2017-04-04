﻿using System;
using System.IO;
using System.Threading;
using OxyPlot;
using OxyPlot.Axes;
//using OxyPlot.Wpf;

namespace KozzionPerfusionCL.Chart
{
    /// <summary>
    /// Interaction logic for NormalDistributionsWindow.xaml
    /// </summary>
    public class NormalDistribution
    {

        PlotModel Model;
        /// <summary>
        /// Initializes a new instance of the <see cref="NormalDistributionsWindow"/> class.
        /// </summary>
        public NormalDistribution()
        {
            this.Model = CreateNormalDistributionModel();
        }

        /// <summary>
        /// Creates a model showing normal distributions.
        /// </summary>
        /// <returns>A PlotModel.</returns>
        private static PlotModel CreateNormalDistributionModel()
        {
            // http://en.wikipedia.org/wiki/Normal_distribution
            var plot = new PlotModel
            {
                Title = "Normal distribution",
                Subtitle = "Probability density function"
            };

            plot.Axes.Add(new OxyPlot.Axes.LinearAxis
            {
                Position = AxisPosition.Left,
                Minimum = -0.05,
                Maximum = 1.05,
                MajorStep = 0.2,
                MinorStep = 0.05,
                TickStyle = TickStyle.Inside
            });
            plot.Axes.Add(new OxyPlot.Axes.LinearAxis
            {
                Position = AxisPosition.Bottom,
                Minimum = -5.25,
                Maximum = 5.25,
                MajorStep = 1,
                MinorStep = 0.25,
                TickStyle = TickStyle.Inside
            });
            plot.Series.Add(CreateNormalDistributionSeries(-5, 5, 0, 0.2));
            plot.Series.Add(CreateNormalDistributionSeries(-5, 5, 0, 1));
            plot.Series.Add(CreateNormalDistributionSeries(-5, 5, 0, 5));
            plot.Series.Add(CreateNormalDistributionSeries(-5, 5, -2, 0.5));
            return plot;
        }

        private static OxyPlot.Series.LineSeries CreateNormalDistributionSeries(double x0, double x1, double mean, double variance, int n = 1000)
        {
            OxyPlot.Series.LineSeries ls = new OxyPlot.Series.LineSeries();

            ls.Title = string.Format("μ={0}, σ²={1}", mean, variance);
            for (int i = 0; i < n; i++)
            {
                double x = x0 + ((x1 - x0) * i / (n - 1));
                double f = 1.0 / Math.Sqrt(2 * Math.PI * variance) * Math.Exp(-(x - mean) * (x - mean) / 2 / variance);
                ls.Points.Add(new DataPoint(x, f));
            }

            return ls;
        }


        //public void SaveToFile(string file_name)
        //{
        //    Thread myThread = new Thread(() =>
        //    {
        //        using (var stream = File.Create(file_name))
        //        {
        //            PngExporter.Export(this.Model, stream, 600, 400, OxyColor.FromRgb(255, 255, 255), 96);
        //        }
        //    });

        //    myThread.SetApartmentState(ApartmentState.STA);
        //    myThread.Start();
        //}
          



    }
}