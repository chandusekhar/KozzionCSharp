using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KozzionMachineLearning.DataSet;
using ReactiveUI;
using System.Collections.ObjectModel;

namespace KozzionMachineLearningUI.Model
{
    public class ModelFeatureNominal : ReactiveObject, IModelFeature
    {
        public VariableDescriptor feature_descriptor {get; private set;}

        private IList<string> feature_value_types;
        public IList<string> FeatureValueTypes
        {
            get { return this.feature_value_types; }
            set { this.RaiseAndSetIfChanged(ref this.feature_value_types, value); }
        }

        private string name;
        public string Name
        {
            get { return this.name; }
            set { this.RaiseAndSetIfChanged(ref this.name, value); }
        }

        public ModelFeatureNominal(VariableDescriptor feature_descriptor) 
        {
            if(feature_descriptor.DataLevel != DataLevel.NOMINAL)
            {
                throw new Exception("Not a nominal feature");
            }
            this.feature_descriptor = feature_descriptor;
            this.Name = feature_descriptor.Name;
            if (feature_descriptor.ValueCount < 20)
            {
                this.FeatureValueTypes = feature_descriptor.ValueStrings;
            }
            else
            {
                IList<string> all_value_types = feature_descriptor.ValueStrings;
                IList<string> selected_value_types = new List<string>();
                for (int index = 0; index < 20; index++)
                {
                    selected_value_types.Add(all_value_types[index]);
                }
                selected_value_types.Add("And many others");
                this.FeatureValueTypes = selected_value_types;
            }
        }
    }
}
