using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionCore.DataStructure.Trie
{

    public class TrieNode<KeyType, ValueType>
    {
        private Dictionary<KeyType, TrieNode<KeyType, ValueType>> child_nodes;
        private ValueType value;

        public TrieNode(IList<KeyType> keys, int current_key_index, ValueType value)
        {
            child_nodes = new Dictionary<KeyType, TrieNode<KeyType, ValueType>>();
            if (current_key_index == keys.Count)
            {
                // terminal
                this.value = value;
            }
            else
            {
                // non terminal
                KeyType next_key = keys[current_key_index];
                if (child_nodes.ContainsKey(next_key))
                {
                    child_nodes[next_key].Add(keys, current_key_index + 1, value);
                }
                else
                {
                    child_nodes[next_key] = new TrieNode<KeyType, ValueType>(keys, current_key_index + 1, value);
                }
            }
        }

        // Returns true if value was replaced
        public bool Add(IList<KeyType> keys, int current_key_index, ValueType value)
        {
            if (current_key_index == keys.Count)
            {
                // terminal
                if (this.value == null)
                {
                    this.value = value;
                    return false;
                }
                else
                {
                    this.value = value;
                    return true;
                }
            }
            else
            {
                // non terminal
                KeyType next_key = keys[current_key_index];
                if (child_nodes.ContainsKey(next_key))
                {
                    return child_nodes[next_key].Add(keys, current_key_index + 1, value);
                }
                else
                {
                    child_nodes[next_key] = new TrieNode<KeyType, ValueType>(keys, current_key_index + 1, value);
                    return false;
                }
            }

        }

        public void GetAll(List<ValueType> values)
        {
            if (value == null)
            {
                values.Add(value);
            }

            foreach(TrieNode< KeyType, ValueType > child in child_nodes.Values)
            {
                child.GetAll(values);
            }

        }

        public bool ContainsKey(List<KeyType> keys, int current_key_index)
        {
            if (current_key_index == keys.Count)
            {
                // terminal
                if (value == null)
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
                // non terminal
                KeyType next_key = keys[current_key_index];
                if (child_nodes.ContainsKey(next_key))
                {
                    return child_nodes[next_key].ContainsKey(keys, current_key_index + 1);
                }
                else
                {
                    return false;
                }
            }
        }
    }
}