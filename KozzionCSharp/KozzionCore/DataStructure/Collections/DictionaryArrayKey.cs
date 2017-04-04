using KozzionCore.Tools;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionCore.DataStructure.Collections
{
    public class DictionaryArrayKey<KeyElement, ValueType> :  IDictionary<KeyElement[], ValueType>
    {
        private Dictionary<int, List<Tuple<KeyElement[], ValueType>>> hash_code_to_key_value_list;
        public DictionaryArrayKey()
        {
            hash_code_to_key_value_list = new Dictionary<int, List<Tuple<KeyElement[], ValueType>>>();
        }
        public ValueType this[KeyElement[] key]
        {
            get
            {
                int hash_code = GetHashCode(key);
                foreach (Tuple<KeyElement[], ValueType> tuple in hash_code_to_key_value_list[hash_code])
                {
                    if (ToolsCollection.EqualsArray(key, tuple.Item1))
                    {
                        return tuple.Item2;
                    }
                }
                throw new Exception("No such key");
            }

            set
            {
                Add(key, value);
            }
        }

        public int Count { get; private set; }

        public bool IsReadOnly { get { return false; } }

        public ICollection<KeyElement[]> Keys
        {
            get
            {
                List<KeyElement[]> keys = new List<KeyElement[]>();
                foreach (List<Tuple<KeyElement[], ValueType>> tuple_list in hash_code_to_key_value_list.Values)
                {
                    foreach (Tuple<KeyElement[], ValueType> tuple in tuple_list)
                    {
                        keys.Add(tuple.Item1);
                    }
                }
                return keys;
            }
        }

        public ICollection<ValueType> Values
        {
            get
            {
                List<ValueType> keys = new List<ValueType>();
                foreach (List<Tuple<KeyElement[], ValueType>> tuple_list in hash_code_to_key_value_list.Values)
                {
                    foreach (Tuple<KeyElement[], ValueType> tuple in tuple_list)
                    {
                        keys.Add(tuple.Item2);
                    }
                }
                return keys;
            }
        }

        public void Add(KeyValuePair<KeyElement[], ValueType> item)
        {
            Add(item.Key, item.Value);
        }

        public void Add(KeyElement[] key, ValueType value)
        {
            int hash_code = GetHashCode(key);
            if (hash_code_to_key_value_list.ContainsKey(hash_code))
            {
                foreach (Tuple<KeyElement[], ValueType> tuple in hash_code_to_key_value_list[hash_code])
                {
                    if (ToolsCollection.EqualsArray(key, tuple.Item1))
                    {
                        throw new Exception("Value already present");
                    }
                }
            }
            else
            {
                hash_code_to_key_value_list[hash_code] = new List<Tuple<KeyElement[], ValueType>>();
            }
            hash_code_to_key_value_list[hash_code].Add(new Tuple<KeyElement[], ValueType>(key, value));
            Count++;
        }

        private int GetHashCode(KeyElement[] key)
        {
            int hash_code = 0;
            for (int index = 0; index < key.Length; index++)
            {
                hash_code += key[index].GetHashCode();
            }
            return hash_code;
        }

        public void Clear()
        {
            hash_code_to_key_value_list.Clear();
        }

        public bool Contains(KeyValuePair<KeyElement[], ValueType> item)
        {
            throw new NotImplementedException();
        }

        public bool ContainsKey(KeyElement[] key)
        {
            int hash_code = GetHashCode(key);
            if (hash_code_to_key_value_list.ContainsKey(hash_code))
            { 
                foreach (Tuple<KeyElement[], ValueType> tuple in hash_code_to_key_value_list[hash_code])
                {
                    if (ToolsCollection.EqualsArray(key, tuple.Item1))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public void CopyTo(KeyValuePair<KeyElement[], ValueType>[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public IEnumerator<KeyValuePair<KeyElement[], ValueType>> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyValuePair<KeyElement[], ValueType> item)
        {
            throw new NotImplementedException();
        }

        public bool Remove(KeyElement[] key)
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(KeyElement[] key, out ValueType value)
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
    }
}
