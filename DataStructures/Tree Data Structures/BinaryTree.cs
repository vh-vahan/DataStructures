using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{
    public class BinaryTree<T>
    {
        public T Value { get; set; }
        public BinaryTree<T> LeftChild { get; private set; }
        public BinaryTree<T> RightChild { get; private set; }

        public BinaryTree(T value, BinaryTree<T> leftChild, BinaryTree<T> rightChild)
        {
            this.Value = value;
            this.LeftChild = leftChild;
            this.RightChild = rightChild;
        }
        public BinaryTree(T value)
            : this(value, null, null)
        {
        }


        /// <summary>
        /// In-order (Left-Root-Right) – the traversal algorithm first traverses the left sub-tree, then the root and last the left sub-tree.
        /// Pre-order(Root-Left-Right) – in this case the algorithm first traverses the root, then the left sub-tree and last the right sub-tree.
        /// Post-order(Left-Right-Root) – here we first traverse the left sub-tree, then the right one and last the root.
        /// </summary>
        public void InOrderTraversal()
        {
            // 1. Visit the left child
            if (this.LeftChild != null)
            {
                this.LeftChild.InOrderTraversal();
            }

            // 2. Visit the root of this sub-tree
            Console.Write(this.Value + " ");

            // 3. Visit the right child
            if (this.RightChild != null)
            {
                this.RightChild.InOrderTraversal();
            }
        }
        public void PostorderTraversal()
        {
            if (this.LeftChild != null)
            {
                this.LeftChild.PostorderTraversal();
            }
            if (this.RightChild != null)
            {
                this.RightChild.PostorderTraversal();
            }
            Console.WriteLine(this.Value.ToString() + " ");

        }
        public void PreorderTraversal()
        {

            Console.WriteLine(this.Value.ToString() + " ");
            if (this.LeftChild != null)
            {
                this.LeftChild.PreorderTraversal();
            }
            if (this.RightChild != null)
            {
                this.RightChild.PreorderTraversal();
            }
            
        }




    }


}
