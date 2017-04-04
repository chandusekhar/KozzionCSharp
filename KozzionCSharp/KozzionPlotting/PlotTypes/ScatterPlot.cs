using System;
using OxyPlot;
using OxyPlot.Series;

namespace KozzionPlotting.PlotTypes
{
    public class ScatterPlot
    {
        public PlotModel PlotModel { get; private set; }

        public ScatterPlot(double[] x_values, double[] y_values)
        {
            if (x_values.Length != y_values.Length)
            {
                throw new Exception("Arrays must be of equal length");
            }

            this.PlotModel = new PlotModel { Title = "ScatterSeries" };
            ScatterSeries scatterSeries = new ScatterSeries { MarkerType = MarkerType.Circle };

            for (int index = 0; index < x_values.Length; index++)
            {
                ScatterPoint point = new ScatterPoint(x_values[index], y_values[index], 5 ,5, OxyColors.Black);
                scatterSeries.Points.Add(point);
            }
            this.PlotModel.Series.Add(scatterSeries);
            //this.PlotModel.Axes.Add(new LinearColorAxis { Position = AxisPosition.Right, Palette = OxyPalettes.Jet(200) });
        }
    }
}
