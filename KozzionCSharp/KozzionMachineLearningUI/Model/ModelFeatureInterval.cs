using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KozzionMachineLearning.DataSet;
using ReactiveUI;

namespace KozzionMachineLearningUI.Model
{
    public class ModelFeatureInterval : ReactiveObject, IModelFeature
    {
        VariableDescriptor feature_descriptor;

        private string name;
        public string Name
        {
            get { return this.name; }
            set { this.RaiseAndSetIfChanged(ref this.name, value); }
        }

        public ModelFeatureInterval(VariableDescriptor feature_descriptor) 
        {
            if (feature_descriptor.DataLevel != DataLevel.INTERVAL)
            {
                throw new Exception("Not a interval feature");
            }
            this.feature_descriptor = feature_descriptor;
            this.Name = feature_descriptor.Name;
        }

    }
}
