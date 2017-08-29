using KozzionCore.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMachineLearning.DataSet
{
    public class DataInstance
    {
        private IDataContext DataContext { get; }

        private double[] _featureData;
        public double[] FeatureData { get { return ToolsCollection.Copy(_featureData); } }
        private bool[] _featureDataMissing;
        public bool[] FeatureDataMissing { get { return ToolsCollection.Copy(_featureDataMissing); } }

        private double[] _labelData;
        public double[] LabelData { get { return ToolsCollection.Copy(_labelData); } }
        private bool[] _labelDataMissing;
        public bool[] LabelDataMissing { get { return ToolsCollection.Copy(_labelDataMissing); } }

        public DataInstance(IDataContext dataContext, double[] featureData)
        {
            this.DataContext = dataContext;
            this._featureData = featureData;
        }



    }
}
