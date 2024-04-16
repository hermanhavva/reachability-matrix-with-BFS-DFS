namespace Reachability_matrix_bfs_dfs;

public class Graph
{

    public List<Node> Vertexes;
    public List<Edge> Edges;

    public Graph(List<Node> vertexes, List<Edge> edges)
    {
        Vertexes = vertexes;
        Edges = edges;
    }
    public class Node // nested class Node
    {
        public List<Node> AdjacencyList = new List<Node>();

        public void AddNeighbor(Node neighbor)
        {
            AdjacencyList.Add(neighbor);
        }
    }

    public class Edge // nested class Edge
    {
        public Node AdjacentVertex1;
        public Node AdjacentVertex2;

        public Edge(Node adjacentVertex1, Node adjacentVertex2)
        {
            AdjacentVertex1 = adjacentVertex1;
            AdjacentVertex2 = adjacentVertex2;
        }
    }
}

