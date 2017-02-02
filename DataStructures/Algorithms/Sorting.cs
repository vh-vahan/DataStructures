using DataStructures.Tree_Data_Structures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures.Algorithms
{
    public class Sorting
    {

        public static int[] BubbleSortAsc(params int[] source)
        {
            for (int i = 1; i < source.Length; i++)
            {
                for (int j = 0; j < source.Length - i; j++)
                {
                    if (source[j] > source[j + 1])
                    {
                        int tmp = source[j];
                        source[j] = source[j + 1];
                        source[j + 1] = tmp;
                    }
                }
            }
            return source;
        }
        public static int[] BubbleSortBetter(params int[] source)
        {
            bool swapped = false;
            while (swapped)
            {
                swapped = false;
                for (int j = 0; j < source.Length - 1; j++)
                {
                    if (source[j] > source[j + 1])
                    {
                        int tmp = source[j + 1];
                        source[j] = source[j + 1];
                        source[j + 1] = tmp;

                        swapped = true;
                    }
                }
            }
            return source;
        }
        public static int[] BubbleSortOptimized(params int[] source)
        {
            bool swapped = false;
            int n = source.Length;
            while (swapped)
            {
                swapped = false;
                for (int j = 0; j < n - 1; j++)
                {
                    if (source[j] > source[j + 1])
                    {
                        int tmp = source[j + 1];
                        source[j] = source[j + 1];
                        source[j + 1] = tmp;

                        swapped = true;
                    }
                }
                n--;
            }
            return source;
        }
        public static int[] BubbleSortDesc(params int[] source)
        {
            for (int i = 1; i < source.Length; i++)
            {
                for (int j = 0; j < source.Length - i; j++)
                {
                    if (source[j] < source[j + 1])
                    {
                        int tmp = source[j];
                        source[j] = source[j + 1];
                        source[j + 1] = tmp;
                    }
                }
            }
            return source;
        }


        public static int[] MergeSort(params int[] source)
        {
            if (source.Length == 1)
            {
                return source;
            }


            int median = source.Length / 2;

            int[] left = new int[median];
            int[] right = new int[source.Length - median];

            Array.Copy(source, 0, left, 0, median);
            Array.Copy(source, median + 1, right, 0, source.Length - median);


            left = MergeSort(left);
            right = MergeSort(right);


            return MergeOrderedArrays(left, right);
        }
        private static int[] MergeOrderedArrays(int[] sourceLeft, int[] sourceRight)
        {
            int size = sourceLeft.Length + sourceRight.Length;
            int[] result = new int[size];

            int l = 0, r = 0;
            for (int i = 0; i < size; i++)
            {

                if (l == sourceLeft.Length)
                {
                    Array.Copy(sourceRight, r, result, i, sourceRight.Length - r);
                    break;
                }

                if (r == sourceRight.Length)
                {
                    Array.Copy(sourceLeft, l, result, i, sourceLeft.Length - l);
                    break;
                }


                result[i] = sourceLeft[l] < sourceRight[r] ? sourceLeft[l++] : sourceRight[r++];
            }
            return result;
        }


        public static int[] QuickSort(params int[] source)
        {
            if (source.Length <= 1)
            {
                return source;
            }

            List<int> less = new List<int>();
            List<int> greater = new List<int>();
            List<int> equal = new List<int>();

            source = MedianLeft(source);

            foreach (int item in source)
            {
                if (item < source[0])
                {
                    less.Add(item);
                }
                else if (item > source[0])
                {
                    greater.Add(item);
                }
                else
                {
                    equal.Add(item);
                }
            }

            return Concatenate(QuickSort(less.ToArray()), equal.ToArray(), QuickSort(greater.ToArray()));
        }
        static int[] Concatenate(int[] less, int[] equal, int[] greater)
        {
            List<int> concatenated = new List<int>();

            foreach (int item in less)
            {
                concatenated.Add(item);
            }

            foreach (int item in equal)
            {
                concatenated.Add(item);
            }

            foreach (int item in greater)
            {
                concatenated.Add(item);
            }

            return concatenated.ToArray();
        }
        static int[] MedianLeft(int[] source)
        {
            int middle = source.Length / 2;
            const int left = 0;
            int right = source.Length - 1;

            int tmp;
            if (source[left] > source[middle])
            {
                tmp = source[left];
                source[left] = source[middle];
                source[middle] = tmp;
            }

            if (source[left] > source[right])
            {
                tmp = source[left];
                source[left] = source[right];
                source[right] = tmp;
            }

            if (source[middle] > source[right])
            {
                tmp = source[middle];
                source[middle] = source[right];
                source[right] = tmp;
            }

            tmp = source[middle];
            source[middle] = source[left];
            source[left] = tmp;

            return source;
        }

        public static List<int> QuickSortUsingList(int[] arr)
        {
            List<int> left = new List<int>();
            List<int> equal = new List<int>();
            List<int> right = new List<int>();

            if (arr.Length > 1)
            {
                int pivot = arr[0];
                for (int i = 0; i < arr.Length; i++)
                {
                    int x = arr[i];
                    if (x < pivot)
                        left.Add(x);
                    if (x == pivot)
                        equal.Add(x);
                    if (x > pivot)
                        right.Add(x);
                }

                List<int> sorted = QuickSortUsingList(left.ToArray());
                sorted.AddRange(equal);
                sorted.AddRange(QuickSortUsingList(right.ToArray()));

                return sorted;
            }
            else
            {
                return arr.ToList();
            }
        }

        // pivot element is last element in partition(Lomuto partition scheme)
        public static void QuickSortInplace(int[] arr, int left, int right)
        {
            if (left < right)
            {
                int p = QuickSortInplacePartitioner(arr, left, right);
                QuickSortInplace(arr, left, p - 1);
                QuickSortInplace(arr, p + 1, right);
            }
        }
        static int QuickSortInplacePartitioner(int[] arr, int left, int right)
        {
            int pivot = arr[right];
            int i = left;
            for (int j = left; j <= right - 1; j++)
            {
                if (arr[j] <= pivot)
                {
                    int temp1 = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp1;

                    i++;
                }
            }

            int temp = arr[i];
            arr[i] = arr[right];
            arr[right] = temp;

            return i;

        }


        //O(n^2) best O(n)
        public static int[] InsertionSort(int[] source)
        {
            for (int i = 1; i < source.Length; i++)
            {
                int position = i;
                while (position > 0 && source[position - 1] > source[position])
                {
                    int tmp = source[position];
                    source[position] = source[position - 1];
                    source[position - 1] = tmp;

                    position--;
                }
            }
            return source;
        }
        public static int[] InsertionSortBetter(int[] source)
        {
            for (int i = 1; i < source.Length; i++)
            {
                int tmp = source[i];
                int j = i - 1;
                while (j >= 0 && source[j] > tmp)
                {
                    source[j + 1] = source[j];
                    j--;
                }

                source[j + 1] = tmp;
            }
            return source;
        }


        //O(n^2)
        public static int[] SelectionSort(int[] source)
        {
            for (int i = 0; i < source.Length - 1; i++)
            {
                int min = i;
                for (int j = i + 1; j < source.Length; j++)
                {
                    if (source[min] >= source[j])
                        min = j;
                }

                if (min != i)
                {
                    int tmp = source[min];
                    source[i] = source[min];
                    source[min] = tmp;
                }
            }
            return source;
        }


        public static int[] ShellSort(int[] source)
        {
            int n = source.Length;
            int gap = n / 2;
            int temp;

            while (gap > 0)
            {
                for (int i = 0; i + gap < n; i++)
                {
                    int j = i + gap;
                    temp = source[j];

                    while (j - gap >= 0 && temp < source[j - gap])
                    {
                        source[j] = source[j - gap];
                        j = j - gap;
                    }

                    source[j] = temp;
                }

                gap = gap / 2;
            }

            return source;
        }


        public static int[] HeapSort(int[] source)
        {
            Heap hp = new Heap(Heap.MINSTRATEGY);

            for (int i = 0; i < source.Length; i++)
            {
                hp.Add(source[i]);
            }

            List<int> sorted = new List<int>();
            while (hp.Count > 0)
            {
                sorted.Add(hp[0]);
                hp.Remove(0);
            }

            return sorted.ToArray();
        }


        public static int[] CountingSort(int[] source)
        {
            int range = 10;
            int[] counts = new int[range];

            for (int i = 0; i < source.Length; i++)
            {
                counts[source[i]]++;
            }

            for (int i = 0; i < range; i++)
            {
                counts[i] += counts[i - 1];
            }

            int[] sorted = new int[source.Length];
            for (int i = 0; i < sorted.Length; i++)
            {
                sorted[counts[source[i]] - 1] = source[i];
                counts[source[i]]--;
            }

            return sorted;
        }


        public static void RadixSort(int[] source, int max)
        {
            for (int i = 1; max / i > 0; i = i * 10)
            {
                CountingSortByDigits(source, i);
            }
        }
        public static void CountingSortByDigits(int[] source, int dig)
        {
            int range = 10;
            int[] counts = new int[range];

            for (int i = 0; i < source.Length; i++)
            {
                counts[(source[i] / dig) % 10]++;
            }

            for (int i = 0; i < range; i++)
            {
                counts[i] += counts[i - 1];
            }

            int[] sorted = new int[source.Length];
            for (int i = 0; i < sorted.Length; i++)
            {
                sorted[counts[(source[i] / dig) % 10] - 1] = source[i];
                counts[(source[i] / dig) % 10]--;
            }

            for (int i = 0; i < source.Length; i++)
            {
                source[i] = sorted[i];
            }
        }


    }
}
