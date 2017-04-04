using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KozzionCore.DataStructure.Trie
{
    public class TrieNodeString
    {
        Dictionary<string, TrieNodeString> child_nodes;
        bool terminal;

        public TrieNodeString(string value)
        {
            child_nodes = new Dictionary<string, TrieNodeString>();
            if (value.Length == 0)
            {
                terminal = true;
            }
            else
            {
                terminal = false;
                Add(value);
            }
        }

        public bool Add(string value)
        {
            if (value.Length == 0)
            {
                if (terminal)
                {
                    return false;
                }
                else
                {
                    terminal = true;
                    return true;
                }
            }
            else
            {
                if (child_nodes.ContainsKey(value.Substring(0, 1)))
                {
                    return child_nodes[value.Substring(0, 1)].Add(value.Substring(1));
                }
                else
                {
                    child_nodes[value.Substring(0, 1)] = new TrieNodeString(value.Substring(1));
                    return true;
                }
            }

        }

        public void GetAll(List<string> list, string acummulator)
        {
            if (terminal)
            {
                list.Add(acummulator);
            }
            foreach (string key in child_nodes.Keys)
            {
                child_nodes[key].GetAll(list, acummulator + key);
            }

        }

        public bool Contains(string value)
        {
            if (value.Length == 0)
            {
                if (terminal)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            if (child_nodes.ContainsKey(value.Substring(0, 1)))
            {
                return child_nodes[value.Substring(0, 1)].Contains(value.Substring(1));
            }
            else
            {
                return false;
            }
        }

        public void StartWith(List<string> list, string value, string acummulator, bool case_sensitive)
        {
            if (value.Length == 0)
            {
                GetAll(list, acummulator);
            }
            else
            {
                if (case_sensitive)
                {
                    string key = value.Substring(0, 1);
                    if (child_nodes.ContainsKey(key))
                    {
                        child_nodes[key].StartWith(list, value.Substring(1), acummulator + key, case_sensitive);
                    }
                }
                else
                { 
                    string upper_case_key = value.Substring(0, 1).ToUpper();
                    if (child_nodes.ContainsKey(upper_case_key))
                    {
                        child_nodes[upper_case_key].StartWith(list, value.Substring(1), acummulator + upper_case_key, false);
                    }
                    string lower_case_key = value.Substring(0, 1).ToLower();
                    if (child_nodes.ContainsKey(lower_case_key))
                    {
                        child_nodes[lower_case_key].StartWith(list, value.Substring(1), acummulator + lower_case_key, false);
                    }
                }
            }
        }
    }
}