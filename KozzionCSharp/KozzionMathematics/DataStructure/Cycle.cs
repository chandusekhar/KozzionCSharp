using KozzionCore.Tools;
using KozzionMathematics.Function;
using KozzionMathematics.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionCore.DataStructure.Collections
{
    public class Cycle<DataType> : IList<DataType>, IFunction<int, DataType>
    {

        public string FunctionType
        {
            get
            {
                return "FunctionCycle";
            }
        }

        private IList<DataType> inner_list;
  

        public Cycle(int cycle_size)
        {
            inner_list = new DataType[cycle_size];
        }

        public Cycle(IList<DataType> list_0)
        {
            inner_list = ToolsCollection.Copy(list_0);
        }

        public DataType this[int index]
        {
            get
            {
                return inner_list[ToolsMath.Wrap(index, inner_list.Count)];
            }

            set
            {
                inner_list[ToolsMath.Wrap(index, inner_list.Count)] = value;
            }
        }

        public int Count
        {
            get
            {
                return inner_list.Count;
            }
        }


        public bool IsReadOnly
        {
            get
            {
                return inner_list.IsReadOnly;
            }
        }

        public void Add(DataType item)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            inner_list.Clear();
        }

        public DataType Compute(int domain_value_0)
        {
            return this[domain_value_0];
        }

        public bool Contains(DataType item)
        {
            return inner_list.Contains(item);
        }

        public void CopyTo(DataType[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<DataType> GetEnumerator()
        {
            return inner_list.GetEnumerator();
        }

        public void Insert(int index, DataType item)
        {
            this[index] = item;
        }     

        IEnumerator IEnumerable.GetEnumerator()
        {
            return inner_list.GetEnumerator();
        }

        public int IndexOf(DataType item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(DataType item)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }
    }
}
