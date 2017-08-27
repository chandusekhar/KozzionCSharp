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


namespace KozzionMachineLearningCL
{
    class Program1
    {

        static void ExampleML4()
        {
            string file_path_ser = @"D:\Dropbox\TestData\ML4\brains_dataset.ser";
            IDataSetHybrid data_set_hybrid_unlabeled = null;
            if (File.Exists(file_path_ser))
            {
                try
                {
                    data_set_hybrid_unlabeled = DataSetHybrid.Read(new BinaryReader(File.Open(file_path_ser, FileMode.Open)));
                }
                catch (Exception)
                {
                    data_set_hybrid_unlabeled = null;
                }
            }

            if (data_set_hybrid_unlabeled == null)
            {
                IDataSet<int> data_set0 = ReadExcellNominal(@"D:\Dropbox\TestData\ML4\brains_ml.xlsx");
                data_set_hybrid_unlabeled = CleanUpML4(data_set0);
                ToolsIOSerialization.SerializeToFile(file_path_ser, data_set_hybrid_unlabeled);
            }

            IDataSet < int> data_set_nominal_unlabeled = data_set_hybrid_unlabeled.ConvertToNominal(5);
            IDataSet<int, int> data_set_nominal_labeled = data_set_nominal_unlabeled.PromoteFeatureToLabel(0);
            Tuple<IDataSet<int, int>, IDataSet<int, int>> split = data_set_nominal_labeled.Split();
            IDataSet<int, int> training_set = split.Item1;
            IDataSet<int, int> test_set = split.Item2;
            ProccessDataSetNominalJoinTable(training_set, test_set);
        }


        static void ExampleML5()
        {
            IDataSet<int> data_set0 = ReadCSVNominal(@"D:\Dropbox\TestData\ML5\soybean-large.data");
            IDataSet<int, int> data_set_labeled = data_set0.PromoteFeatureToLabel(0);
            Tuple<IDataSet<int, int>, IDataSet<int, int>> split = data_set_labeled.Split(0.8);
            IDataSet<int, int> training_set = split.Item1;
            IDataSet<int, int> test_set = split.Item2;
            Console.WriteLine(training_set);
            Console.WriteLine(training_set);
            //ProccessDataSetNominalJoinTable(training_set, test_set);
            //ProccessDataSetNominalNaiveBayes(training_set, test_set);
            //ProccessDataSetNominalNearestNeighbor(training_set, test_set);
            ProccessDataSetNominalPCA(training_set, test_set);
            //ProccessDataSetNominalSupportVectorMachine(training_set, test_set);
        }



        private static IDataSet<int> ReadCSVNominal(string file_path)
        {
            return new DataSet<int,int>(ToolsIOCSV.ReadCSVFile(file_path, Delimiter.Comma, Delimiter.None));
        }

        public static IDataSet<int> ReadExcellNominal(string file_path)
        {
            Application excell_application = new Application();
            Workbook work_book = excell_application.Workbooks.Open(file_path, 0, true, 5, "", "", true, XlPlatform.xlWindows, "\t", false, false, 0, true, 1, 0);
            Worksheet work_sheet = (Worksheet)work_book.Worksheets.get_Item(1);
            Range range = work_sheet.UsedRange;
            string[,] table = new string[range.Rows.Count, range.Columns.Count];
            object[,] values = (object[,])range.Value2;
            for (int row_index = 0; row_index < values.GetLength(0); row_index++)
            {
                for (int column_index = 0; column_index < values.GetLength(1); column_index++)
                {
                    table[row_index, column_index] = Convert.ToString(values[row_index + 1, column_index + 1]);
                }
            }

            work_book.Close(true, null, null);
            excell_application.Quit();
            return new DataSet<int, int>(table);
        }


