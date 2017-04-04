using System.Collections.Generic;
using System.IO;

namespace KozzionMachineLearning.DataSet
{
    public interface IDataContext
    {
        int FeatureCount { get; }
        int LabelCount { get; }

        string GetFeatureName(int feature_index);
        string GetLabelName(int label_index);

        int GetFeatureIndex(string feature_name);
        int GetLabelIndex(string feature_name);

        IList<string> FeatureNames { get; }
        IList<string> LabelNames { get; }

        IList<VariableDescriptor> FeatureDescriptors { get; }
        IList<VariableDescriptor> LabelDescriptors { get; }

        VariableDescriptor GetFeatureDescriptor(int feature_index);
        VariableDescriptor GetFeatureDescriptor(string feature_name);

        VariableDescriptor GetLabelDescriptor(int label_index);
        VariableDescriptor GetLabelDescriptor(string label_name);

        IDataContext SelectFeature(int selected_feature_index);
        IDataContext SelectFeatures(IList<int> selected_feature_indexes);

        IDataContext SelectLabel(int selected_label_index);
        IDataContext SelectLabels(IList<int> selected_label_indexes);

        IDataContext SelectLabelsAndFeatures(IList<int> selected_feature_indexes, IList<int> selected_label_indexes);

        void Write(BinaryWriter writer);
    }
}