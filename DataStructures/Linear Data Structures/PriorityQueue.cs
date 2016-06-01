using DataStructures.Tree_Data_Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Linear_Data_Structures
{
    public class PriorityQueue
    {
        Heap hp = new Heap(Heap.MINSTRATEGY);
        
        public int Dequeue()
        {
            int tmp = hp[0];
            hp.Remove(0);
            return tmp;
        }


        public void Enqueue(int item)
        {
            hp.Add(item);
        }


    }
}
