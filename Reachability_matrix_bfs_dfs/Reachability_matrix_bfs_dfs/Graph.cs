namespace Reachability_matrix_bfs_dfs;

public class Graph
{

    public HashSet<Node> VertexesSet { get; }
    public HashSet<Tuple<Node, Node>> EdgesSet { get; }

    public Graph(HashSet<Node> vertexesSet, HashSet<Tuple<Node,Node>> edgesSet) // duplicates of vertexes and edges are not allowed
    {
        VertexesSet = vertexesSet;
        EdgesSet = edgesSet;
        foreach (var edge in EdgesSet)  // filling the adjacency list of each vertex
        {
            edge.Item1.AddNeighbor(edge.Item2);
            edge.Item2.AddNeighbor(edge.Item1);
        }
    }


    public int[,] GetAdjacencyMatrix()
    {
        int matrixSize = VertexesSet.Count;
        int[,] adjacencyMatrix = new int[matrixSize, matrixSize];  // the matrix of adjacency is square
        List<Node> listOfVertexes = VertexesSet.ToList();  // list is needed to get a vertex by its index

        for (int row = 0; row < matrixSize; row++)  // this will get 0 or 1 on everything above the main diagonal 
        {
            for (int column = row + 1; column < matrixSize; column++)  // row+1 skips the diagonal
            {
                var edgeToCheck = Tuple.Create(listOfVertexes[row], listOfVertexes[column]);
                var edgeToCheckReversed = Tuple.Create(edgeToCheck.Item2, edgeToCheck.Item1);
                if (EdgesSet.Contains(edgeToCheck) || EdgesSet.Contains(edgeToCheckReversed))
                {
                    adjacencyMatrix[row, column] = 1;
                }
                else
                {
                    adjacencyMatrix[row, column] = 0;
                }
            }
        }
        //  now fill the second part of adjacency matrix accordingly to the first + main diagonal with 0 
        for (int row = 0; row < matrixSize; row++)
        {
            for (int column = 0; column <= row; column++)
            {
                if (row == column)
                {
                    adjacencyMatrix[row, column] = 0;
                }
                else
                {
                    adjacencyMatrix[row, column] = adjacencyMatrix[column, row]; 
                }
            }
        }
        return adjacencyMatrix;
    }


public class Node // nested class Node
    {
        private HashSet<Node> _adjacencySet = new HashSet<Node>();
        

        public void AddNeighbor(Node neighbor)
        {
            _adjacencySet.Add(neighbor);
        }

        public HashSet<Node> GetAdjacencySet()
        {
            return _adjacencySet;
        }
    }

    public class Edge // nested class Edge
    {
        public Node Vertex1;
        public Node Vertex2;

        public Edge(Node adjacentVertex1, Node adjacentVertex2)
        {
            Vertex1 = adjacentVertex1;
            Vertex2 = adjacentVertex2;
        }
        
    }

}