        public static IDataSetHybrid CleanUpML4(IDataSet<int> data_set0)
        {
            // Manipulate it
            Console.WriteLine("For feature index: " + 0 + " Adding value type as missing: " + "9");

            Console.WriteLine("Removing all instances missing: " + data_set0.DataContext.GetFeatureName(0));
            IDataSet<int> data_set1 = data_set0.RemoveInstancesMissing(0);

            Console.WriteLine("Removing all instances missing: " + data_set0.DataContext.GetFeatureName(1));
            IDataSet<int> data_set2 = data_set1.RemoveInstancesMissing(1);


            int[] remove_feature_indexes = new int[] { 1, 2, 3, 5, 7, 8, 9, 11, 12 };
            foreach (int remove_feature_index in remove_feature_indexes)
            {
                Console.WriteLine("Removing: " + data_set2.DataContext.GetFeatureName(remove_feature_index));
            }
            IDataSet<int> data_set3 = data_set2.RemoveFeatures(remove_feature_indexes);

            int[] interval_feature_indexes = new int[] { 1, 2, 3 };
            foreach (int interval_feature_index in interval_feature_indexes)
            {
                Console.WriteLine("Promoting to hybrid: " + data_set0.DataContext.GetFeatureName(interval_feature_index));
            }
            IDataSetHybrid data_set4 = data_set3.PromoteFeatureLevelToInterval(interval_feature_indexes);
            return data_set4;
        }

        private static void ProccessDataSetNominalJoinTable(IDataSet<int, int> training_set, IDataSet<int, int> test_set)
        {
            ITemplateModelDiscrete<int, int> template = new TemplateModelFeatureSelecting<int, int>(
                    new FeatureSelectorGreedy<int, int>(
                    new EvaluatorReapetedRandomFold<int, int>(100)),
                    new TemplateModelJointTable());

            ReportDiscrete<int, int> report = template.GenerateAndTestDiscrete(training_set, test_set);

            Console.WriteLine(ToolsCollection.ToString(report.ConfusionMatrixInstances));
            Console.WriteLine(report.CorrectLabelRate);

        }

        private static void ProccessDataSetNominalNaiveBayes(IDataSet<int, int> training_set, IDataSet<int, int> test_set)
        {
            ITemplateModelDiscrete<int, int> template = new TemplateModelFeatureSelecting<int, int>(
                   new FeatureSelectorGreedy<int, int>(
                   new EvaluatorReapetedRandomFold<int, int>(100)),
               new TemplateModelNaiveBayesNominal());

            ReportDiscrete<int, int> report = template.GenerateAndTestDiscrete(training_set, test_set);

            Console.WriteLine(ToolsCollection.ToString(report.ConfusionMatrixInstances));
            Console.WriteLine(report.CorrectLabelRate);
        }

        private static void ProccessDataSetNominalNearestNeighbor(IDataSet<int, int> training_set, IDataSet<int, int> test_set)
        {

            ITemplateModelDiscrete<int, int> template = new TemplateModelFeatureSelecting<int, int>(
                   new FeatureSelectorGreedy<int, int>(
                   new EvaluatorReapetedRandomFold<int, int>(10)),
               new TemplateModelNearestNeighborNominal());
            ReportDiscrete<int, int> report = template.GenerateAndTestDiscrete(training_set, test_set);

            Console.WriteLine(ToolsCollection.ToString(report.ConfusionMatrixInstances));
            Console.WriteLine(report.CorrectLabelRate);
        }

        private static void ProccessDataSetNominalPCA(IDataSet<int, int> training_set, IDataSet<int, int> test_set)
        {
            int cluster_count = 10;
            int instance_count = training_set.InstanceCount;
            ITemplateClusteringCentroid<int, IDataSet<int>> template_cluster = new TemplateClusteringKMeansNominal(cluster_count);
            TemplateDimensionReductionPCADefault template_pca = new TemplateDimensionReductionPCADefault(3);
            IDataSet<int> training_set_unlabeled = training_set;


            IClusteringCentroid<int> clustering = template_cluster.Cluster(training_set_unlabeled);

            double[][] transformed_data1 = new double[instance_count][];
            for (int instance_index = 0; instance_index < instance_count; instance_index++)
            {
                transformed_data1[instance_index] = clustering.Transform(training_set_unlabeled.GetInstanceFeatureData(instance_index));
            }


            IDataSet<double> data_set_transformed1 = new DataSet<double,int>(transformed_data1);


            ITransform<double[], double[]> transform = template_pca.GenerateTransform(data_set_transformed1);
            double[][] transformed_data2 = new double[instance_count][];
            int[][] label_data = training_set.LabelData;
            Color[] instance_colors = new Color[instance_count];
            Color[] label_colors = ToolsColor.GetPallete(training_set.DataContext.GetLabelDescriptor(0).ValueCount);

            for (int instance_index = 0; instance_index < instance_count; instance_index++)
            {
                transformed_data2[instance_index] = transform.Compute(data_set_transformed1.GetInstanceFeatureData(instance_index));
                instance_colors[instance_index] = label_colors[label_data[instance_index][0]];
            }
            RendererPoints<Matrix<double>> renderer = new RendererPoints<Matrix<double>>(new AlgebraLinearReal64MathNet(), 1000, 1000, (AngleRadian)0, (AngleRadian)0, 10);
            BitmapFast bitmap = renderer.Render(new Tuple<double[][], Color[]>(transformed_data2, instance_colors));
            bitmap.Bitmap.Save("test.png");
        }


