using System;
using KozzionMachineLearning.Voting;
using KozzionMathematics.Algebra;
using KozzionMathematics.Datastructure.k_d_tree;
using KozzionMathematics.Function.Implementation.Distance;
using KozzionMachineLearning.DataSet;
using KozzionMachineLearning.Model;
using System.IO;
using KozzionCore.IO;
using System.Collections.Generic;
using KozzionCore.Tools;

namespace KozzionMachineLearning.Method.NearestNeighbor
{
    //This is the guy you generally want to use
    public class ModelNearestNeighborListDefault : ModelNearestNeighborList<double, double, int>
    {
        public ModelNearestNeighborListDefault(IDataContext data_context) 
            : base(data_context, new FunctionDistanceEuclidean())
    	{
        }

        public static IModelDiscrete<double, int> Read(BinaryReader reader)
        {
            IDataContext data_context = DataSet.DataContext.Read(reader);  
            ModelNearestNeighborListDefault model = new ModelNearestNeighborListDefault(data_context);

            double[][] instance_features = ToolsCollection.ConvertToArrayArray(reader.ReadFloat64Array2D());
            bool[][] instance_missings = ToolsCollection.ConvertToArrayArray(new bool[instance_features.GetLength(0), instance_features.GetLength(1)]);
            int[][] instance_labels = ToolsCollection.ConvertToArrayArray(reader.ReadInt32Array2D());
            IDataSet<double, int> training_set = new DataSet<double, int>(data_context, instance_features, instance_missings, instance_labels);
            return model.GenerateModelDiscrete(training_set);
        }

        public static void Write(BinaryWriter writer, ModelNearestNeighborListDefault model)
        {
            model.DataContext.Write(writer);
            IList<Tuple<double[], int>> list = model.list;
            double[,] instance_features = new double[list.Count, model.DataContext.FeatureCount];
            int[] instance_labels = new int[list.Count];
            for (int instance_index = 0; instance_index < list.Count; instance_index++)
            {
                instance_features.Set1DIndex1(instance_index, list[instance_index].Item1);
                instance_labels[instance_index] = list[instance_index].Item2;
            }
            writer.Write(instance_features);
            writer.Write(instance_labels);
        }
    }
}