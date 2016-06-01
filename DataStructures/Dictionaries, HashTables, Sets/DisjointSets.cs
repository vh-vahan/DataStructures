using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Dictionaries__HashTables__Sets
{
    public class DisjointSets<T>
    {
        bool isInitialized;

        List<HashSet<T>> sets = new List<HashSet<T>>();

        public void Init(List<T> items)
        {
            if (isInitialized)
                return;

            foreach (var item in items)
            {
                var hs = new HashSet<T>();
                hs.Add(item);
                sets.Add(hs);
            }
            isInitialized = true;
        }


        public int Find(T item)
        {
            for (int i = 0; i < sets.Count; i++)
            {
                if (sets[i].Contains(item))
                    return i;
            }

            return -1;
        }


        public void Union(int i, int j)
        {
            var si = sets[i];
            var sj = sets[j];

            if (si.Count < sj.Count)
            {
                sj.UnionWith(si);
                sets.RemoveAt(i);
            }
            else
            {
                si.UnionWith(sj);
                sets.RemoveAt(j);
            }
                
        }

    }

}
