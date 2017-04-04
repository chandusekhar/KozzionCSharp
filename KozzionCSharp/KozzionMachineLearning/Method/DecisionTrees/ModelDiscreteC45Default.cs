using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KozzionMachineLearning.Model;
using KozzionMachineLearning.DataSet;
using Accord.MachineLearning.DecisionTrees;
using KozzionCore.IO;

namespace KozzionMachineLearning.Method.DecisionTrees
{
    public class ModelDiscreteC45Default : ModelDiscreteC45<int>
    {

        public ModelDiscreteC45Default(IDataContext data_context, DecisionTree tree)
            :base(data_context, tree, "ModelDiscreteC45Default")
        {
        }

        public static ModelDiscreteC45Default Read(BinaryReader reader)
        {
   
            byte[] data = reader.ReadByteArray1D();
            MemoryStream stream = new MemoryStream(data);
            DecisionTree tree = DecisionTree.Load(stream);
            stream.Dispose();

            IDataContext data_context = DataSet.DataContext.Read(reader);
            return new ModelDiscreteC45Default(data_context, tree);
        }

        public static void Write(BinaryWriter writer, ModelDiscreteC45Default model)
        {    
            MemoryStream stream = new MemoryStream();
            model.tree.Save(stream);
            byte[] data = stream.ToArray();
            writer.WriteByteArray1D(data);
            stream.Dispose();

            model.DataContext.Write(writer);
        }
    }
}
