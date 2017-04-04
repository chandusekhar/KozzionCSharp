using Accord.MachineLearning.DecisionTrees;
using Accord.MachineLearning.DecisionTrees.Learning;
using Accord.Math;
using AForge;
using KozzionCore.IO;
using KozzionCore.Tools;
using KozzionMachineLearning.DataSet;
using KozzionMachineLearning.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMachineLearning.Method
{

    public class ModelDiscreteC45<LabelType>: AModelLabel<double, LabelType>, IModelDiscrete<double, LabelType>
    {
        protected DecisionTree tree;
        //https://en.wikipedia.org/wiki/C4.5_algorithm

        protected ModelDiscreteC45(IDataContext data_context, DecisionTree tree, string model_type)
            : base(data_context, model_type)
        {
            this.tree = tree;
        }

        public ModelDiscreteC45(IDataContext data_context, DecisionTree tree)
          : this(data_context, tree, "ModelDiscreteC45Generic")
        {
  
        }


        public override LabelType GetLabel(double[] instance_features)
        {
            throw new NotImplementedException();
            //return DataContext.LabelDescriptors[0].(GetLabelIndex(instance_features));
        }

        public int GetLabelIndex(double[] instance_features)
        {
            return tree.Compute(instance_features);
        }
    }
}
