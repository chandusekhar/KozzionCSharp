using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionCore.DataStructure.Trie
{
    public class StringTrie
    {
        private Dictionary<string, TrieNodeString> root_nodes;


        public StringTrie(IList<string> values)
        {
            root_nodes = new Dictionary<string, TrieNodeString>();
            Add(values);
        }

        public StringTrie()
            : this(new string[] { })
        {

        }

        private void Add(IList<string> values)
        {
            foreach (string value in values)
            {
                Add(value);
            }
        }

        // returns false if value was alreaddy present
        public bool Add(string value)
        {
            if (value.Length == 0)
            {
                return false;
            }
            else
            {
                if (root_nodes.ContainsKey(value.Substring(0, 1)))
                {
                    return root_nodes[value.Substring(0, 1)].Add(value.Substring(1));
                }
                else
                {
                    root_nodes[value.Substring(0, 1)] = new TrieNodeString(value.Substring(1));
                    return true;
                }
            }
        }

        public bool Contains(string value)
        {// returns true if contains value.
            if (value.Length == 0)
            {
                return false;
            }
            if (root_nodes.ContainsKey(value.Substring(0, 1)))
            {
                return root_nodes[value.Substring(0, 1)].Contains(value.Substring(1));
            }
            else
            {
                return false;
            }

        }


        // returns a list of all contained strings starting with value.
        public List<string> StartWith(string value, bool case_sensitive = false)
        {
            List<string> list = new List<string>();
            StartWith(list, value, true);
            return list;
        }


        // returns a list of all contained strings starting with value.
        private void StartWith(List<string> list, string value, bool case_sensitive)
        {
            if (value.Length == 0)
            {
                GetAll(list);
            }
            else
            {
                if (case_sensitive)
                {
                    string key = value.Substring(0, 1);
                    if (root_nodes.ContainsKey(key))
                    {
                        root_nodes[key].StartWith(list, value.Substring(1), key, case_sensitive);
                    }
                }
                else
                {
                    string upper_case_key = value.Substring(0, 1).ToUpper();
                    if (root_nodes.ContainsKey(upper_case_key))
                    {
                        root_nodes[upper_case_key].StartWith(list, value.Substring(1), upper_case_key, case_sensitive);
                    }
                    string lower_case_key = value.Substring(0, 1).ToLower();
                    if (root_nodes.ContainsKey(lower_case_key))
                    {
                        root_nodes[lower_case_key].StartWith(list, value.Substring(1), lower_case_key, case_sensitive);
                    }
                }
            }
        }


        public List<string> GetAll()
        {
            List<string> list = new List<string>();
            GetAll(list);
            return list;
        }

        private void GetAll(List<string> list)
        {
            foreach (string key in root_nodes.Keys)
            {
                root_nodes[key].GetAll(list, key);
            }

        }
    }
}