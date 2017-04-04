using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionMachineLearning.DataSet
{
    public class DataSetHybrid : IDataSetHybrid
    {
        public IDataSet<int, int> ConvertToNominal(int stratification_levels)
        {
            throw new NotImplementedException();
        }

        public static DataSetHybrid Read(BinaryReader reader)
        {
            throw new NotImplementedException();
        }
    }
}
