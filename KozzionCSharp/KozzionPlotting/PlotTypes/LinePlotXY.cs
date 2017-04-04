using System;
using OxyPlot;
using OxyPlot.Series;
using KozzionCore.Tools;
using System.Drawing;
using System.Collections.Generic;

namespace KozzionPlotting.Tools
{
    public class PlotLine2D
    {
        public PlotModel PlotModel { get; private set; }

        public PlotLine2D()
        { 
            this.PlotModel = new PlotModel();

            //this.PlotModel.Axes.Add(new LinearColorAxis { Position = AxisPosition.Right, Palette = OxyPalettes.Jet(200) });
        }

        public void AddSeries(IList<Tuple<double [], double[], Color>> series_list)
        {
            foreach (var series in series_list)
            {
                AddSeries(series.Item1, series.Item2, series.Item3);
            }
        }

        public void AddSeries(IList<Tuple<IList<double>, IList<double>, Color>> series_list)
        {   
            foreach (var series in series_list)
            {
               AddSeries(series.Item1.AsReadOnly(), series.Item2.AsReadOnly(), series.Item3);
            }
        }

        public void AddSeries(IList<float> x_values, IList<float> y_values)
        {
            AddSeries(ToolsCollection.ConvertToDoubleArray(x_values), ToolsCollection.ConvertToDoubleArray(y_values), Color.Black);
        }

        public void AddSeries(IList<double> x_values, IList<double> y_values)
        {
            AddSeries(x_values.AsReadOnly(), y_values.AsReadOnly(), Color.Black);
        }

        public void AddSeries(IReadOnlyList<double> x_values, IReadOnlyList<double> y_values, Color color)
        {
            if (x_values.Count != y_values.Count)
            {
                throw new Exception("Arrays must be of equal length");
            }

            LineSeries line_series = new LineSeries { MarkerType = MarkerType.None, Color = OxyColor.FromArgb(color.A, color.R, color.G, color.B) };
            for (int index = 0; index < x_values.Count; index++)
            {
                line_series.Points.Add(new DataPoint(x_values[index], y_values[index]));
            }
            this.PlotModel.Series.Add(line_series);
        }
    }
}
