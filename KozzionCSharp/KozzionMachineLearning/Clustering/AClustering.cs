using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KozzionMachineLearning.DataSet;

namespace KozzionMachineLearning.Clustering
{
    public abstract class AClustering : IClustering
    {
        public IDataContext DataContext { get; private set; }

        protected AClustering(IDataContext data_context)
        {
            this.DataContext = data_context;
        }
    }
}
