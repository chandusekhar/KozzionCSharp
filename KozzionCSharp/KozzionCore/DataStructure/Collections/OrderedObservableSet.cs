using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionCore.DataStructure
{
    public class OrderedObservableSet<Key, Value> : IList<Value>, INotifyCollectionChanged
        where Value : IKeyProvider<Key>
    {
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private SortedList<Key, Value> inner_set_list;


        public OrderedObservableSet()
        { 
            inner_set_list = new SortedList<Key, Value>();
        }

        public int Count
        {
            get
            {
                return inner_set_list.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        public Value this[int index]
        {
            get
            {
                return inner_set_list[inner_set_list.Keys[index]];
            }

            set
            {
                Insert(index, value);
      
            }
        }

  
        public int IndexOf(Value item)
        {
            throw new NotImplementedException();
        }

        public void Insert(int index, Value value)
        {
            if ((inner_set_list.ContainsKey(value.Key)))
            {
                throw new Exception("duplicate key");
            }

            Value old_item = inner_set_list[inner_set_list.Keys[index]];
            inner_set_list[inner_set_list.Keys[index]] = value;
            if (CollectionChanged != null)
            {
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, old_item, inner_set_list[inner_set_list.Keys[index]], index));
            }
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public void Add(Value item)
        {
            if (!Contains(item))
            {
                inner_set_list.Add(item.Key, item);
                if (CollectionChanged != null)
                {
                    CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, item));
                }
            }
        }

        public void Clear()
        {
            if (CollectionChanged != null)
            {
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        public bool Contains(Value item)
        {
            return inner_set_list.ContainsKey(item.Key);
        }

        public void CopyTo(Value[] array, int arrayIndex)
        {
            inner_set_list.Values.CopyTo(array, arrayIndex);
        }

        public bool Remove(Value item)
        {
            if (!Contains(item))
            {
                return false;
            }

            int index = inner_set_list.IndexOfKey(item.Key);
            bool result = inner_set_list.Remove(item.Key);
            //TODO chage what is removed
            if (CollectionChanged != null)
            {
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
            return result;
        }

        public IEnumerator<Value> GetEnumerator()
        {
            return inner_set_list.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return inner_set_list.Values.GetEnumerator();
        }

        public bool ContainsKey(Key key)
        {
            return inner_set_list.ContainsKey(key);
        }

        public Value GetByKey(Key key)
        {
            return inner_set_list[key];
        }
    }
}