        private static void ProccessDataSetNominalSupportVectorMachine(IDataSet<int, int> training_set, IDataSet<int, int> test_set)
        {
            //ITemplateModelInterval<int, IModelLikelyHood<double[], int, double>>
            IDataSet<double, int> training_set_interval = training_set.ConvertToDataSetInterval();
            IDataSet<double, int> test_set_interval = test_set.ConvertToDataSetInterval();
            //ITemplateModelDiscrete<IDataSetIntervalLabeled<double,int>, IModelLikelyHood<double[], int, double>> template = 
            //    new TemplateModelFeatureSelecting<double, int, IDataSetIntervalLabeled<double, int>>(
            //        new FeatureSelectorGreedy<double, int, IDataSetIntervalLabeled<double,int>>(
            //        new EvaluatorReapetedRandomFold<double, int, IDataSetIntervalLabeled<double, int>>(10)),
            //        new TemplateModelLibSVMCSVC());

            ITemplateModelDiscrete<double, int> template = new TemplateModelLibSVMCSVC(100, 5);

            ReportDiscrete<double, int> report = template.GenerateAndTestDiscrete(training_set_interval, test_set_interval);

            Console.WriteLine(ToolsCollection.ToString(report.ConfusionMatrixInstances));
            Console.WriteLine(report.CorrectLabelRate);
        }









        private static void ProccessDataSetNominalCluster3D(IDataSet<int, int> training_set, IDataSet<int, int> test_set)
        {
            //ITemplateModelDiscrete<int, int, IDataSetNominalLabeled<int>> template = new TemplateModelFeatureSelecting<int, int, IDataSetNominalLabeled<int>>(



        }




        public static void ProccessDataSet(IDataSet<int, int> data_set)
        {
            //DataSetHybridFloat32 data_set = ReadSer();
            IDataContext data_context = data_set.DataContext;

            // 1.  Prediction Classical : 
            // 1.1 Check single feature predictors for label
            for (int feature_0_index = 0; feature_0_index < data_context.FeatureCount; feature_0_index++)
            {
                VariableDescriptor feature_descriptor = data_context.GetFeatureDescriptor(feature_0_index);
                //data_set_labeled.GetFeatureData(feature_0_index);
            }
            // 1.2 Check double feature predictors for label

            // 1.3 split the data in a test and training set
            Tuple<IDataSet<int, int>, IDataSet<int, int>> split = data_set.Split();
            IDataSet<int, int> training_set = split.Item1;
            IDataSet<int, int> test_set = split.Item2;
            // 1.4 Check greedy joint predictors for label



            // 2.  Prediction Advanced 
            // 2.1 Naive bayes (bag of words like)  Report ROC, Best Feature space


            // 2.2 For each level build random forests                  Report ROC, Best Feature space
            //ROCReport random_forest_report = BuildAndEvaluate();
            // 3.  Clustering Classical
            // 3.1 Correlation clustering shortest link, complete link. Report cluster typing at point

            // 4.  Clustering Advance
            // 4.1 Correlation clustering shortest link, complete link. Report cluster typing at point
        }

    }
}
