using System;
using System.Collections.Generic;
using KozzionCore.Tools;
using KozzionMathematics.Tools;
using System.IO;
using KozzionCore.IO;
using System.Text;

namespace KozzionMachineLearning.DataSet
{
    public class DataSet<FeatureType, LabelType> : IDataSet<FeatureType, LabelType>
	{
        public int InstanceCount { get { return feature_data.Length; } }
        public int FeatureCount { get { return DataContext.FeatureCount; } }
        public int LabelCount { get { return DataContext.LabelCount; } }
        public IDataContext DataContext {get; private set;}

        private FeatureType[][] feature_data;
        public FeatureType[][]  FeatureData { get { return ToolsCollection.Copy(feature_data); } }
        private bool[] [] missing_data;
        public bool[][] MissingData { get { return ToolsCollection.Copy(missing_data); } }
        private LabelType[][] label_data;
        private IDataContext data_context_labeled;
        private double[][] feature_data1;
        private int[][] label_data1;

        public LabelType[][] LabelData { get { return ToolsCollection.Copy(label_data); } }   

        public LabelType[] GetInstanceLabelData(int instance_index)
        {
            return label_data[instance_index];
        }

        public bool[] GetInstanceMissingData(int instance_index)
        {
            return missing_data[instance_index];
        }

        //Dummy Constructor
        public DataSet()
        {
            this.DataContext = new DataContext();
            this.feature_data = new FeatureType[0][];
            this.missing_data = new bool[0][];
            this.label_data = new LabelType[0][];
        }

        //Main Contructor
        public DataSet(
            IDataContext data_context,
            IList<FeatureType[]> feature_data,
            IList<bool[]> missing_data,
            IList<LabelType[]> label_data)
        {

            if (data_context == null)
            {
                throw new NullReferenceException();
            }
            if (feature_data == null)
            {
                throw new NullReferenceException();
            }
            if (missing_data == null)
            {
                throw new NullReferenceException();
            }
            if (label_data == null)
            {
                throw new NullReferenceException();
            }

            if (ToolsCollection.IsStaggered(feature_data))
            {
                throw new Exception("Does not accept staggered feature data");
            }

            if (ToolsCollection.IsStaggered(missing_data))
            {
                throw new Exception("Does not accept staggered missing data");
            }
            //TODO check that feature_data is of equal size as missing data
            if (ToolsCollection.IsStaggered(label_data))
            {
                throw new Exception("Does not accept staggered label data");
            }

            if (!ToolsCollection.AreEqualSize(feature_data, missing_data))
            {
                throw new Exception("Must have missing data for every feature");
            }

            if (feature_data.Count != label_data.Count)
            {
                throw new Exception("Feature and label instances must be equal");
            }

            this.DataContext = data_context;
            this.feature_data = ToolsCollection.ConvertToArrayArray(feature_data);
            this.missing_data = ToolsCollection.ConvertToArrayArray(missing_data);
            this.label_data = ToolsCollection.ConvertToArrayArray(label_data);
        }



        public DataSet(List<IDataSet<double, int>> data_set_list)
        {
            throw new NotImplementedException();
        }

        public DataSet(IDataContext data_context_labeled, double[][] feature_data1, int[][] label_data1)
        {
            this.data_context_labeled = data_context_labeled;
            this.feature_data1 = feature_data1;
            this.label_data1 = label_data1;
        }
    

        public DataSet(string[,] table)
        {
            //NOMINAl data thing
            throw new NotImplementedException();
        }

        public DataSet(IList<double[]> table)
        {
            //INTERVAL data thing
            throw new NotImplementedException();
        }


        public int GetMissingCount(int feature_index) 
        {
            int missing = 0;
            for (int instance_index = 0; instance_index < InstanceCount; instance_index++)
            {
                if(missing_data[instance_index][feature_index])
                {
                    missing++;
                }
            }
            return missing;
        }

        public List<int> GetInstanceIndexesMissing(int feature_index)
        {
            List<int> missing = new List<int>();
            for (int instance_index = 0; instance_index < InstanceCount; instance_index++)
            {
                if (missing_data[instance_index][feature_index])
                {
                    missing.Add(instance_index);
                }
            }
            return missing;
        }

        public List<int> GetInstanceIndexesNotMissing(int feature_index)
        {
            List<int> not_missing = new List<int>();
            for (int instance_index = 0; instance_index < InstanceCount; instance_index++)
            {
                if (!missing_data[instance_index][feature_index])
                {
                    not_missing.Add(instance_index);
                }
            }
            return not_missing;
        }


        protected IList<FeatureType[]> SelectFeatureData(IList<int> instance_indexes, IList<int> feature_indexes)
        {
            return ToolsCollection.SelectRowsAndColumns(feature_data, instance_indexes, feature_indexes);
        }

        protected IList<bool[]> SelectMissingData(IList<int> instance_indexes, IList<int> feature_indexes)
        {
            return ToolsCollection.SelectRowsAndColumns(missing_data, instance_indexes, feature_indexes);
        }

        public FeatureType[] GetInstanceFeatureData(int instance_index)
        {
            return feature_data[instance_index];
        }

        public IDataSet<FeatureType, LabelType> RemoveInstancesMissing(int feature_index)
        {
            List<int> not_missing = GetInstanceIndexesNotMissing(feature_index);
            return SelectInstances(not_missing.ToArray());
        }

