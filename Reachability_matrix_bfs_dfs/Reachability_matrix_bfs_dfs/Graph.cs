namespace Reachability_matrix_bfs_dfs;

public class Graph
{

    public HashSet<Node> VertexesSet { get; }  
    public HashSet<Tuple<Node, Node>> EdgesSet { get; }

    public Graph(HashSet<Node> vertexesSet, HashSet<Tuple<Node,Node>> edgesSet) // duplicates of vertexes and edges are not allowed
    {
        VertexesSet = vertexesSet;
        EdgesSet = edgesSet;
        HashSet<string> vertexNames = new();
        foreach (var vertex in vertexesSet)  // checking the names of nodes to be unique
        {
            if (!vertexNames.Add(vertex.Name))
                throw new Exception($"Two or more vertexes have the same name: {vertex.Name}");
        }
        foreach (var edge in EdgesSet)  // filling the adjacency list of each vertex
        {
            edge.Item1.AddNeighbor(edge.Item2);
            edge.Item2.AddNeighbor(edge.Item1);
        }
    }

    public  Dictionary<Node, List<Node>> GetAdjacencyLists()
    {
        Dictionary<Node, List<Node>> adjacencyDict = new();  // key is a vertex and the list of values is adjacent vertexes
        var listOfVertexes = VertexesSet.ToList();
        foreach (var vertex in listOfVertexes)
        {
            adjacencyDict.Add(vertex, vertex.GetAdjacencySet().ToList());
        }

        return adjacencyDict;
    }

 
    
    public (int[,] adjacencyMatrix, List<Node> listOfVertexes) GetAdjacencyMatrix()
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
        
        return (adjacencyMatrix, listOfVertexes);
    }
    
    public void PrintAdjacencyMatrix()
    {
        var (adjacencyMatrix, listOfVertexes) = GetAdjacencyMatrix();

        // Print the header row with vertex names
        Console.Write(" \t");
        foreach (var vertex in listOfVertexes)
        {
            Console.Write($"{vertex.Name}\t");
        }
        Console.WriteLine();

        // Print the matrix
        for (int row = 0; row < listOfVertexes.Count; row++)
        {
            Console.Write($"{listOfVertexes[row].Name}\t");

            for (int column = 0; column < listOfVertexes.Count; column++)
            {
                Console.Write($"{adjacencyMatrix[row, column]}\t");
            }

            Console.WriteLine();
        }
    }


    public void PrintAdjacencyLists()
    {
        var adjacencyDict = GetAdjacencyLists();
        Console.WriteLine("\nAdjacency lists:");
        foreach (var vertex in adjacencyDict.Keys)
        {
            Console.Write($"{vertex.Name} | ");
            foreach (var neighbour in adjacencyDict[vertex])
                Console.Write($"{neighbour.Name} ");
    
            Console.WriteLine();
        }
    }
    
    public class Node // nested class Node
        (string name)  // primary constructor to ensure that each vertex has a name
    {
        private readonly HashSet<Node> _adjacencySet = new();
        public string Name { get; } = name;

        public void AddNeighbor(Node neighbor)
        {
            _adjacencySet.Add(neighbor);
        }

        public HashSet<Node> GetAdjacencySet()
        {
            return _adjacencySet;
        }
    }

}
