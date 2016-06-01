using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    /// <summary>
    /// Static Stack (Array-Based Implementation)
    /// Linked Stack (Dynamic Implementation)
    /// The Stack<T> Class is implemented statically with an array in .NET Framework
    /// </summary>
    public class StackImplementation
    {
        const int initialSize = 32;
        object[] dataArray = new object[initialSize];

        int size = 0;


        public void Push(object data)
        {
            if (dataArray.Length == size)
            {
                IncrementArray();
            }

            dataArray[size] = data;
            size++;
        }

        public object Pop()
        {
            size--;
            object data = dataArray[size];
            dataArray[size] = null;

            return data;
        }

        void IncrementArray()
        {
            int newSize = dataArray.Length * 2;
            object[] newArray = new object[newSize];
            Array.Copy(dataArray, 0, newArray, 0, size);
            dataArray = newArray;

        }


    }
}
