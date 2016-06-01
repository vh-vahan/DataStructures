using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataStructures
{


    /*
     *Balanced binary tree – a binary tree in which no leaf is at “much greater” depth than any other leaf. 
     *Perfectly balanced binary tree – binary tree in which the difference in the left and right tree nodes’ count of any node is at most one.

     * Balanced binary search trees implementations - Red-Black Trees, AA Trees and AVL Trees.
     * Non-binary balanced search trees implementations - B-Trees, B+ Trees and Interval Trees.
     
     * TreeSet<T> in .NET Framework is an implementation of a red-black tree
    */

    public class Node
    {
        public int val;            //value
        public Node left;          //left child
        public Node right;         //right child
        public int ht;             //height of the node
    }


    public class BalancedTree
    {
        //balanceFactor = height(left subtree) - height(right subtree) -1 or +1

        public static Node Insert(Node root, int val)
        {
            Node rt;
            if (root == null)
            {
                rt = new Node();
                rt.val = val;
            }
            else
            {
                rt = InsertNode(root, val);
            }


            return rt;
        }

        static int Max(int v1, int v2)
        {
            if (v1 >= v2)
                return v1;
            else
                return v2;
        }
        static Node InsertNode(Node node, int value)
        {
            Node nd = node;
            Node left = nd.left;
            Node right = nd.right;

            if (value < nd.val)
            {
                if (nd.left == null)
                {
                    nd.left = new Node();
                    nd.left.val = value;
                }
                else
                {
                    nd.left = InsertNode(left, value);
                }
            }
            else
            {
                if (nd.right == null)
                {
                    nd.right = new Node();
                    nd.right.val = value;
                }
                else
                {
                    nd.right = InsertNode(right, value);
                }
            }


            if ((GetBalanceFactor(nd) == 2) || (GetBalanceFactor(nd) == -2))
            {
                return Balance(nd);
            }
            else
            {
                AdjustHeight(nd);
                return nd;
            }

        }

        static void AdjustHeight(Node node)
        {
            int lh = 0;
            int rh = 0;

            if (node.left == null)
                lh = -1;
            else
                lh = node.left.ht;

            if (node.right == null)
                rh = -1;
            else
                rh = node.right.ht;


            node.ht = Max(lh, rh) + 1;
        }

        static int GetBalanceFactor(Node node)
        {
            if (node == null)
            {
                return 0;
            }
            else
            {
                int lh = 0;
                int rh = 0;

                if (node.left == null)
                    lh = -1;
                else
                    lh = node.left.ht;

                if (node.right == null)
                    rh = -1;
                else
                    rh = node.right.ht;

                return lh - rh;
            }
        }

        static Node Balance(Node node)
        {
            // Left Heavy Tree
            if (GetBalanceFactor(node) == 2)
            {
                if (GetBalanceFactor(node.left) == 1) // Left Left case
                {
                    node = SingleRightRotation(node);
                }
                else if (GetBalanceFactor(node.left) == -1)
                {
                    node = DoubleLeftRightRotation(node); //Left right which comes to left left
                }
            }// Right Heavy Tree
            else if (GetBalanceFactor(node) == -2)
            {
                if (GetBalanceFactor(node.right) == -1) // Right right case
                {
                    node = SingleLeftRotation(node);
                }
                else if (GetBalanceFactor(node.right) == 1)
                {
                    node = DoubleRightLeftRotation(node); // right left which comes to right right
                }
            }

            return node;
        }
        static Node DoubleLeftRightRotation(Node node) //Left right case
        {
            Node node1 = node.left.right;
            node.left.right = node1.left;
            node1.left = node.left;
            AdjustHeight(node.left);
            AdjustHeight(node);

            node.left = node1.right;
            node1.right = node;
            AdjustHeight(node);
            AdjustHeight(node1);
            node = node1;
            return node;
        }
        static Node SingleLeftRotation(Node node) //right right case
        {
            Node node1 = node.right;
            node.right = node1.left;
            node1.left = node;
            AdjustHeight(node);
            AdjustHeight(node1);
            node = node1;
            return node;
        }
        static Node DoubleRightLeftRotation(Node node) //right left case
        {
            Node node1 = node.right.left;
            node.right.left = node1.right;
            node1.right = node.right;
            AdjustHeight(node.right);
            AdjustHeight(node1);

            node.right = node1.left;
            node1.left = node;
            AdjustHeight(node);
            AdjustHeight(node1);
            node = node1;
            return node;
        }
        static Node SingleRightRotation(Node node) // Left Left case
        {
            Node node1 = node.left;
            node.left = node1.right;
            node1.right = node;
            AdjustHeight(node);
            AdjustHeight(node1);
            node = node1;
            return node;
        }


    }



}
