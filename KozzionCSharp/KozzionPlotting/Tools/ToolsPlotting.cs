using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Drawing;
using System.Windows;
using KozzionMathematics.Tools;
using KozzionPlotting.PlotTypes;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

//using System.Windows.Controls;


namespace KozzionPlotting.Tools
{
    public class ToolsPlotting
    {
        //private class PlotShower
        //{
        //    PlotModel plot_model;

        //    public PlotShower(PlotModel plot_model)
        //    {
        //        this.plot_model = plot_model;
        //    }

        //    public void ShowPlotInWindowTread()
        //    {

        //        //Canvas canvas = new Canvas();
        //        //canvas.Width = 800;
        //        //canvas.Height = 800;
        //        //canvas.Background = System.Windows.Media.Brushes.Red;

    
        //        Window window = new Window();
        //        window.AllowsTransparency = false;
        //        window.WindowStyle = WindowStyle.ThreeDBorderWindow;
        //        window.Background = System.Windows.Media.Brushes.White;
        //        window.Topmost = true;
        //        window.Width = 800;
        //        window.Height = 800;
        //        window.ResizeMode = ResizeMode.CanResize;

        //        var plotModel = new PlotModel { Title = "OxyPlot Demo" };

        //        plotModel.Axes.Add(new OxyPlot.Axes.LinearAxis { Position = AxisPosition.Bottom });
        //        plotModel.Axes.Add(new OxyPlot.Axes.LinearAxis { Position = AxisPosition.Left, Maximum = 10, Minimum = 0 });

        //        var series1 = new OxyPlot.Wpf.LineSeries
        //        {
        //            MarkerType = MarkerType.Circle,
        //            MarkerSize = 4,
        //            MarkerStroke = System.Windows.Media.Color.FromRgb(50, 50, 50)
        //        };
           
        //        //series1.Points.Add(new DataPoint(0.0, 6.0));
        //        //series1.Points.Add(new DataPoint(1.4, 2.1));
        //        //series1.Points.Add(new DataPoint(2.0, 4.2));
        //        //series1.Points.Add(new DataPoint(3.3, 2.3));
        //        //series1.Points.Add(new DataPoint(4.7, 7.4));
        //        //series1.Points.Add(new DataPoint(6.0, 6.2));
        //        //series1.Points.Add(new DataPoint(8.9, 8.9));

        //        //plotModel.Series.Add(series1);


        //        PlotView plot_view = new PlotView();
        //        plot_view.Background = System.Windows.Media.Brushes.Blue;
        //        plot_view.DataContext = plotModel;
        //        plot_view.ToolTip = "oops";
               
        //         //plot_view.Title = "Testplotview";


        //         window.Content = plot_view;  
        //        window.Show();
        //        System.Windows.Threading.Dispatcher.Run();

        //    }
        //}

        //public static void ShowPlotInWindow(PlotLine2D line_plot)
        //{
        //    ShowPlotInWindow(line_plot.PlotModel);
        //}


        //private static void ShowPlotInWindow(PlotModel plot_model)
        //{
        //    PlotShower shower = new PlotShower(plot_model);

        //    Thread tread = new Thread(shower.ShowPlotInWindowTread);
        //    tread.SetApartmentState(ApartmentState.STA);
        //    tread.Start();
        //}

       
        public static void PlotHistogram(string file_path)
        {
            PlotModel model = new HistrogramPlot().PlotModel;
            WriteToFile(file_path, model, 800, 800);
        }

        public static void PlotHistogram(string file_path, double[] values, int bincount, double lower_quantile, double upper_quantile)
        {
            Array.Sort(values);
            double lowerbound = ToolsMathStatistics.QuantileSorted(values, lower_quantile);
            double upperbound = ToolsMathStatistics.QuantileSorted(values, upper_quantile);
            double[] selected_values = ToolsMathCollection.Select(values, lowerbound, upperbound);
            double stride = (upperbound - lowerbound)/ bincount;
            double[] bin_limits = new double[bincount - 1];
            bin_limits[0] = lowerbound + stride;
            for (int bin_limit_index = 1; bin_limit_index < bin_limits.Length; bin_limit_index++)
            {
                bin_limits[bin_limit_index] = bin_limits[bin_limit_index - 1] + stride;
            }

            PlotModel model = new HistrogramPlot(selected_values, bin_limits).PlotModel;
            WriteToFile(file_path, model, 800, 800);
        }

        public static LineSeries CreateSeriesLine(string series_title, IList<double> domain, IList<double> range)
        {

            LineSeries line_series = new LineSeries();
            line_series.Title = series_title;
            for (int index_time = 0; index_time < domain.Count; index_time++)
            {
                line_series.Points.Add(new DataPoint(domain[index_time], range[index_time]));
            }
            return line_series;
        }

