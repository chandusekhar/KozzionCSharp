using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KozzionMachineLearning.Model;
using KozzionMachineLearning.Method.DecisionTrees;
using KozzionMachineLearning.Method.NearestNeighbor;
using KozzionMachineLearning.Method.SupportVectorMachine;
using KozzionMachineLearning.Method.NaiveBayes;

namespace KozzionMachineLearning.Tools
{
    public class ToolsModelIO
    {

        public static IModelDiscrete<DomainType, LabelType> ReadModelDiscrete<DomainType, LabelType>(BinaryReader reader)
        {
            string model_type = reader.ReadString();
            switch (model_type)
            {
                case "ModelDiscreteC45Default":
                    return (IModelDiscrete<DomainType, LabelType>)ModelDiscreteC45Default.Read(reader);
                case "ModelNearestNeighborListDefault":
                    return (IModelDiscrete<DomainType, LabelType>)ModelNearestNeighborListDefault.Read(reader);
                case "ModelNearestNeighborKDTreeDefault":
                    return (IModelDiscrete<DomainType, LabelType>)ModelNearestNeighborKDTreeDefault.Read(reader);
                case "ModelLibSVMCSVC":
                    return (IModelDiscrete<DomainType, LabelType>)ModelLibSVMCSVC.Read(reader);
                case "ModelNaiveBayesInterval":
                    return (IModelDiscrete<DomainType, LabelType>)ModelNaiveBayesInterval.Read(reader);
                default:
                    throw new Exception("Unknown discrete model type: " + model_type);
            }        
        }

        public static void WriteModelDiscrete<DomainType, LabelType>(BinaryWriter writer, IModelDiscrete<DomainType, LabelType> model)
        {
            string model_type = model.ModelType;
            writer.Write(model_type);
            switch (model_type)
            {
                case "ModelDiscreteC45Default":
                    ModelDiscreteC45Default.Write(writer, (ModelDiscreteC45Default)model);
                    break;
                case "ModelNearestNeighborListDefault":
                    ModelNearestNeighborListDefault.Write(writer, (ModelNearestNeighborListDefault)model);
                    break;
                case "ModelNearestNeighborKDTreeDefault":
                    ModelNearestNeighborKDTreeDefault.Write(writer, (ModelNearestNeighborKDTreeDefault)model);
                    break;
                case "ModelLibSVMCSVC":
                    ModelLibSVMCSVC.Write(writer, (ModelLibSVMCSVC)model);
                    break;
                case "ModelNaiveBayesIntervalDefault":
                    ModelNaiveBayesInterval.Write(writer, (ModelNaiveBayesInterval)model);
                    break;
                default:
                    throw new Exception("Unknown discrete model type: " + model_type);
            }
        }
    }
}
