using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{

    /// <summary>
    /// Static Queue (Array-Based Implementation)
    /// Linked Queue (Dynamic Implementation)
    /// The Queue<T> Class is the static implementation of queue in .NET Framework
    /// </summary>
    public class Queue
    {
        const int initialSize = 32;
        object[] dataArray = new object[initialSize];

        int head = 0;
        int tail = 0;
        int size = 0;


        public void Enqueue(object data)
        {
            if (dataArray.Length == size)
            {
                IncrementArray();
            }

            dataArray[tail] = data;
            tail = NextIndex(tail);
            size++;
        }

        public object Dequeue()
        {
            object data = dataArray[head];
            dataArray[head] = null;
            head = NextIndex(head);
            size--;

            return data;
        }


        int NextIndex(int index)
        {
            return (index + 1) % dataArray.Length;
        }

        void IncrementArray()
        {
            int newSize = dataArray.Length * 2;
            object[] newArray = new object[newSize];

            if (head < tail)
            {
                Array.Copy(dataArray, head, newArray, 0, size);
            }
            else
            {
                Array.Copy(dataArray, head, newArray, 0, dataArray.Length - head);
                Array.Copy(dataArray, 0, newArray, dataArray.Length - head, tail);
            }

            dataArray = newArray;
            head = 0;
            tail = size;
 
        }


    }
}
