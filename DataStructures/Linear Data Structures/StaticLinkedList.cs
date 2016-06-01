using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    /// <summary>
    /// Static List (Array-Based Implementation)
    /// ArrayList,  List<T> classes are a static implementations of a linked list built in .NET Framework
    /// </summary>
    public class StaticLinkedList
    {
        private const int INITIAL_CAPACITY = 4;
        private const int REDIMENSION_COEFFICIENT = 2;
        private int[] array;
        private int count;

        public StaticLinkedList(int capacity = INITIAL_CAPACITY)
        {
            this.array = new int[capacity];
            this.count = 0;
        }

        public int this[int index]
        {
            get
            {
                if (index >= this.count || index < 0)
                {
                    throw new ArgumentOutOfRangeException("Invalid index: " + index);
                }
                return this.array[index];
            }
            set
            {
                if (index >= this.count || index < 0)
                {
                    throw new ArgumentOutOfRangeException("Invalid index: " + index);
                }
                this.array[index] = value;
            }
        }

        public int Count
        {
            get
            {
                return this.count;
            }
        }

        public void Add(int item)
        {
            Redimension();
            this.array[this.count] = item;
            this.count++;
        }

        public void Insert(int index, int item)
        {
            if (index > this.count || index < 0)
            {
                throw new IndexOutOfRangeException("Invalid index: " + index);
            }
            Redimension();
            Array.Copy(this.array, index, this.array, index + 1, this.count - index);
            this.array[index] = item;
            this.count++;
        }

        public void Clear()
        {
            this.array = new int[INITIAL_CAPACITY];
            this.count = 0;
        }

        public int IndexOf(int item)
        {
            for (int i = 0; i < this.array.Length; i++)
            {
                if (object.Equals(item, this.array[i]))
                {
                    return i;
                }
            }

            return -1;
        }

        public bool Contains(int item)
        {
            int index = IndexOf(item);
            bool found = (index != -1);
            return found;
        }

        public int RemoveAt(int index)
        {
            if (index >= this.count || index < 0)
            {
                throw new ArgumentOutOfRangeException("Invalid index: " + index);
            }

            int item = this.array[index];
            Array.Copy(this.array, index + 1, this.array, index, this.count - index - 1);
            this.array[this.count - 1] = 0;
            this.count--;

            return item;
        }

        public int Remove(int item)
        {
            int index = IndexOf(item);
            if (index != -1)
            {
                this.RemoveAt(index);
            }
            return index;
        }

        private void Redimension()
        {
            if (this.count + 1 > this.array.Length)
            {
                int[] extendedArray = new int[this.array.Length * REDIMENSION_COEFFICIENT];
                Array.Copy(this.array, extendedArray, this.count);
                this.array = extendedArray;
            }
        }

    }



}
