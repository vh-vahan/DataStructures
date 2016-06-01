using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class BinarySearchTree<T> where T : IComparable<T>
    {
        internal class BinaryTreeNode<T> : IComparable<BinaryTreeNode<T>> where T : IComparable<T>
        {
            internal T value;
            internal BinaryTreeNode<T> parent;
            internal BinaryTreeNode<T> leftChild;
            internal BinaryTreeNode<T> rightChild;

            public BinaryTreeNode(T value)
            {
                if (value == null)
                {
                    // Null values cannot be compared -> do not allow them
                    throw new ArgumentNullException("Cannot insert null value!");
                }

                this.value = value;
                this.parent = null;
                this.leftChild = null;
                this.rightChild = null;
            }

            public override string ToString()
            {
                return this.value.ToString();
            }
            public override int GetHashCode()
            {
                return this.value.GetHashCode();
            }
            public override bool Equals(object obj)
            {
                BinaryTreeNode<T> other = (BinaryTreeNode<T>)obj;
                return this.CompareTo(other) == 0;
            }
            public int CompareTo(BinaryTreeNode<T> other)
            {
                return this.value.CompareTo(other.value);
            }
        }

        private BinaryTreeNode<T> root;

        public BinarySearchTree()
        {
            this.root = null;
        }

        public void Insert(T value)
        {
            this.root = Insert(value, null, root);
        }
        private BinaryTreeNode<T> Insert(T value, BinaryTreeNode<T> parentNode, BinaryTreeNode<T> node)
        {
            if (node == null)
            {
                node = new BinaryTreeNode<T>(value);
                node.parent = parentNode;

                int compareTo = value.CompareTo(parentNode.value);
                if (compareTo < 0)
                {
                    parentNode.leftChild = node;
                }
                else if (compareTo > 0)
                {
                    parentNode.rightChild = node;
                }
            }
            else
            {
                int compareTo = value.CompareTo(node.value);
                if (compareTo < 0)
                {
                    node.leftChild = Insert(value, node, node.leftChild);
                }
                else if (compareTo > 0)
                {
                    node.rightChild = Insert(value, node, node.rightChild);
                }
            }

            return node;
        }

        private BinaryTreeNode<T> Find(T value)
        {
            BinaryTreeNode<T> node = this.root;
            while (node != null)
            {
                int compareTo = value.CompareTo(node.value);
                if (compareTo < 0)
                {
                    node = node.leftChild;
                }
                else if (compareTo > 0)
                {
                    node = node.rightChild;
                }
                else
                {
                    break;
                }
            }

            return node;
        }

        public bool Contains(T value)
        {
            bool found = this.Find(value) != null;
            return found;
        }

        public void Remove(T value)
        {
            BinaryTreeNode<T> nodeToDelete = Find(value);
            if (nodeToDelete != null)
            {
                Remove(nodeToDelete);
            }
        }
        private void Remove(BinaryTreeNode<T> node)
        {
            // If the node has two children.
            if (node.leftChild != null && node.rightChild != null)
            {
                BinaryTreeNode<T> lastLeftNode = node.rightChild;
                while (lastLeftNode.leftChild != null)
                {
                    lastLeftNode = lastLeftNode.leftChild;
                }
                node.value = lastLeftNode.value;
                //node = replacement;
                lastLeftNode = null;
            }

            // If the node has at most one child
            BinaryTreeNode<T> theChild = node.leftChild != null ? node.leftChild : node.rightChild;

            // If the element to be deleted has one child
            if (theChild != null)
            {
                theChild.parent = node.parent;

                // Handle the case when the element is the root
                if (node.parent == null)
                {
                    root = theChild;
                }
                else
                {
                    // Replace the element with its child sub-tree
                    if (node.parent.leftChild == node)
                    {
                        node.parent.leftChild = theChild;
                    }
                    else
                    {
                        node.parent.rightChild = theChild;
                    }
                }
            }
            else //has no child
            {
                // Handle the case when the element is the root
                if (node.parent == null)
                {
                    root = null;
                }
                else
                {
                    // Remove the element - it is a leaf
                    if (node.parent.leftChild == node)
                    {
                        node.parent.leftChild = null;
                    }
                    else
                    {
                        node.parent.rightChild = null;
                    }
                }
            }
        }

        public void DFSTraversal()
        {
            DFSTraversal(this.root);
            Console.WriteLine();
        }
        private void DFSTraversal(BinaryTreeNode<T> node)
        {
            if (node != null)
            {
                DFSTraversal(node.leftChild);
                Console.Write(node.value + " ");
                DFSTraversal(node.rightChild);
            }
        }


    }


}
