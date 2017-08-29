using KozzionCore.Tools;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMachineLearning.DataSet
{
    public class DataSetDefault : IDataSet
    {
   
        public IDataContext DataContext { get; }

        public int FeatureCount { get { return DataContext.FeatureCount; } }
        public int InstanceCount { get { return _instances.Count; } }

        public IList<DataInstance> _instances;
        public IList<DataInstance> InstanceList { get { return new List<DataInstance>(_instances); } }


        public DataSetDefault(double[,] array)
            : this(array.ToArrayArray10())
        {
        }

        public DataSetDefault(IList<double[]> array)
           :  this(new DataContext(DataLevel.INTERVAL, array[0].Length, DataLevel.INTERVAL, 0), array)
        {
        }

        public DataSetDefault(IDataContext dataContext, IList<double[]> array)
        {
            DataContext = dataContext;
            _instances = new List<DataInstance>();
            foreach (var item in array)
            {
                _instances.Add(new DataInstance(DataContext, item));
            }
        }

        public int GetMissingCount(int feature_index)
        {
            throw new NotImplementedException();
        }

        public IDataSet PromoteFeatureLevelToInterval(int[] interval_feature_indexes)
        {
            throw new NotImplementedException();
        }

        public IDataSet PromoteFeatureToLabel(int feature_index)
        {
            throw new NotImplementedException();
        }

        public IDataSet RemoveFeatures(IList<int> feature_indexes)
        {
            throw new NotImplementedException();
        }

        public IDataSet RemoveInstancesMissing(int feature_index)
        {
            throw new NotImplementedException();
        }

        public Matrix<double> GetFeatureDataAsMatrix()
        {
            var matrix = new DenseMatrix(InstanceCount, FeatureCount);
            for (int i = 0; i < InstanceCount; i++)
            {
                matrix.SetRow(i, InstanceList[i].FeatureData);
            }
            return matrix;
        }
    }
}
