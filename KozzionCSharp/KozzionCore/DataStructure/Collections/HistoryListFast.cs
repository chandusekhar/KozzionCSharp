using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionCore.DataStructure.Collections
{
    public class HistoryListFast<DataType> : IHistoryList<DataType>
    {
        public int HistoryCount { get { return offset; } }
        public int FutureCount { get { return history_future.Count - offset; } }

        private int offset;
        private IReadOnlyList<DataType> history_future;

        //Primary Constructor
        public HistoryListFast(IReadOnlyList<DataType> future)
        {
            this.offset = 0;
            this.history_future = future;
        }

        //Copy Constructor
        public HistoryListFast(HistoryListFast<DataType> other)
        {
            this.offset = other.offset;
            this.history_future = other.history_future;
        }  

        public void Add(DataType item)
        {
            throw new NotImplementedException("Not implemented in fast version");
        }

        public void Step()
        {
            if (FutureCount == 0)
            {
                throw new Exception("No future to step");
            }
            offset++;
        }

        public DataType GetHistory(int index)
        {
            return history_future[offset - index - 1];
        }

        public DataType GetFuture(int index)
        {
            return history_future[offset + index];
        }

        public List<DataType> ToList()
        {
           throw new NotImplementedException("Not implemented in fast version");
        }

        public IReadOnlyList<DataType> ToReadOnlyList()
        {
            return history_future;
        }
    }
}
