
using System.Collections.Generic;

namespace KozzionMachineLearning.DataSet
{
    public interface IDataSetHybrid
    {
        //IDataSetUniqueUnlabeled DataSetUnique { get;  }
        //IDataSetIntervalUnlabeled DataSetInterval { get;  }
        //IDataSetOrdinalUnlabeled DataSetOrdinal { get; }
        //IDataSetNominalUnlabeled DataSetNominal { get; }
        //IDataSetBinaryUnlabeled DataSetBinary { get; }

        IDataSet<int,int> ConvertToNominal(int stratification_levels);
    }
}