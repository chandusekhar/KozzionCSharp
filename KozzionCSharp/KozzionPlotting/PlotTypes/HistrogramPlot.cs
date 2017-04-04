using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace KozzionPlotting.PlotTypes
{
    public class HistrogramPlot
    {
        public PlotModel PlotModel { get; private set; }

        public HistrogramPlot()
        {
            this.PlotModel = new PlotModel { Title = "BarSeries" };
            BarSeries bar_series = new BarSeries { BarWidth = 1 };


//#pragma warning disable CS0612 // Type or member is obsolete
//            this.PlotModel.Axes.Add(new CategoryAxis(AxisPosition.Left, "blieb", new string [] {"blieb","blieb"}));
//#pragma warning restore CS0612 // Type or member is obsolete
//            this.PlotModel.Axes.Add(new LinearAxis(AxisPosition.Left) { MinimumPadding = 0, AbsoluteMinimum = 0 });

            bar_series.Items.Add(new BarItem(0.5));
            bar_series.Items.Add(new BarItem(0.5));

            this.PlotModel.Series.Add(bar_series);
            //this.PlotModel.Axes.Add(new LinearColorAxis { Position = AxisPosition.Right, Palette = OxyPalettes.Jet(200) });
        }

        public HistrogramPlot(double[] values_sorted, double[] bin_limits)
        {
            double[] bin_fillings = new double[bin_limits.Length + 1];
            double[] bin_centres = new double[bin_limits.Length + 1];

            bin_centres[0] = (values_sorted[0] + bin_limits[0]) / 2.0f;
            bin_centres[bin_centres.Length - 1] = (values_sorted[values_sorted.Length - 1] + bin_limits[bin_limits.Length - 1]) / 2.0f;

            int bin_limit_index = 0;
       
            for (int index = 0; index < values_sorted.Length; index++)
            {
                double value = values_sorted[index];
                while ((bin_limit_index < bin_limits.Length - 1) && (bin_limits[bin_limit_index] < value))
                {
            
                    bin_centres[bin_limit_index + 1] = (bin_limits[bin_limit_index + 1] + bin_limits[bin_limit_index]) / 2.0f;
                    bin_limit_index++;
                }

                bin_fillings[bin_limit_index]++;

            }


            this.PlotModel = new PlotModel { Title = "ScatterSeries" };
            LineSeries line_series = new LineSeries { MarkerType = MarkerType.None };
            for (int index = 0; index < bin_fillings.Length; index++)
            {
                line_series.Points.Add(new DataPoint(bin_centres[index], bin_fillings[index]));
            }
            this.PlotModel.Series.Add(line_series);
            //this.PlotModel.Axes.Add(new LinearColorAxis { Position = AxisPosition.Right, Palette = OxyPalettes.Jet(200) });
        }
    }
}
