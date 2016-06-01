using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Dictionaries__HashTables__Sets
{
    /*
     * red-black tree is an ordered binary balanced search tree
     * two most popular dictionary implementations – with a balanced tree and a hash-table 
     * In .NET Framework there are two major implementations of the interface IDictionary<K, V> 
     * -     Dictionary<K, V>  - Dictionary is an implementation by a hash-table
     * -     SortedDictionary<K, V>  - SortedDictionary is an implementation by a balanced (red-black) tree
    */

    /*
     * Set Implementations in .NET Framework
        In .NET (version 4.0 and above) there is an interface ISet<T> representing the ADT "set" and it has two standard implementation classes:
        -     HashSet<T> – hash-table based implementation of set.
        -     SortedSet<T> – red-black tree based implementation of set.
    */


    public struct KeyValuePair<TKey, TValue>
    {
        public TKey Key { get; private set; }
        public TValue Value { get; private set; }

        public KeyValuePair(TKey key, TValue value)
        {
            this.Key = key;
            this.Value = value;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append('[');
            if (this.Key != null)
            {
                builder.Append(this.Key.ToString());
            }
            builder.Append(", ");
            if (this.Value != null)
            {
                builder.Append(this.Value.ToString());
            }
            builder.Append(']');
            return builder.ToString();
        }
    }

    public class HashDictionary<K, V> : IEnumerable<KeyValuePair<K, V>>
    {
        private const int DEFAULT_CAPACITY = 16;
        private const float DEFAULT_LOAD_FACTOR = 0.75f;
        private List<KeyValuePair<K, V>>[] table;
        private float loadFactor;
        private int threshold;
        private int size;
        private int initialCapacity;

        public HashDictionary()
            : this(DEFAULT_CAPACITY, DEFAULT_LOAD_FACTOR)
        { }
        public HashDictionary(int capacity, float loadFactor)
        {
            this.initialCapacity = capacity;
            this.table = new List<KeyValuePair<K, V>>[capacity];
            this.loadFactor = loadFactor;
            this.threshold = (int)(capacity * this.loadFactor);
        }


        private List<KeyValuePair<K, V>> FindChain(K key, bool createIfMissing)
        {
            int index = key.GetHashCode();
            index = index & 0x7FFFFFFF; // clear the negative bit
            index = index % this.table.Length;
            if (this.table[index] == null && createIfMissing)
            {
                this.table[index] = new List<KeyValuePair<K, V>>();
            }
            return this.table[index] as List<KeyValuePair<K, V>>;
        }

        public V Get(K key)
        {
            List<KeyValuePair<K, V>> chain = this.FindChain(key, false);
            if (chain != null)
            {
                foreach (KeyValuePair<K, V> entry in chain)
                {
                    if (entry.Key.Equals(key))
                    {
                        return entry.Value;
                    }
                }
            }

            return default(V);
        }
        public V Set(K key, V value)
        {
            if (this.size >= this.threshold)
            {
                this.Expand();
            }

            List<KeyValuePair<K, V>> chain = this.FindChain(key, true);
            for (int i = 0; i < chain.Count; i++)
            {
                KeyValuePair<K, V> entry = chain[i];
                if (entry.Key.Equals(key))
                {
                    // Key found -> replace its value with the new value
                    KeyValuePair<K, V> newEntry = new KeyValuePair<K, V>(key, value);
                    chain[i] = newEntry;
                    return entry.Value;
                }
            }
            chain.Add(new KeyValuePair<K, V>(key, value));
            this.size++;

            return default(V);
        }
        public V this[K key]
        {
            get
            {
                return this.Get(key);
            }
            set
            {
                this.Set(key, value);
            }
        }
        public bool Remove(K key)
        {
            List<KeyValuePair<K, V>> chain = this.FindChain(key, false);

            if (chain != null)
            {
                for (int i = 0; i < chain.Count; i++)
                {
                    KeyValuePair<K, V> entry = chain[i];
                    if (entry.Key.Equals(key))
                    {
                        // Key found -> remove it
                        chain.RemoveAt(i);
                        this.size--;
                        return true;
                    }
                }
            }
            return false;
        }
        public int Count
        {
            get
            {
                return this.size;
            }
        }
        public void Clear()
        {
            this.table = new List<KeyValuePair<K, V>>[this.initialCapacity];
            this.size = 0;
        }

        private void Expand()
        {
            int newCapacity = 2 * this.table.Length;
            List<KeyValuePair<K, V>>[] oldTable = this.table;
            this.table = new List<KeyValuePair<K, V>>[newCapacity];
            this.threshold = (int)(newCapacity * this.loadFactor);
            foreach (List<KeyValuePair<K, V>> oldChain in oldTable)
            {
                if (oldChain != null)
                {
                    foreach (KeyValuePair<K, V> keyValuePair in oldChain)
                    {
                        List<KeyValuePair<K, V>> chain = FindChain(keyValuePair.Key, true);
                        chain.Add(keyValuePair);
                    }
                }
            }
        }


        IEnumerator<KeyValuePair<K, V>> IEnumerable<KeyValuePair<K, V>>.GetEnumerator()
        {
            foreach (List<KeyValuePair<K, V>> chain in this.table)
            {
                if (chain != null)
                {
                    foreach (KeyValuePair<K, V> entry in chain)
                    {
                        yield return entry;
                    }
                }
            }
        }
        IEnumerator IEnumerable.GetEnumerator()
        {
            return ((IEnumerable<KeyValuePair<K, V>>)this).GetEnumerator();
        }

    }



}
