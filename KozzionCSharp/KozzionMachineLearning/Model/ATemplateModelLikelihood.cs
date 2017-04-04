using System;
using KozzionMachineLearning.Reporting;
using KozzionMachineLearning.Method.JointTable;
using KozzionMachineLearning.DataSet;
using KozzionMathematics.Algebra;

namespace KozzionMachineLearning.Model
{
    public abstract class ATemplateModelLikelihood<DomainType, LabelType> :
        ATemplateModelDiscrete<DomainType, LabelType>,
        ITemplateModelLikelihood<DomainType, LabelType>

    {
        public ReportLikelihood<DomainType, LabelType, double> GenerateAndTestLikelihood(IDataSet<DomainType, LabelType> training_set, IDataSet<DomainType, LabelType> test_set)
        {
            IModelLikelihood<DomainType, LabelType, double> model = GenerateModelLikelihood(training_set);
            double[][] likelihoods = new double[test_set.InstanceCount][];
            LabelType[] labels = new LabelType[test_set.InstanceCount];
            for (int instance_index = 0; instance_index < test_set.InstanceCount; instance_index++)
            {
                likelihoods[instance_index] = model.GetLikelihoods(test_set.GetInstanceFeatureData(instance_index));
            }
       
            return new ReportLikelihood<DomainType, LabelType, double>(new AlgebraRealFloat64(), model, likelihoods, labels);
        }

        public override IModelDiscrete<DomainType, LabelType> GenerateModelDiscrete(IDataSet<DomainType, LabelType> training_set)
        {
            return GenerateModelLikelihood(training_set);
        }

        public abstract IModelLikelihood<DomainType, LabelType, double> GenerateModelLikelihood(IDataSet<DomainType, LabelType> training_set);
    }

   
}