using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionCore.DataStructure.Trie
{

    public class Trie<KeyType, ValueType>
    {
        public int Count { get; private set; }

        private Dictionary<KeyType, TrieNode<KeyType, ValueType>> root_nodes;
        private ValueType root_value;
    

        public Trie()
        {
            this.root_nodes = new Dictionary<KeyType, TrieNode<KeyType, ValueType>>();
            this.root_value = default(ValueType);
            this.Count = 0;
        }

        /**
         * @param keys
         *            a list of consecutive keys
         * @param value
         *            value to insert
         * @return returns true if key was not already in use.
         */
        public bool put(IList<KeyType> keys, ValueType value)
        {
            //
            if (keys.Count == 0)
            {
                if (root_value == null)
                {
                    root_value = value;
                    Count++;
                    return true;
                }
                else
                {
                    root_value = value;
                    return false;
                }

            }
            else
            {
                if (root_nodes.ContainsKey(keys[0]))
                {
                    if (root_nodes[keys[0]].Add(keys, 1, value))
                    {
                        Count++;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    root_nodes[keys[0]] = new TrieNode<KeyType, ValueType>(keys, 1, value);
                    Count++;
                    return true;
                }
            }
        }

        /**
         * @param keys
         *            a list of consecutive keys.
         * @return returns true if key is in use.
         */
        public bool ContainsKey(List<KeyType> keys)
        {
            // returns true if contains value.
            if (keys.Count == 0)
            {
                if (root_value == null)
                {
                    return false;
                }
                else
                {
                    return true;
                }

            }
            else
            {
                if (root_nodes.ContainsKey(keys[0]))
                {
                    return root_nodes[keys[0]].ContainsKey(keys, 1);
                }
                else
                {
                    return false;
                }
            }

        }

        /**
         * @return returns a list of alle values in the trie
         */
        public List<ValueType> getAll()
        {
            List<ValueType> list = new List<ValueType>();
            if (root_value != null)
            {
                list.Add(root_value);
            }
            GetAll(list);
            return list;
        }

        private void GetAll(List<ValueType> list)
        {
            foreach (TrieNode<KeyType, ValueType> node in root_nodes.Values)
            {
                node.GetAll(list);
            }
        }

    }
}