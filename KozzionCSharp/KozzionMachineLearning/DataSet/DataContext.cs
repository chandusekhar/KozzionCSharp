using KozzionCore.Tools;
using KozzionMathematics.Tools;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace KozzionMachineLearning.DataSet
{
	public class DataContext : IDataContext
    {
        public int FeatureCount { get { return feature_descriptors.Count; } }
        public int LabelCount { get { return label_descriptors.Count; } }

        private IList<VariableDescriptor> feature_descriptors;
        public IList<VariableDescriptor> FeatureDescriptors { get{ return new List<VariableDescriptor>(feature_descriptors); } }
        private IList<VariableDescriptor> label_descriptors;
        public IList<VariableDescriptor> LabelDescriptors { get { return new List<VariableDescriptor>(label_descriptors); } }

        public IList<string> FeatureNames
        {
            get
            {
                IList<string> feature_names = new List<string>();
                foreach (VariableDescriptor feature_descriptor in FeatureDescriptors)
                {
                    feature_names.Add(feature_descriptor.Name);
                }
                return feature_names;
            }
        }   
        public IList<string> LabelNames
        {
            get
            {
                IList<string> label_names = new List<string>();
                foreach (VariableDescriptor feature_descriptor in FeatureDescriptors)
                {
                    label_names.Add(feature_descriptor.Name);
                }
                return label_names;
            }
        }

        private Dictionary<string, int> feature_name_to_feature_index;
        private Dictionary<string, int> label_name_to_label_index;


        public DataContext(IList<VariableDescriptor> feature_descriptors, IList<VariableDescriptor> label_descriptors)
		{
            this.feature_descriptors = new List<VariableDescriptor>(feature_descriptors);
            this.label_descriptors = new List<VariableDescriptor>(label_descriptors);

            feature_name_to_feature_index = new Dictionary<string, int>();
            for (int feature_index = 0; feature_index < feature_descriptors.Count; feature_index++)
            {
                string feature_name = feature_descriptors[feature_index].Name;
                if (feature_name_to_feature_index.ContainsKey(feature_name))
                {
                    throw new Exception("Duplciate feature: " + feature_name);
                }
                this.feature_name_to_feature_index[feature_name] = feature_index;
            }

            label_name_to_label_index = new Dictionary<string, int>();
            for (int label_index = 0; label_index < label_descriptors.Count; label_index++)
            {
                string label_name = label_descriptors[label_index].Name;
                if (label_name_to_label_index.ContainsKey(label_name))
                {
                    throw new Exception("Duplciate label: " + label_name);
                }
                this.label_name_to_label_index[label_name] = label_index;
            }
        }

        public DataContext()
           : this(new List<VariableDescriptor>(), new List<VariableDescriptor>())
        {

        }
  

        public DataContext(DataLevel feature_level, int feature_count, DataLevel label_level, int label_count)
            : this(CreateVariableDescriptors(feature_level, feature_count, "FEATURE"), CreateVariableDescriptors(feature_level, feature_count, "LABEL"))
		{
			
        }


        public DataContext(DataLevel feature_level, IList<string> feature_names, DataLevel label_level, IList<string> label_names)
      : this(CreateVariableDescriptors(feature_level, feature_names), CreateVariableDescriptors(feature_level, label_names))
        {

        }

        private static IList<VariableDescriptor> CreateVariableDescriptors(DataLevel data_lavel, IList<string> feature_names)
        {
            List<VariableDescriptor> feature_descriptors = new List<VariableDescriptor>();
            for (int variable_index = 0; variable_index < feature_names.Count; variable_index++)
            {
                feature_descriptors.Add(new VariableDescriptor(feature_names[variable_index], data_lavel));
            }
            return feature_descriptors;
        }

        private static IList<VariableDescriptor> CreateVariableDescriptors(DataLevel data_lavel, int variable_count, string variable_type)
        {
            string variable_name_prefix = "";
            switch (data_lavel)
            {
                case DataLevel.BINARY:
                    variable_name_prefix = "BINARY_" + variable_type + "_";
                    break;
                case DataLevel.INTERVAL:
                    variable_name_prefix = "INTERVAL_" + variable_type + "_";
                    break;
                case DataLevel.NOMINAL:
                    throw new Exception("illegal "+ variable_type + " level: " + data_lavel);
                case DataLevel.ORDINAL:
                    throw new Exception("illegal " + variable_type + " level: " + data_lavel);
                case DataLevel.UNIQUE:
                    variable_name_prefix = "UNIQUE_" + variable_type + "_";
                    break;
                default:
                    throw new Exception("unkown " + variable_type + " level: " + data_lavel);
            }
            List<VariableDescriptor> variable_descriptors = new List<VariableDescriptor>();
            for (int variable_index = 0; variable_index < variable_count; variable_index++)
            {
                string variable_name = variable_name_prefix + variable_index.ToString();
                variable_descriptors.Add(new VariableDescriptor(variable_name, data_lavel));
            }
            return variable_descriptors;
        }

        public string GetFeatureName(int feature_index)
        {
            return feature_descriptors[feature_index].Name;
        }

        public string GetLabelName(int label_index)
        {
            return label_descriptors[label_index].Name;
        }

        public int GetFeatureIndex(
            string feature_name)
        {
            return feature_name_to_feature_index[feature_name];
        }

        public int GetLabelIndex(
             string label_name)
        {
            return label_name_to_label_index[label_name];
        }

        public VariableDescriptor GetFeatureDescriptor(int feature_index)
        {
            return feature_descriptors[feature_index];
        }

        public VariableDescriptor GetLabelDescriptor(int label_index)
        {
            return label_descriptors[label_index];
        }

        public VariableDescriptor GetFeatureDescriptor(string feature_name)
        {
            return feature_descriptors[feature_name_to_feature_index[feature_name]];
        }

        public VariableDescriptor GetLabelDescriptor(string label_name)
        {
            return label_descriptors[label_name_to_label_index[label_name]];
        }




		public int get_converted_interval_feature_count()
		{
			int feature_level_count_sum = 0;
            foreach (VariableDescriptor feature_descriptor in this.FeatureDescriptors)
			{
				feature_level_count_sum += feature_descriptor.ValueCount;
			}
			return feature_level_count_sum;
		}     


        public static DataContext Read(BinaryReader reader)
        {
            IList<VariableDescriptor> feature_descriptors = new List<VariableDescriptor>();
            IList<VariableDescriptor> label_descriptors = new List<VariableDescriptor>();
            int feature_count = reader.ReadInt32();
            for (int feature_index = 0; feature_index < feature_count; feature_index++)
            {
                string feature_name = reader.ReadString();
                DataLevel data_level = reader.ReadEnum<DataLevel>();
                string[] value_strings = reader.ReadStringArray1D();
                feature_descriptors.Add(new VariableDescriptor(feature_name, data_level, value_strings));
            }

            int label_count = reader.ReadInt32();
            for (int label_index = 0; label_index < label_count; label_index++)
            {
                string label_name = reader.ReadString();
                DataLevel data_level = reader.ReadEnum<DataLevel>();
                string[] value_strings = reader.ReadStringArray1D();
                label_descriptors.Add(new VariableDescriptor(label_name, data_level, value_strings));
            }

            return new DataContext(feature_descriptors, label_descriptors);
        }

        public void Write(BinaryWriter writer)
        {
            writer.Write(this.FeatureCount);
            for (int feature_index = 0; feature_index < FeatureCount; feature_index++)
            {
                VariableDescriptor desciptor = this.feature_descriptors[feature_index];
                writer.Write(desciptor.Name);
                writer.WriteEnum(desciptor.DataLevel);
                writer.WriteStringArray1D(desciptor.ValueStrings);
            }

            writer.Write(this.LabelCount);
            for (int label_index = 0; label_index < LabelCount; label_index++)
            {
                VariableDescriptor desciptor = this.label_descriptors[label_index];
                writer.Write(desciptor.Name);
                writer.WriteEnum(desciptor.DataLevel);
                writer.WriteStringArray1D(desciptor.ValueStrings);
            }
        }


        public override bool Equals(object other)
        {
            if (!(other is DataContext))
            {
                return false;
            }

            DataContext other_typed = (DataContext)other;

            if (!other_typed.FeatureCount.Equals(this.FeatureCount))
            {
                return false;
            }

            if (!other_typed.LabelCount.Equals(this.LabelCount))
            {
                return false;
            }

            for (int feature_index = 0; feature_index < this.FeatureCount; feature_index++)
            {
                if (!feature_descriptors[feature_index].Equals(other_typed.feature_descriptors[feature_index]))
                {
                    return false;
                }
            }

            for (int label_index = 0; label_index < this.LabelCount; label_index++)
            {
                if (!label_descriptors[label_index].Equals(other_typed.label_descriptors[label_index]))
                {
                    return false;
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            int hash_code = 0;
            for (int feature_index = 0; feature_index < this.FeatureCount; feature_index++)
            {
                hash_code += feature_descriptors[feature_index].GetHashCode();
            }
            for (int label_index = 0; label_index < this.FeatureCount; label_index++)
            {
                hash_code += label_descriptors[label_index].GetHashCode();
            }
            return hash_code;
        }




        public IDataContext SelectFeature(int selected_feature_index)
        {
            return SelectFeatures(new int[] { selected_feature_index });
        }

        public IDataContext SelectFeatures(IList<int> selected_feature_indexes)
        {
            return SelectLabelsAndFeatures(selected_feature_indexes, ToolsMathSeries.RangeInt32(LabelCount));
        }

        public IDataContext SelectLabel(int selected_label_index)
        {
            return SelectLabels(new int[] { selected_label_index });
        }

        public IDataContext SelectLabels(IList<int> selected_label_indexes)
        {
            return SelectLabelsAndFeatures(ToolsMathSeries.RangeInt32(FeatureCount), selected_label_indexes);
        }

        public IDataContext SelectLabelsAndFeatures(IList<int> selected_feature_indexes, IList<int> selected_label_indexes)
        {
            IList<VariableDescriptor> new_feature_descriptors = ToolsCollection.SelectList(feature_descriptors, selected_feature_indexes);
            IList<VariableDescriptor> new_label_descriptors = ToolsCollection.SelectList(label_descriptors, selected_label_indexes);
            return new DataContext(new_feature_descriptors, new_label_descriptors);
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine("Features");
            foreach (VariableDescriptor feature_descriptor in feature_descriptors)
            {
                builder.Append(feature_descriptor.ToString());
            }
            builder.AppendLine("Labels");
            foreach (VariableDescriptor label_descriptor in label_descriptors)
            {
                builder.Append(label_descriptor.ToString());
            }
            return builder.ToString();
        }
    }
}