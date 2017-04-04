using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionCore.DataStructure.Collections
{
    public interface IHistoryList<DataType>
    {
        int HistoryCount { get; }
        int FutureCount { get; }

        DataType GetHistory(int index);
        DataType GetFuture(int index);

        void Step();
        List<DataType> ToList();
        IReadOnlyList<DataType> ToReadOnlyList();
        void Add(DataType candle);
    }
}
