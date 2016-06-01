using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Tree_Data_Structures
{
    public class Trie
    {
        public class TrieNode
        {
            public TrieNode[] children;
            public bool isLeaf;
        };


        TrieNode root;

        public Trie()
        {
            root = new TrieNode() { isLeaf = false, children = new TrieNode[26] };
        }


        public void Add(string key)
        {
            TrieNode current = root;

            int last = 0;
            for (int i = 0; i < key.Length; i++)
            {
                char item = key[i];
                if (root.children[item] != null)
                    current = root.children[item];
                else
                    current.children[item] = new TrieNode() {children = new TrieNode[26] };

                last = i;
            }
            current.children[key[last]].isLeaf = true;
        }


        public bool Contains(string key)
        {
            TrieNode current = root;

            for (int i = 0; i < key.Length; i++)
            {
                char item = key[i];
                if (root.children[item] == null)
                    return false;

                current = root.children[item];
            }

            return current != null && current.isLeaf;

        }


    }
}
