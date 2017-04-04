
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionCore.DataStructure.Map
{

    public class DictionaryDualKey<KeyType0, KeyType1, ValueType> 
    {
        Dictionary<KeyType0, Dictionary<KeyType1, ValueType>> primary_map;

        public DictionaryDualKey()
        {

            primary_map = new Dictionary<KeyType0, Dictionary<KeyType1, ValueType>>();
        }


		public void Clear()
        {
            primary_map.Clear();
    
        }


		public bool ContainsKeyPair(KeyType0 key_0, KeyType1 key_1)
        {
            if (!primary_map.ContainsKey(key_0))
            {
                return false;
            }
            else
            {
                return primary_map[key_0].ContainsKey(key_1);
            }
        }

 
		public bool Contains(ValueType value)
        {
            throw new NotImplementedException();
            //for (ValueType stored_value : values)
            //{
            //    if (stored_value.equals(value))
            //    {
            //        return true;
            //    }
            //}
            //return false;
        }

 
		public ValueType Get(KeyType0 key_0, KeyType1 key_1)
        {
            if (ContainsKeyPair(key_0, key_1))
            {
                return primary_map[key_0][key_1];
            }
            else
            {
                return default(ValueType);
            }
        }

 
		public bool is_empty()
        {
            return primary_map.Count == 0;
        }

 
		public HashSet<Tuple<KeyType0, KeyType1>> KeyPairSet()
        {
            HashSet< Tuple < KeyType0, KeyType1 >> key_pair_set = new HashSet<Tuple<KeyType0, KeyType1>>();
            HashSet< KeyType0 > key_set_0 = new HashSet<KeyType0>(primary_map.Keys);
            foreach (KeyType0 key_0 in key_set_0)
            {
                HashSet< KeyType1 > key_set_1 = new HashSet<KeyType1>(primary_map[key_0].Keys);
                foreach (KeyType1 key_1 in key_set_1)
                {
                    key_pair_set.Add(new Tuple<KeyType0, KeyType1>(key_0, key_1));
                }
            }
            return key_pair_set;
        }

 
		public HashSet<KeyType0> Key0Set()
        {
            return new HashSet<KeyType0>(primary_map.Keys);
        }

 
		public HashSet<KeyType1> Key1Set()
        {
            HashSet< KeyType1 > key_set_1 = new HashSet<KeyType1>();
            foreach (Dictionary<KeyType1,ValueType> value  in  primary_map.Values)
            {
                foreach (KeyType1 key in value.Keys)
                {
                    key_set_1.Add(key);
                }
            }
            return key_set_1;
        }

 
		public void Add(KeyType0 key_0, KeyType1 key_1, ValueType value)
        {
            if (!primary_map.ContainsKey(key_0))
            {
                Dictionary< KeyType1, ValueType > secondary_map = new Dictionary<KeyType1, ValueType>();
                primary_map[key_0] =  secondary_map;
                secondary_map[key_1] = value;
            }
            else
            {
                primary_map[key_0][key_1] = value;
            }
        }

 
		public ValueType Remove(KeyType0 key_0, KeyType1 key_1)
        {
            ValueType value = default(ValueType);
            if (primary_map.ContainsKey(key_0))
            {
                Dictionary< KeyType1, ValueType > secondary_map = primary_map[key_0];
                value = secondary_map[key_1];
				secondary_map.Remove(key_1);

                if (secondary_map.Count == 0)
                {
                    primary_map.Remove(key_0);
                }
            }
            return value;
        }

 
		public int Count()
        {
			throw new NotImplementedException();
            //return values.size();
        }

 
		public List<ValueType> Values()
        {
			throw new NotImplementedException();
        //return new ArrayList<ValueType>(values);
        }

    }
}