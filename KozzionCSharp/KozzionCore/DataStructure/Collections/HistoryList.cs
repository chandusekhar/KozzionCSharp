using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionCore.DataStructure.Collections
{
    public class HistoryList<DataType> :  IHistoryList<DataType>
    {      
        public int HistoryCount { get { return history.Count; } }
        public int FutureCount { get { return future.Count; } }

        private IList<DataType> history;
        private IList<DataType> future;

        //Main default
        public HistoryList()
        {
            this.history = new List<DataType>();
            this.future = new List<DataType>();
        }

        //Primary Constructor
        public HistoryList(IReadOnlyList<DataType> history, IReadOnlyList<DataType> future, bool reverese_lists = false)
        {
            this.history = new List<DataType>(history.Reverse());
            this.future = new List<DataType>(future.Reverse());     

        }

        //Copy Constructor
        public HistoryList(HistoryList<DataType> other)
        {
            this.history = new List<DataType>(other.history);
            this.future = new List<DataType>(other.future);
        }


        public HistoryList(IReadOnlyList<DataType> future)
           : this(new List<DataType>(), future, true)
        {

        }  

        public void Add(DataType item)
        {
            if (future.Count != 0)
            {
                throw new Exception("Cannot add while future remains");
            }
            history.Add(item);
        }

        public void Step()
        {
            if ( future.Count == 0)
            {
                throw new Exception("No future to step");
            }
            history.Add(future.RemoveLast());
        }

        public DataType GetHistory(int index)
        {
            return history[history.Count - index - 1];
        }

        public DataType GetFuture(int index)
        {
            return future[future.Count - index - 1];
        }

        public List<DataType> ToList()
        {
            List<DataType> full_list = new List<DataType>(history.Reverse());
            full_list.AddRange(future.Reverse());
            return full_list;
        }

        public IReadOnlyList<DataType> ToReadOnlyList()
        {
            return ToList().AsReadOnly();
        }
        
    }
}
