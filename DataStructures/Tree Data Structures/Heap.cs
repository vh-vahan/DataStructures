using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Tree_Data_Structures
{
    public class Heap
    {
        public const string MINSTRATEGY = "MIN";
        public const string MAXSTRATEGY = "MAX";

        readonly string strategy; //MIN, MAX
        int[] data;
        int count = 0;

        public Heap()
        {
            data = new int[4];
            strategy = MINSTRATEGY;
        }
        public Heap(string strategy)
        {
            data = new int[4];
            this.strategy = strategy;
        }

        public int Count
        {
            get { return count; }
        }
        public int this[int index]
        {
            get { return data[index]; }
        }

        public void Add(int item)
        {
            if (count == data.Length)
            {
                Array.Resize(ref data, data.Length * 2);
            }

            data[count] = item;
            count++;

            if (strategy == MINSTRATEGY)
            {
                HeapifyAsMin();
            }
            else
            {
                HeapifyAsMax();
            }
        }

        public bool Remove(int item)
        {
            int index = Array.IndexOf(data, item);
            if (index < 0)
            {
                return false;
            }

            count--;

            data[index] = data[count];

            if (strategy == MINSTRATEGY)
            {
                HeapifyAsMinStartingFromToDown(index);
            }
            else
            {
                HeapifyAsMaxStartingFromToDown(index);
            }

            return true;
        }




        public bool Contains(int item)
        {
            if (strategy == MINSTRATEGY)
            {
                return ContainsForMin(0, item);
            }
            else
            {
                return ContainsForMax(0, item);
            }
        }
        bool ContainsForMin(int index, int item)
        {
            if (index > count)
                return false;

            if (data[index] == item)
                return true;
            if (data[index] > item)
                return false;

            if (ContainsForMin(LeftChildIndex(index), item))
                return true;

            if (ContainsForMin(RightChildIndex(index), item))
                return true;

            return false;

            //return ContainsForMin(LeftChildIndex(index), item) || ContainsForMin(RightChildIndex(index), item);
        }
        bool ContainsForMax(int index, int item)
        {
            if (index > count)
                return false;

            if (data[index] == item)
                return true;
            if (data[index] < item)
                return false;

            if (ContainsForMin(LeftChildIndex(index), item))
                return true;

            if (ContainsForMin(RightChildIndex(index), item))
                return true;

            return false;


            //return ContainsForMax(LeftChildIndex(index), item) || ContainsForMax(RightChildIndex(index), item);
        }

        public void Clear(int item)
        {
            count = 0;
            data = new int[4];
        }



        void HeapifyAsMin()
        {
            int index = count - 1;
            while (index > 0 && data[index] < data[ParentIndex(index)])
            {
                int tmp = data[index];
                data[index] = data[ParentIndex(index)];
                data[ParentIndex(index)] = tmp;

                index = ParentIndex(index);
            }
        }

        void HeapifyAsMax()
        {
            int index = count - 1;
            while (index > 0 && data[index] < data[ParentIndex(index)])
            {
                int tmp = data[index];
                data[index] = data[ParentIndex(index)];
                data[ParentIndex(index)] = tmp;

                index = ParentIndex(index);
            }
        }


        void HeapifyAsMinStartingFromToDown(int index)
        {
            while (LeftChildIndex(index) < count && (data[index] > data[LeftChildIndex(index)] || data[index] > data[RightChildIndex(index)]))
            {
                if (data[LeftChildIndex(index)] < data[RightChildIndex(index)])
                {
                    int tmp = data[LeftChildIndex(index)];
                    data[index] = data[LeftChildIndex(index)];
                    data[LeftChildIndex(index)] = tmp;
                    index = LeftChildIndex(index);
                }
                else
                {
                    int tmp = data[RightChildIndex(index)];
                    data[index] = data[RightChildIndex(index)];
                    data[RightChildIndex(index)] = tmp;
                    index = RightChildIndex(index);
                }
            }
        }

        void HeapifyAsMaxStartingFromToDown(int index)
        {
            while (LeftChildIndex(index) < count && (data[index] < data[LeftChildIndex(index)] || data[index] < data[RightChildIndex(index)]))
            {
                if (data[LeftChildIndex(index)] > data[RightChildIndex(index)])
                {
                    int tmp = data[LeftChildIndex(index)];
                    data[index] = data[LeftChildIndex(index)];
                    data[LeftChildIndex(index)] = tmp;
                    index = LeftChildIndex(index);
                }
                else
                {
                    int tmp = data[RightChildIndex(index)];
                    data[index] = data[RightChildIndex(index)];
                    data[RightChildIndex(index)] = tmp;
                    index = RightChildIndex(index);
                }
            }
        }


        int LeftChildIndex(int index)
        {
            return 2 * index + 1;
        }
        int RightChildIndex(int index)
        {
            return 2 * index + 2;
        }
        int ParentIndex(int index)
        {
            return (index - 1) / 2;
        }


    }


    public class Heap<T> where T : class, IComparable
    {
        public const string MINSTRATEGY = "MIN";
        public const string MAXSTRATEGY = "MAX";

        readonly string strategy; //MIN, MAX
        T[] data;
        int count = 0;

        public Heap()
        {
            data = new T[4];
            strategy = MINSTRATEGY;
        }
        public Heap(string strategy)
        {
            data = new T[4];
            this.strategy = strategy;
        }

        public int Count
        {
            get { return count; }
        }
        public T this[int index]
        {
            get { return data[index]; }
        }

        public void Add(T item)
        {
            if (count == data.Length)
            {
                Array.Resize(ref data, data.Length * 2);
            }

            data[count] = item;
            count++;

            if (strategy == MINSTRATEGY)
            {
                HeapifyAsMin();
            }
            else
            {
                HeapifyAsMax();
            }
        }

        public bool Remove(int item)
        {
            int index = Array.IndexOf(data, item);
            if (index < 0)
            {
                return false;
            }

            count--;

            data[index] = data[count];

            if (strategy == MINSTRATEGY)
            {
                HeapifyAsMinStartingFromToDown(index);
            }
            else
            {
                HeapifyAsMaxStartingFromToDown(index);
            }

            return true;
        }




        public bool Contains(T item)
        {
            if (strategy == MINSTRATEGY)
            {
                return ContainsForMin(0, item);
            }
            else
            {
                return ContainsForMax(0, item);
            }
        }
        bool ContainsForMin(int index, T item)
        {
            if (index > count)
                return false;

            if (data[index] == item)
                return true;

            if (data[index].CompareTo(item) > 0) //data[index] > item
                return false;

            if (ContainsForMin(LeftChildIndex(index), item))
                return true;

            if (ContainsForMin(RightChildIndex(index), item))
                return true;

            return false;

            //return ContainsForMin(LeftChildIndex(index), item) || ContainsForMin(RightChildIndex(index), item);
        }
        bool ContainsForMax(int index, T item)
        {
            if (index > count)
                return false;

            if (data[index] == item)
                return true;
            if (data[index].CompareTo(item) < 0) //data[index] < item
                return false;

            if (ContainsForMin(LeftChildIndex(index), item))
                return true;

            if (ContainsForMin(RightChildIndex(index), item))
                return true;

            return false;


            //return ContainsForMax(LeftChildIndex(index), item) || ContainsForMax(RightChildIndex(index), item);
        }

        public void Clear(int item)
        {
            count = 0;
            data = new T[4];
        }



        void HeapifyAsMin()
        {
            int index = count - 1;
            while (index > 0 && data[index].CompareTo(data[ParentIndex(index)]) < 0) //data[index] < data[ParentIndex(index)]
            {
                T tmp = data[index];
                data[index] = data[ParentIndex(index)];
                data[ParentIndex(index)] = tmp;

                index = ParentIndex(index);
            }
        }

        void HeapifyAsMax()
        {
            int index = count - 1;
            while (index > 0 && data[index].CompareTo(data[ParentIndex(index)]) > 0) //data[index] < data[ParentIndex(index)]
            {
                T tmp = data[index];
                data[index] = data[ParentIndex(index)];
                data[ParentIndex(index)] = tmp;

                index = ParentIndex(index);
            }
        }


        void HeapifyAsMinStartingFromToDown(int index)
        {
            while (LeftChildIndex(index) < count && (data[index].CompareTo( data[LeftChildIndex(index)]) > 0 || data[index].CompareTo( data[RightChildIndex(index)]) > 0)) //data[index] > data[LeftChildIndex(index)] || data[index] > data[RightChildIndex(index)]
            {
                if (data[LeftChildIndex(index)].CompareTo(data[RightChildIndex(index)]) < 0) //data[LeftChildIndex(index)] < data[RightChildIndex(index)]
                {
                    T tmp = data[LeftChildIndex(index)];
                    data[index] = data[LeftChildIndex(index)];
                    data[LeftChildIndex(index)] = tmp;
                    index = LeftChildIndex(index);
                }
                else
                {
                    T tmp = data[RightChildIndex(index)];
                    data[index] = data[RightChildIndex(index)];
                    data[RightChildIndex(index)] = tmp;
                    index = RightChildIndex(index);
                }
            }
        }

        void HeapifyAsMaxStartingFromToDown(int index)
        {
            while (LeftChildIndex(index) < count && (data[index].CompareTo( data[LeftChildIndex(index)] )< 0 || data[index].CompareTo( data[RightChildIndex(index)]) < 0)) //data[index] < data[LeftChildIndex(index)] || data[index] < data[RightChildIndex(index)]
            {
                if (data[LeftChildIndex(index)].CompareTo(data[RightChildIndex(index)]) > 0) //data[LeftChildIndex(index)] > data[RightChildIndex(index)]
                {
                    T tmp = data[LeftChildIndex(index)];
                    data[index] = data[LeftChildIndex(index)];
                    data[LeftChildIndex(index)] = tmp;
                    index = LeftChildIndex(index);
                }
                else
                {
                    T tmp = data[RightChildIndex(index)];
                    data[index] = data[RightChildIndex(index)];
                    data[RightChildIndex(index)] = tmp;
                    index = RightChildIndex(index);
                }
            }
        }


        int LeftChildIndex(int index)
        {
            return 2 * index + 1;
        }
        int RightChildIndex(int index)
        {
            return 2 * index + 2;
        }
        int ParentIndex(int index)
        {
            return (index - 1) / 2;
        }


    }


}
