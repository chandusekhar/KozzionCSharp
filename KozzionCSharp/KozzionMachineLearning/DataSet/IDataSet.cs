using MathNet.Numerics.LinearAlgebra;
using System;
using System.Collections.Generic;

namespace KozzionMachineLearning.DataSet
{
    public interface IDataSet
    {
        IDataContext DataContext { get; }

        int InstanceCount { get; }
        int FeatureCount { get; }
        int GetMissingCount(int feature_index);

        IList<DataInstance> InstanceList { get; }

        IDataSet RemoveFeatures(IList<int> feature_indexes);
        IDataSet RemoveInstancesMissing(int feature_index);

        IDataSet PromoteFeatureToLabel(int feature_index);
        Matrix<double> GetFeatureDataAsMatrix();
        IDataSet PromoteFeatureLevelToInterval(int[] interval_feature_indexes);
    }



    public interface IDataSet<FeatureType>
    {
        IDataContext DataContext { get; }

        int InstanceCount { get; }
        int FeatureCount { get; }
        int GetMissingCount(int feature_index);

        FeatureType[][] FeatureData { get; }
        FeatureType[] GetInstanceFeatureData(int instance_index);

        bool[][] MissingData { get; }
        bool[] GetInstanceMissingData(int instance_index);

        IDataSet<FeatureType> RemoveFeatures(IList<int> feature_indexes);
        IDataSet<FeatureType> RemoveInstancesMissing(int feature_index);

        IDataSet<FeatureType, FeatureType> PromoteFeatureToLabel(int feature_index);

        IDataSetHybrid PromoteFeatureLevelToInterval(int[] interval_feature_indexes);
    }

    public interface IDataSet<FeatureType, LabelType> : IDataSet<FeatureType>
    {
        IDataSet<FeatureType, LabelType> SelectInstances(IList<int> instance_indexes);
        IDataSet<FeatureType, LabelType> SelectFeatures(IList<int> feature_indexes);

        new IDataSet<FeatureType, LabelType> RemoveFeatures(IList<int> feature_indexes);   
        new IDataSet<FeatureType, LabelType> RemoveInstancesMissing(int feature_index);

        Tuple<IDataSet<FeatureType, LabelType>, IDataSet<FeatureType, LabelType>> Split();
        Tuple<IDataSet<FeatureType, LabelType>, IDataSet<FeatureType, LabelType>> Split(double fraction);

        int LabelCount { get; }   
        LabelType[][] LabelData { get; }
   
        LabelType[] GetInstanceLabelData(int instance_index);
        LabelType[] GetLabelDataColumn(int label_index);
        IDataSet<double, int> ConvertToDataSetInterval();
    }
}