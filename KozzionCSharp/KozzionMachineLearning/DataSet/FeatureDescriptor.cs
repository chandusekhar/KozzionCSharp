using KozzionCore.Tools;
using System;
using System.Collections.Generic;
using System.Text;

namespace KozzionMachineLearning.DataSet
{
    [Serializable]
    public class VariableDescriptor
    {
        public string Name {get; private set;}

        public DataLevel DataLevel { get; private set; }

        public IList<string> ValueStrings { get; private set; }

        public int ValueCount { get { return ValueStrings.Count; } }

        Dictionary<string, int> value_string_to_value_index;

        public VariableDescriptor(
            string feature_name,
            DataLevel feature_level,
            IList<string> value_strings)
        {
            Name = feature_name;
            DataLevel = feature_level;
            ValueStrings = ToolsCollection.Copy(value_strings);

            value_string_to_value_index = new Dictionary<string, int>();
            for (int value_level_index = 0; value_level_index < value_strings.Count; value_level_index++)
            {
                value_string_to_value_index[value_strings[value_level_index]] = value_level_index;
            }
        }

        // For Unique binary of interval
        public VariableDescriptor(
            string feature_name,
            DataLevel feature_level)
            : this(feature_name, feature_level, new string [0])
        {

        }

        public ValueType GetValue<ValueType>(int value_index)
        {
            throw new NotImplementedException();
        }

        public int GetValueIndex<ValueType>(ValueType label_value)
        {
            throw new NotImplementedException();
        }

        //preferred for nominal
        public VariableDescriptor(
            string feature_name,
            DataLevel feature_level,
            Dictionary<string, int> feature_value_levels)
            : this(feature_name, feature_level, ToolsCollection.ConvertToArray1D(feature_value_levels))
        {

		}

        public string GetValueString(int value_index)
        {
            return ValueStrings[value_index];
        }

        public override bool Equals(object other)
        {
            if (!(other is VariableDescriptor))
            {
                return false;
            }

            VariableDescriptor other_typed = (VariableDescriptor)other;



            if (!other_typed.Name.Equals(this.Name))
            {
                return false;
            }

            if (!other_typed.DataLevel.Equals(this.DataLevel))
            {
                return false;
            }

            if (!other_typed.ValueStrings.Count.Equals(this.ValueStrings.Count))
            {
                return false;
            }

            for (int value_string_index = 0; value_string_index <ValueStrings.Count; value_string_index++)
            {
                if (!this.ValueStrings[value_string_index].Equals(other_typed.ValueStrings[value_string_index]))
                {
                    return false;
                }
            }
            return true;
        }

        public override int GetHashCode()
        {
            int hash_code = Name.GetHashCode();
            hash_code += DataLevel.GetHashCode();
            hash_code += ValueStrings.GetHashCode();
            return hash_code;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendLine(Name + " " + ToolsEnum.EnumToString(DataLevel));
            //public IList<string> ValueStrings { get; private set; }
            //public int ValueCount { get { return ValueStrings.Count; } }
            return builder.ToString();
        }
    }
}