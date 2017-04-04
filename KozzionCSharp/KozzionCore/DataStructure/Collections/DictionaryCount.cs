using System;
using System.Collections;
using System.Collections.Generic;

namespace KozzionCore.Collections
{
    //TODO first transfer... make this a propper dictionart
    public class DictionaryCount<Key> : IDictionary<Key, int> 
    {
 
        private Dictionary<Key, int> inner_dictionary;
        public int Count
        {
            get
            {
                return inner_dictionary.Count;
            }
        }

        public int TotalCount { get; private set; }
     
        public DictionaryCount()
        {
            TotalCount = 0;
            inner_dictionary = new Dictionary<Key,int>();
        }

        public DictionaryCount(DictionaryCount<Key> other)
        {
            TotalCount = other.TotalCount;
            inner_dictionary = new Dictionary<Key,int>(other.inner_dictionary);
        }


        public DictionaryCount(IList<Key> data)
            : this()
        {
            foreach (Key key in data)
            {
                Increment(key);
            }
        }

        public void Increment(Key key)
        {
            Add(key, 1);
        }
    
        public void Decrement(Key key)
        {
            Add(key, -1);        
        }

        public void Add(Key key, int value)
        {
            if (ContainsKey(key))
            {
                if ((inner_dictionary[key] + value) == 0)
                {
                    Remove(key);
                }
                else
                {
                    inner_dictionary[key] = inner_dictionary[key] + value;
                }
            }
            else
            {
                inner_dictionary[key] = value;
            }
            TotalCount += value;
        }

        public void Add(KeyValuePair<Key, int> item)
        {
            Add(item.Key, item.Value);
        }
  
        public int Get( Key key)
        {
  
            if (!ContainsKey(key))
            {
                return 0;
            }
            else
            {
                return inner_dictionary[key];
            }
        }

        public int GetMaximalCount()
        {
            List<int> value_list = new List<int>(Values);
            value_list.Sort();
            value_list.Reverse();
            return value_list[0];
        }

        public List<Key> GetMaximumCountKeys()
        {
            if (Count == 0)
            {
                return null;
            }

            int maximum_count = GetMaximalCount();

            List<Key> maximum_count_keys = new List<Key>();
            foreach (Key key in Keys)
            {
                if (maximum_count == this[key])
                {
                    maximum_count_keys.Add(key);
                }
            }
            return maximum_count_keys;
        }
    

        public void Clear()
        {
            inner_dictionary.Clear();
            TotalCount = 0;
        }
    

        public bool ContainsKey(Key key)
        {
            return inner_dictionary.ContainsKey(key);
        }

        public ICollection<Key> Keys
        {
            get { return inner_dictionary.Keys; }
        }

        public bool Remove(Key key)
        {
 	        throw new NotImplementedException();
        }

        public bool TryGetValue(Key key, out int value)
        {
 	        throw new NotImplementedException();
        }

        public ICollection<int> Values
        {
	        get { throw new NotImplementedException(); }
        }

        public int this[Key key]
        {
	        get 
	        {
                return inner_dictionary[key];
            }

	        set
            {
                Increment(key);
            }
        }

     

        public bool Contains(KeyValuePair<Key,int> item)
        {
 	        throw new NotImplementedException();
        }

        public void CopyTo(KeyValuePair<Key,int>[] array, int arrayIndex)
        {
 	        throw new NotImplementedException();
        }

   

        public bool IsReadOnly
        {
	        get { throw new NotImplementedException(); }
        }

        public bool Remove(KeyValuePair<Key,int> item)
        {
 	        throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<Key,int>> GetEnumerator()
        {
 	        throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
 	        throw new NotImplementedException();
        }
    }
}
