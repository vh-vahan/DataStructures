using DataStructures.Dictionaries__HashTables__Sets;
using DataStructures.Tree_Data_Structures;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static DataStructures.Graph;

namespace DataStructures
{
    //Minimum Spanning Tree Problem

    /*
    - List of successors – in this representation for each vertex v a list of successor vertices is kept. 

    - Adjacency matrix – the graph is represented as a square matrix g[N][N], where if there is an edge from vi to vj, then the position g[i][j] is contains the value 1. 
        If such an edge does not exist, the field g[i][j] is contains the value 0. If the graph is weighted, in the position g[i][j] we record weight of the edge, 
        and matrix is called a matrix of weights. If between two nodes in this matrix there is no edge, then it is recorded a special value meaning infinity. 
        If the graph is undirected, the adjacency matrix will be symmetrical.

    - List of the edges – it is represented through the list of ordered pairs (vi, vj), where there is an edge from vi to vj. 

    - Matrix of incidence between vertices and edges – in this case, again we are using a matrix but with dimensions g[M][N], where N is the number of vertices, 
        and M is the number of edges. Each column represents one edge, and each row a vertex. Then the column corresponding to the edge (vi, vj) 
        will contain 1 only at position i and position j, and other items in this column will contain 0. If the edge is a loop, i.e. is (vi, vi), 
        then on position i we record 2. If the graph we want to represent is oriented and we want to introduce edge from vi to vj, 
        then to position i we write 1 and to the position j we write -1. 
    */

    public class GraphAsAdjacencyMatrix
    {
        private int MaxNode = 1024;
        private int[][] childNodes;

        public GraphAsAdjacencyMatrix(int[][] childNodes)
        {
            this.childNodes = childNodes;
        }

        public void TraverseBFS(int node)
        {
            bool[] visited = new bool[MaxNode];
            Queue<int> queue = new Queue<int>();
            queue.Enqueue(node);
            visited[node] = true;
            while (queue.Count > 0)
            {
                int currentNode = queue.Dequeue();
                Console.Write("{0} ", currentNode);
                foreach (int childNode in childNodes[currentNode])
                {
                    if (!visited[childNode])
                    {
                        queue.Enqueue(childNode);
                        visited[childNode] = true;
                    }
                }
            }
        }
        public void TraverseDFS(int node)
        {
            bool[] visited = new bool[MaxNode];
            Stack<int> stack = new Stack<int>();
            stack.Push(node);
            visited[node] = true;
            while (stack.Count > 0)
            {
                int currentNode = stack.Pop();
                Console.Write("{0} ", currentNode);
                foreach (int childNode in childNodes[currentNode])
                {
                    if (!visited[childNode])
                    {
                        stack.Push(childNode);
                        visited[childNode] = true;
                    }
                }
            }
        }
    }
    public class GraphAsListOfSuccessors
    {
        private List<int>[] childNodes;

        public List<int>[] Nodes { get { return childNodes; } }


        public GraphAsListOfSuccessors(int size)
        {
            this.childNodes = new List<int>[size];
            for (int i = 0; i < size; i++)
            {
                this.childNodes[i] = new List<int>();
            }
        }
        public GraphAsListOfSuccessors(List<int>[] childNodes)
        {
            this.childNodes = childNodes;
        }

        public int Size
        {
            get { return this.childNodes.Length; }
        }


        public void AddEdge(int u, int v)
        {
            childNodes[u].Add(v);
        }

        public void RemoveEdge(int u, int v)
        {
            childNodes[u].Remove(v);
        }

        public bool HasEdge(int u, int v)
        {
            bool hasEdge = childNodes[u].Contains(v);
            return hasEdge;
        }

        public IList<int> GetSuccessors(int v)
        {
            return childNodes[v];
        }

    }


    public class Graph
    {
        public class GraphNode : IComparable
        {
            public string key;
            public int SomeData;
            public List<GraphEdge> neighbors;
            public GraphNode(string k, List<GraphEdge> n)
            {
                key = k;
                neighbors = n;
            }

            public int CompareTo(object obj)
            {
                GraphNode other = obj as GraphNode;
                if (this.SomeData > other.SomeData)
                    return 1;
                else if (this.SomeData < other.SomeData)
                    return -1;
                else
                    return 0;
            }
        }

        public class GraphEdge
        {
            public int cost;
            public GraphNode source;
            public GraphNode neighbor;
        }


        public List<GraphNode> Nodes;

    }


    public class TopoligicalSort
    {

        private void Sort(GraphAsListOfSuccessors graph, Stack<int> st, bool[] visited, int i)
        {
            visited[i] = true;

            foreach (var item in graph.Nodes[i])
            {
                if(!visited[item])
                {
                    Sort(graph, st, visited, item);
                }
            }
            st.Push(i);
        }

