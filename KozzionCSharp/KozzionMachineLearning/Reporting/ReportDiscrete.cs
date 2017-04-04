using System.IO;
using KozzionCore.Tools;
using KozzionMachineLearning.Model;
using KozzionCore.IO;
using System;
using KozzionMachineLearning.Tools;

namespace KozzionMachineLearning.Reporting
{
    public class ReportDiscrete<DomainType, LabelType>
    {

        public IModelDiscrete<DomainType, LabelType> Model { get; private set; }

        int[,] confusion_matrix_instances;
        public int[,] ConfusionMatrixInstances { get { return ToolsCollection.Copy(confusion_matrix_instances); } }

        double[,] confusion_matrix_rates;

        public double[,] ConfusionMatrixRates { get { return ToolsCollection.Copy(confusion_matrix_rates); } }

        public int CorrectLabelCount { get; private set; }
        public double CorrectLabelRate { get; private set; }

        public ReportDiscrete(IModelDiscrete<DomainType, LabelType> model,  int[,] confusion_matrix_instances)
        {
            this.Model = model;
            this.confusion_matrix_instances = ToolsCollection.Copy(confusion_matrix_instances);
            this.confusion_matrix_rates = new double[confusion_matrix_instances.GetLength(0), confusion_matrix_instances.GetLength(1)];
            int instance_count = 0;
            this.CorrectLabelCount = 0;
            for (int index_0 = 0; index_0 < confusion_matrix_instances.GetLength(0); index_0++)
            {
                for (int index_1 = 0; index_1 < confusion_matrix_instances.GetLength(1); index_1++)
                {
                    instance_count += confusion_matrix_instances[index_0, index_1];
                    if (index_0 == index_1)
                    {
                        CorrectLabelCount += confusion_matrix_instances[index_0, index_1];
                    }
                }
            }
            CorrectLabelRate = CorrectLabelCount / (double)instance_count;

            for (int index_0 = 0; index_0 < confusion_matrix_instances.GetLength(0); index_0++)
            {
                for (int index_1 = 0; index_1 < confusion_matrix_instances.GetLength(1); index_1++)
                {
                    confusion_matrix_rates[index_0, index_1] = (double)confusion_matrix_instances[index_0, index_1] / (double)instance_count;
                }
            }
        }

        public ReportDiscrete(IModelDiscrete<DomainType, LabelType> model, BinaryReader reader)
            : this(model, reader.ReadInt32Array2D())
        {
      
        }
        public static ReportDiscrete<DomainType, LabelType> Read(IModelDiscrete<DomainType, LabelType> model, BinaryReader reader)
        {
            return new ReportDiscrete<DomainType, LabelType>(model, reader.ReadInt32Array2D());
        }

        public void Write(BinaryWriter writer)
        {
            ToolsModelIO.WriteModelDiscrete(writer, Model);
            writer.Write(this.confusion_matrix_instances);
        }
    }
}
