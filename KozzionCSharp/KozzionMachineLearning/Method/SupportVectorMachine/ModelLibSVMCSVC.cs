using KozzionMachineLearning.Model;
using KozzionMachineLearning.Method.JointTable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using libsvm;
using KozzionCore.Tools;
using KozzionMachineLearning.DataSet;
using System.IO;
using KozzionCore.IO;

namespace KozzionMachineLearning.Method.SupportVectorMachine
{
    public class ModelLibSVMCSVC : 
        AModelDiscrete<double, int>, 
        IModelLikelihood<double, int, double>
    {
        private C_SVC svm;

        public ModelLibSVMCSVC(IDataContext data_context, C_SVC c_SVC)
            : base(data_context, "ModelLibSVMCSVC")
        {
            this.svm = c_SVC;
        }

        public override int GetLabel(double[] instance_features)
        {
            svm_node[] testnode = TemplateModelLibSVMCSVC.CreateNodeArray(instance_features);
            return (int)svm.Predict(testnode);
        }


        public double[] GetLikelihoods(double[] instance_features)
        {
            svm_node[] testnode = TemplateModelLibSVMCSVC.CreateNodeArray(instance_features);

            //This works correctly:
            Dictionary<int, double> prediction = svm.PredictProbabilities(testnode);
            int[] labels = prediction.Keys.ToArray();
            return ToolsCollection.GetValueArray(prediction, labels);
        }



        public static IModelLikelihood<double, int, double> Read(BinaryReader reader)
        {
            byte[] bytes = reader.ReadByteArray1D();
            string temp_file_name = Path.GetTempFileName();
            File.WriteAllBytes(temp_file_name, bytes); //Super HAXX
            C_SVC svm = new C_SVC(temp_file_name);
            File.Delete(temp_file_name);

            IDataContext data_context = DataSet.DataContext.Read(reader);
            return new ModelLibSVMCSVC(data_context, svm);
        }


        public static void Write(BinaryWriter writer, ModelLibSVMCSVC model)
        {
            string temp_file_name = Path.GetTempFileName();
            model.svm.Export(temp_file_name);  //Super HAXX
            byte[] bytes = File.ReadAllBytes(temp_file_name);
            writer.WriteByteArray1D(bytes);
            File.Delete(temp_file_name);
            model.DataContext.Write(writer);


        }
    }
}