        public List<int> Sort(GraphAsListOfSuccessors graph)
        {
            Stack<int> st = new Stack<int>();

            bool[] visited = new bool[graph.Size];

            for (int i = 0; i < graph.Size; i++)
            {
                if(!visited[i])
                {
                    Sort(graph, st, visited, i) ;
                }
            }

            var res = st.ToList();
            return res;
        }
    }


    //Dijkstra's Algorithm to find the shortest path.
    public class ShortestPath
    {
        public Graph graph;

        private Hashtable distance = new Hashtable();
        private Hashtable route = new Hashtable();


        public Stack<string> FindShortestPath(string from, string to, out int shortDistance)
        {
            InitDistanceAndRouteTables(from);

            List<GraphNode> nodes = graph.Nodes;

            while (nodes.Count > 0)
            {
                GraphNode u = GetNodeWithMinimumValueInDistanceTable(nodes);
                nodes.Remove(u);

                foreach (GraphEdge edge in u.neighbors)
                    UpdateDistanceAndRouteTables(u, edge.neighbor, edge.cost);
            }


            shortDistance = (int)distance[to];

            Stack<string> trace = new Stack<string>();
            GraphNode current = new GraphNode(to, null);
            trace.Push(current.key);
            do
            {
                current = (GraphNode)route[current.key];
                trace.Push(current.key);
            } while (current.key != from);

            return trace;
        }

        private void InitDistanceAndRouteTables(string start)
        {
            foreach (GraphNode n in graph.Nodes)
            {
                distance.Add(n.key, Int32.MaxValue);
                route.Add(n.key, null);
            }

            distance[start] = 0;
        }
        private void UpdateDistanceAndRouteTables(GraphNode from, GraphNode to, int cost)
        {
            int distFromCity = (int)distance[from.key];
            int distToCity = (int)distance[to.key];

            if (distToCity > distFromCity + cost)
            {
                distance[to.key] = distFromCity + cost;
                route[to.key] = from;
            }
        }
        private GraphNode GetNodeWithMinimumValueInDistanceTable(List<GraphNode> nodes)
        {
            int minDist = Int32.MaxValue;
            GraphNode minNode = null;
            foreach (GraphNode node in nodes)
            {
                if (((int)distance[node.key]) <= minDist)
                {
                    minDist = (int)distance[node.key];
                    minNode = node;
                }
            }

            return minNode;
        }


    }

    //Kruskal's Algorithm
    public class MinimumSpanningTreeByKruskel
    {
        public Graph graph;

        SortedList<int, GraphEdge> edges = new SortedList<int, GraphEdge>();

        List<GraphEdge> mst = new List<GraphEdge>();

        DisjointSets<GraphNode> ds = new DisjointSets<GraphNode>();

        void InitDS()
        {
            ds.Init(graph.Nodes);
        }

        void SortEdges()
        {
            foreach (var item in graph.Nodes)
            {
                foreach (var edge in item.neighbors)
                {
                    edges.Add(edge.cost, edge);
                }
            }
        }

        bool IsCycle(GraphEdge edge)
        {
            int sourceindex = ds.Find(edge.source);
            int neighborindex = ds.Find(edge.neighbor);

            if (sourceindex != neighborindex)
            {
                ds.Union(sourceindex, neighborindex);
                return false;
            }

            return true;
        }

        public void FindMinimumSpanningTree()
        {
            SortEdges();
            foreach (var item in edges)
            {
                if (!IsCycle(item.Value))
                {
                    mst.Add(item.Value);
                }
            }

        }

    }

    //Prim's Algorithm
    public class MinimumSpanningTreeByPrim
    {
        public Graph graph;

        List<GraphNode> mst = new List<GraphNode>();

        HashSet<GraphNode> remaining = new HashSet<GraphNode>();



        void Init()
        {
            foreach (var item in graph.Nodes)
            {
                item.SomeData = int.MaxValue;
                remaining.Add(item);
            }

            AddToMST(graph.Nodes[0]);
            remaining.Remove(graph.Nodes[0]);
        }


        private void AddToMST(GraphNode node)
        {
            mst.Add(node);
            foreach (var item in node.neighbors)
            {
                if (item.neighbor.SomeData > item.cost)
                    item.neighbor.SomeData = item.cost;
            }
        }

        private GraphNode MinimumNode()
        {
            GraphNode minimum = null;
            foreach (var item in mst)
            {
                foreach (var neighborNode in item.neighbors)
                {
                    if(!mst.Contains(neighborNode.neighbor))
                    {
                        if (minimum == null || neighborNode.neighbor.SomeData < minimum.SomeData)
                            minimum = neighborNode.neighbor;
                    }
                }
            }
            return minimum;
        }



        public void FindMinimumSpanningTree()
        {
            Init();

            while (remaining.Count > 0)
            {
                GraphNode current = MinimumNode();
                AddToMST(current);
                remaining.Remove(current);
            }

        }


    }

}