        public IDataSet<FeatureType, LabelType> SelectInstances(IList<int> selected_instance_indexes)
        {
            return SelectInstancesFeaturesLabels(selected_instance_indexes, ToolsMathSeries.RangeInt32(FeatureCount), ToolsMathSeries.RangeInt32(LabelCount));
        }

        public IDataSet<FeatureType, LabelType> SelectFeatures(IList<int> selected_features_indexes)
        {
            return SelectInstancesFeaturesLabels(ToolsMathSeries.RangeInt32(InstanceCount), selected_features_indexes, ToolsMathSeries.RangeInt32(LabelCount));
        }

        public IDataSet<FeatureType, LabelType> SelectLabels(IList<int> selected_label_indexes)
        {
            return SelectInstancesFeaturesLabels(ToolsMathSeries.RangeInt32(InstanceCount), ToolsMathSeries.RangeInt32(FeatureCount), selected_label_indexes);
        }

        public IDataSet<FeatureType, LabelType> RemoveFeatures(IList<int> remove_feature_indexes)
        {
            HashSet<int> remove = new HashSet<int>(remove_feature_indexes);
            List<int> retain = new List<int>();
            for (int feature_index = 0; feature_index < FeatureCount; feature_index++)
            {
                if (!remove.Contains(feature_index))
                {
                    retain.Add(feature_index);
                }
            }
            return SelectFeatures(retain.ToArray());
        }

        public Tuple<IDataSet<FeatureType, LabelType>, IDataSet<FeatureType, LabelType>> Split()
        {
            return Split(0.5);
        }


        public Tuple<IDataSet<FeatureType, LabelType>, IDataSet<FeatureType, LabelType>> Split(double fraction)
        {
            List<int> instances = new List<int>(ToolsMathSeries.RangeInt32(InstanceCount));
            ToolsMathCollection.ShuffleIP(instances);
            int set_0_size = (int)(instances.Count * fraction);
            List<int> selected_instance_indexes_0 = ToolsCollection.Crop(instances, 0, set_0_size);
            List<int> selected_instance_indexes_1 = ToolsCollection.Crop(instances, set_0_size, instances.Count);
            IDataSet<FeatureType, LabelType> data_set_0 = SelectInstances(selected_instance_indexes_0);
            IDataSet<FeatureType, LabelType> data_set_1 = SelectInstances(selected_instance_indexes_1);
            return new Tuple<IDataSet<FeatureType, LabelType>, IDataSet<FeatureType, LabelType>>(data_set_0, data_set_1);
        }
          
        public static DataSet<FeatureType, LabelType> Read<FeatureType2, LabelType2>(BinaryReader reader, IBinaryReader<FeatureType> reader_features, IBinaryReader<LabelType> reader_labels)
        {
            DataContext data_context = DataSet.DataContext.Read(reader);
            IList <FeatureType[]> feature_data = ToolsCollection.ConvertToArrayArray(reader_features.ReadArray2D());
            IList<bool []> missing_data = ToolsCollection.ConvertToArrayArray(reader.ReadBooleanArray2D());
            IList<LabelType[]> label_data = ToolsCollection.ConvertToArrayArray(reader_labels.ReadArray2D());
            return new DataSet<FeatureType, LabelType>(data_context, feature_data, missing_data, label_data);
        }

        public void Write(BinaryWriter writer, IBinaryWriter<FeatureType> writer_features, IBinaryWriter<LabelType> writer_labels)
        {
            this.DataContext.Write(writer);
            writer_features.Write(ToolsCollection.ConvertToArray2D(this.feature_data));
            writer.Write(ToolsCollection.ConvertToArray2D(this.missing_data));
            writer_labels.Write(ToolsCollection.ConvertToArray2D(this.label_data));
        }

        public LabelType[] GetLabelDataColumn(int label_index)
        {           
            return ToolsCollection.SelectColumn(label_data, label_index);
        }

        public IDataSet<FeatureType, LabelType> SelectInstancesFeaturesLabels(IList<int> selected_instance_indexes, IList<int> selected_feature_indexes, IList<int> selected_label_indexes)
        {
            throw new NotImplementedException();
        }

        public IDataSet<double, int> ConvertToDataSetInterval()
        {
            throw new NotImplementedException();
        }

        IDataSet<FeatureType> IDataSet<FeatureType>.RemoveFeatures(IList<int> feature_indexes)
        {
            throw new NotImplementedException();
        }

        IDataSet<FeatureType> IDataSet<FeatureType>.RemoveInstancesMissing(int feature_index)
        {
            throw new NotImplementedException();
        }

  

        IDataSet<FeatureType, FeatureType> IDataSet<FeatureType>.PromoteFeatureToLabel(int feature_index)
        {
            throw new NotImplementedException();
        }


        public IDataSet<FeatureType, FeatureType> PromoteFeatureToLabel(int feature_index)
        {
            throw new NotImplementedException();
        }


        public IDataSetHybrid PromoteFeatureLevelToInterval(int[] interval_feature_indexes)
        {
            throw new NotImplementedException();
        }


        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(DataContext.ToString());
            builder.Append(ToolsCollection.ToStringArray(feature_data));
            builder.Append(ToolsCollection.ToStringArray(missing_data));
            builder.Append(ToolsCollection.ToStringArray(label_data));
            return builder.ToString();
        }

    }
}