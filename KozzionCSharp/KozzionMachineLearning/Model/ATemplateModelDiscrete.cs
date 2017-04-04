using KozzionMachineLearning.DataSet;
using KozzionMachineLearning.Reporting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMachineLearning.Model
{
    public abstract class ATemplateModelDiscrete<DomainType, LabelType> :
        ATemplateModelLabel<DomainType, LabelType>,
        ITemplateModelDiscrete<DomainType, LabelType>
    {
        public abstract IModelDiscrete<DomainType, LabelType> GenerateModelDiscrete(IDataSet<DomainType, LabelType> training_set);

        public override IModelLabel<DomainType, LabelType> GenerateModel(IDataSet<DomainType, LabelType> training_set)
        {
            return GenerateModelDiscrete(training_set);
        }  

        public ReportDiscrete<DomainType, LabelType> GenerateAndTestDiscrete(IDataSet<DomainType, LabelType> training_set, IDataSet<DomainType, LabelType> test_set)
        {
            //TODO check data contexts (they should match)
            IModelDiscrete<DomainType, LabelType> model = GenerateModelDiscrete(training_set);
            int label_value_count = test_set.DataContext.GetLabelDescriptor(0).ValueCount;
            int[,] confusion_matrix_instances = new int[label_value_count, label_value_count];
            for (int instance_index = 0; instance_index < test_set.InstanceCount; instance_index++)
            {
                //int true_label_index = test_set.DataContext.GetLabelValueIndex(test_set.GetInstanceLabelData(instance_index));
                //int model_label_index = model.GetLabelIndex(test_set.GetInstanceFeatureData(instance_index));
                //confusion_matrix_instances[true_label_index, model_label_index]++;
                //// 
                throw new NotImplementedException();
            }
            return new ReportDiscrete<DomainType, LabelType>(model, confusion_matrix_instances);
        }

   

     

    
    }
}
