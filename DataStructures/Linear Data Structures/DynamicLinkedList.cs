using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    /// <summary>
    /// Dynamic List (Dynamic Implementation)
    /// The LinkedList<T> class is a dynamic implementation of a doubly linked list built in .NET Framework
    /// </summary>
    public class DynamicLinkedList
    {
        private class ListNode
        {
            public int Element { get; set; }
            public ListNode NextNode { get; set; }

            public ListNode(int element)
            {
                this.Element = element;
                NextNode = null;
            }

            public ListNode(int element, ListNode previousNode)
            {
                this.Element = element;
                previousNode.NextNode = this;
            }
        }

        private ListNode head;
        private ListNode tail;
        private int count;

        public DynamicLinkedList()
        {
            this.head = null;
            this.tail = null;
            this.count = 0;
        }

        public int Count
        {
            get
            {
                return this.count;
            }
        }

        public int this[int index]
        {
            get
            {
                if (index >= count || index < 0)
                {
                    throw new ArgumentOutOfRangeException("Invalid index: " + index);
                }
                ListNode currentNode = this.head;
                for (int i = 0; i < index; i++)
                {
                    currentNode = currentNode.NextNode;
                }
                return currentNode.Element;
            }
            set
            {
                if (index >= count || index < 0)
                {
                    throw new ArgumentOutOfRangeException("Invalid index: " + index);
                }
                ListNode currentNode = this.head;
                for (int i = 0; i < index; i++)
                {
                    currentNode = currentNode.NextNode;
                }
                currentNode.Element = value;
            }
        }

        public void Add(int item)
        {
            if (this.head == null)
            {
                this.head = new ListNode(item);
                this.tail = this.head;
            }
            else
            {
                ListNode newNode = new ListNode(item, this.tail);
                this.tail = newNode;
            }
            this.count++;
        }

        public int RemoveAt(int index)
        {
            if (index >= count || index < 0)
            {
                throw new ArgumentOutOfRangeException("Invalid index: " + index);
            }

            int currentIndex = 0;
            ListNode currentNode = this.head;
            ListNode prevNode = null;
            while (currentIndex < index)
            {
                prevNode = currentNode;
                currentNode = currentNode.NextNode;
                currentIndex++;
            }

            RemoveNode(currentNode, prevNode);
            return currentNode.Element;
        }

        public int Remove(int item)
        {
            int currentIndex = 0;
            ListNode currentNode = this.head;
            ListNode prevNode = null;
            while (currentNode != null)
            {
                if (object.Equals(currentNode.Element, item))
                {
                    break;
                }
                prevNode = currentNode;
                currentNode = currentNode.NextNode;
                currentIndex++;
            }

            if (currentNode != null)
            {
                RemoveNode(currentNode, prevNode);
                return currentIndex;
            }
            else
            {
                return -1;
            }
        }

        public int IndexOf(int item)
        {
            int index = 0;
            ListNode currentNode = this.head;
            while (currentNode != null)
            {
                if (object.Equals(currentNode.Element, item))
                {
                    return index;
                }
                currentNode = currentNode.NextNode;
                index++;
            }
            return -1;
        }

        public bool Contains(int item)
        {
            int index = IndexOf(item);
            bool found = (index != -1);
            return found;
        }

        private void RemoveNode(ListNode nodeToRemove, ListNode prevNode)
        {
            count--;
            if (count == 0)
            {
                this.head = null;
                this.tail = null;
            }
            else if (prevNode == null)
            {
                this.head = nodeToRemove.NextNode;
            }
            else
            {
                prevNode.NextNode = nodeToRemove.NextNode;
            }

            if (object.ReferenceEquals(this.tail, nodeToRemove))
            {
                this.tail = prevNode;
            }
        }

    }

    public class TortoiseAndHare
    {
        public bool HasLoop(LinkedList<int> list)
        {
            LinkedListNode<int> tortoise = list.First;
            LinkedListNode<int> hare = list.First;

            while (tortoise != null && hare != null)
            {
                if (tortoise == hare)
                {
                    return true;
                }
                if (hare.Next != null)
                {
                    hare = hare.Next.Next;
                }
                tortoise = tortoise.Next;
            }
            return false;

        }

        public LinkedListNode<int> FindLoop(LinkedList<int> list)
        {
            LinkedListNode<int> tortoise = list.First;
            LinkedListNode<int> hare = list.First;

            while (hare.Next != null)
            {
                tortoise = tortoise.Next;
                hare = hare.Next.Next;
                if (tortoise == hare)
                {
                    break;
                }
            }

            if (hare.Next == null)
            {
                return null;
            }

            tortoise = list.First; 
            while (tortoise != hare)
            {
                tortoise = tortoise.Next;
                hare = hare.Next;
            }
            return hare;
        }

    }
}
