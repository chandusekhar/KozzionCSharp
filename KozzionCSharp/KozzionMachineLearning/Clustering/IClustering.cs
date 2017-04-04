using KozzionMachineLearning.DataSet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMachineLearning.Clustering
{
    public interface IClustering
    {
        IDataContext DataContext { get; }
    }
}