        public static ScatterSeries CreateSeriesPoint(string series_title, IList<double> domain, IList<double> range, double size = double.NaN)
        {
            ScatterSeries line_series = new ScatterSeries();
            line_series.Title = series_title;
            for (int index_time = 0; index_time < domain.Count; index_time++)
            {
                line_series.Points.Add(new ScatterPoint(domain[index_time], range[index_time], size));
            }
            return line_series;
        }

        public static void PlotHistogram(string file_path, double[] values, int bincount)
        {
            PlotHistogram(file_path, values, bincount, 0.01f, 0.99f);
        }

        public static void PlotScatter(string file_path, double[] x_values, double[] y_values, int size_x, int size_y)
        {
            PlotModel model = new ScatterPlot(x_values, y_values).PlotModel;
            WriteToFile(file_path, model, size_x, size_y);
        }


        public static void PlotLinesXY(string file_path, double[] x_values, double[] y_values_0)
        {
            PlotLine2D plot = new PlotLine2D();
            plot.AddSeries(x_values, y_values_0);
            WriteToFile(file_path, plot.PlotModel, 800, 800);
        }

        public static void PlotLinesXY(string file_path, int size_x, int size_y, IList<Tuple<double[], double[], Color>> series_list)
        {
            PlotLine2D plot = new PlotLine2D();
            AddLines2D(plot, series_list);
            WriteToFile(file_path, plot.PlotModel, 800, 800);
        }


        public static void AddLines2D(PlotLine2D plot, IList<Tuple<double[], double[], Color>> series_list)
        { 
            foreach (var series in series_list)
            {        
                plot.AddSeries(series.Item1, series.Item2, series.Item3);
            }
        }

        public static void PlotLinesXY(string file_path, double[] y_values_0)
        {
            PlotLine2D plot = new PlotLine2D();
            double[] x_values = ToolsMathSeries.RangeFloat64(y_values_0.Length);
            plot.AddSeries(x_values, y_values_0);
            WriteToFile(file_path, plot.PlotModel, 800, 800);
        }

        public static void PlotLinesXY(string file_path, List<double[]> y_values_set)
        {
            PlotLine2D plot = new PlotLine2D();
            foreach (double[] y_values in y_values_set)
            {
                double[] x_values = ToolsMathSeries.RangeFloat64(y_values.Length);
                plot.AddSeries(x_values, y_values);
            }
            WriteToFile(file_path, plot.PlotModel, 800, 800);
        }

        public static void PlotLinesXY(string file_path, double[] x_values, double[] y_values_0, double[] y_values_1)
        {
            PlotLine2D plot = new PlotLine2D();
            plot.AddSeries(x_values, y_values_0);
            plot.AddSeries(x_values, y_values_1);
            WriteToFile(file_path, plot.PlotModel, 800, 800);
        }

        public static void WriteToFile(string file_path, PlotLine2D line_plot, int size_x, int size_y)
        {
            WriteToFile(file_path, line_plot.PlotModel, size_x, size_y);
        }

        private static void WriteToFile(string file_path, PlotModel model, int size_x, int size_y)
        {
            Thread thread = new Thread(() =>
            {
                Directory.CreateDirectory(Path.GetDirectoryName(file_path));
                using (var stream = File.Create(file_path))
                {
                    OxyPlot.Wpf.PngExporter.Export(model, stream, size_x, size_y, OxyColor.FromRgb(255, 255, 255), 96);
                }
            });

            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
        }

        public static PlotModel CreatePlot(string title, IEnumerable<Series> series_list, bool show_ledgend)
        {
            PlotModel plot_model = new PlotModel();
            plot_model.Subtitle = title;
            //LinearAxis linearAxis1 = new LinearAxis();
            //linearAxis1.MajorStep = 0.2;
            //linearAxis1.Maximum = 1.05;
            //linearAxis1.Minimum = -0.05;
            //linearAxis1.MinorStep = 0.05;
            //linearAxis1.TickStyle = TickStyle.Inside;
            //plotModel1.Axes.Add(linearAxis1);
            //LinearAxis linearAxis2 = new LinearAxis();
            //linearAxis2.MajorStep = 1;
            //linearAxis2.Maximum = 5.25;
            //linearAxis2.Minimum = -5.25;
            //linearAxis2.MinorStep = 0.25;
            //linearAxis2.Position = AxisPosition.Bottom;
            //linearAxis2.TickStyle = TickStyle.Inside;
            //plotModel1.Axes.Add(linearAxis2);

            foreach (Series series in series_list)
            { 
                plot_model.Series.Add(series);
            }
            return plot_model;
        }
    }
}
