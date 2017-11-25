using System;
using Microsoft.Office.Interop.Excel;
using System.IO;

using KozzionCore.IO.CSV;
using KozzionCore.Tools;
using KozzionMathematics.Function;
using KozzionMathematics.Algebra;

using KozzionMachineLearning.DataSet;
using KozzionMachineLearning.Reporting;
using KozzionMachineLearning.Method.JointTable;
using KozzionMachineLearning.Model;
using KozzionMachineLearning.FeatureSelection;
using KozzionMachineLearning.Method.NaiveBayes;
using KozzionMachineLearning.Method.SupportVectorMachine;
using KozzionMachineLearning.Method.NearestNeighbor;
using KozzionMachineLearning.Transform;
using KozzionMachineLearning.Clustering;
using KozzionMachineLearning.Clustering.KMeans;
using System.Drawing;
using KozzionGraphics.Tools;
using KozzionGraphics.Rendering.Raster;
using MathNet.Numerics.LinearAlgebra;
using KozzionGraphics.ColorFunction;
using KozzionCore.DataStructure.Science;
using MathNet.Numerics.IntegralTransforms;
using System.Numerics;
using System.Globalization;
using KozzionMathematics.Tools;
using KozzionGraphics.Renderer;
using System.Drawing.Imaging;
using KozzionGraphics.Image;
using KozzionGraphics.ElementTree.MaxTree;
using KozzionPlotting.Tools;

namespace KozzionMachineLearningCL
{
    public class Program0
    {

        static void Main(string[] args)
        {

        }

        static void Old0(string[] args)
        {

            // sample are 30 per second.
            string [,] table = ToolsIOCSV.ReadCSVFile(@"D:\Dropbox\TestData\ML6\data_0.csv",Delimiter.Comma, Delimiter.None);
            string [] row = table.Select1DIndex0(1);
            double[] signal = new double[row.Length];
            for (int element_index = 0; element_index < row.Length; element_index++)
            {
                signal[element_index] = double.Parse(row[element_index].Replace('.',','),NumberStyles.Any);
            }
            double[] speed = RunRoy1(signal, 128);
            ToolsPlotting.PlotLinesXY(@"D:\Dropbox\TestData\ML6\data_0.png", speed);
            Console.ReadLine();
        }

   

        private static double[] RunRoy1(double [] signal_real, int chop_size)
        {
            Complex [] signal = MakeComplex(signal_real);
            Complex[][] signals = ChopWindow(signal, chop_size);
            //ImageRaster3D<float> image = new ImageRaster3D<float>( signals.Length, chop_size, 1);
            double[] speed = new double[signals.Length];
            double[] FrequencyScale = Fourier.FrequencyScale(chop_size, 30);
            for (int singal_index = 0; singal_index < signals.Length; singal_index++)
            {
                Fourier.BluesteinForward(signals[singal_index], FourierOptions.Matlab);
                double[] magnitude = SelectMagnitude(signals[singal_index]);
                //for (int element_index = 0; element_index < magnitude.Length /2; element_index++)
                //{
                //    image.SetElementValue(singal_index, element_index, 0, (float)magnitude[element_index]);
                //}
                speed[singal_index] = magnitude[1] * FrequencyScale[1] + magnitude[2] * FrequencyScale[2] + magnitude[3] * FrequencyScale[3] + magnitude[4] * FrequencyScale[4];
                //Console.WriteLine(speed);
            }
            //Fourier.BluesteinForward(signal, FourierOptions.Default);
            //Console.WriteLine(ToolsCollection.ToString(FrequencyScale));
            return speed;
            //RendererImageRaster3DToBitmapZMIP<float> renderer_histogram = new RendererImageRaster3DToBitmapZMIP<float>(new FunctionFloat32ToColorASIST(0, 1), new ComparerNatural<float>());
            //renderer_histogram.Render(image).Save(@"D:\Dropbox\TestData\ML6\data_3.png", ImageFormat.Png);

        }

        private static Complex[][] ChopWindow(Complex[] signal, int window_length)
        {
            int window_count = signal.Length - window_length;
            Complex[][] signals = new Complex[window_count][];
            for (int window_index = 0; window_index < window_count; window_index++)
            {
                signals[window_index] = new Complex[window_length];
                for (int sample_index = 0; sample_index < window_length; sample_index++)
                {
                    signals[window_index][sample_index] = signal[window_index + sample_index];
                }
            }
            return signals;
        }

        private static Complex[] MakeComplex(double[] signal)
        {
            Complex[] complex_components = new Complex[signal.Length];
            for (int element_index = 0; element_index < signal.Length; element_index++)
            {
                complex_components[element_index] = new Complex(signal[element_index],0);
            }
            return complex_components;
        }

        private static double[] SelectReal(Complex[] signal)
        {
            double[] real_components = new double[signal.Length];
            for (int element_index = 0; element_index < signal.Length; element_index++)
            {
                real_components[element_index] = signal[element_index].Real;
            }
            return real_components;
        }

        private static double[] SelectMagnitude(Complex[] signal)
        {
            double[] real_components = new double[signal.Length];
            for (int element_index = 0; element_index < signal.Length; element_index++)
            {
                real_components[element_index] = signal[element_index].Magnitude;
            }
            return real_components;
        }
    }
}
