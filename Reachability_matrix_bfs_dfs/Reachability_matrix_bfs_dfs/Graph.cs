namespace Reachability_matrix_bfs_dfs;

public class Graph
{

    public HashSet<Node> VertexesSet { get; }  
    public HashSet<Tuple<Node, Node>> EdgesSet { get; }
    public bool[,] AdjacencyMatrix { get; }  // для кращого перфомансу треба додати ліст з усіма вершинами в графі
    public List<Node> VertexesList { get; }  // матрицю суміжності в клас граф і ліст сусідів в клас нод 
    // з цим усім можна буде реалізувати алгоритм пошуку по матриці суміжності, оптимізувати файли щоб функції не повторювались
    public Dictionary<Node, List<Node>> AdjacencyLists { get; }



    public Graph(HashSet<Node> vertexesSet, HashSet<Tuple<Node,Node>> edgesSet) // duplicates of vertexes and edges are not allowed
    {
        if (vertexesSet.Count == 0)
            throw new Exception("Set of vertices is empty");
        HashSet<string> vertexNames = new();
        foreach (var vertex in vertexesSet)  // checking the names of nodes to be unique
        {
            if (!vertexNames.Add(vertex.Name))
                throw new Exception($"Two or more vertexes have the same name: {vertex.Name}");
        }
        VertexesSet = vertexesSet;
        EdgesSet = edgesSet;
        foreach (var edge in EdgesSet)  // filling the adjacency list of each vertex
        {
            edge.Item1.AddNeighbor(edge.Item2);
            edge.Item2.AddNeighbor(edge.Item1);
        }

        
        VertexesList = vertexesSet.ToList();
        
        AdjacencyLists = GetAdjacencyLists();
        AdjacencyMatrix = GetAdjacencyMatrix();
    }

    public Dictionary<Node, List<Node>> GetAdjacencyLists()
    {
        Dictionary<Node, List<Node>> adjacencyDict = new();  // key is a vertex and the list of values is adjacent vertexes
        var listOfVertexes = VertexesSet.ToList();
        foreach (var vertex in listOfVertexes)
        {
            adjacencyDict.Add(vertex, vertex.GetAdjacencyList());
        }

        return adjacencyDict;
    }

 
    
    public bool[,] GetAdjacencyMatrix()
    {
        int matrixSize = VertexesList.Count;
        bool[,] adjacencyMatrix = new bool[matrixSize, matrixSize];  // the matrix of adjacency is square
        

        for (int row = 0; row < matrixSize; row++)  // this will get 0 or 1 on everything above the main diagonal 
        {
            for (int column = row + 1; column < matrixSize; column++)  // row+1 skips the diagonal
            {
                var edgeToCheck = Tuple.Create(VertexesList[row], VertexesList[column]);
                var edgeToCheckReversed = Tuple.Create(edgeToCheck.Item2, edgeToCheck.Item1);
                if (EdgesSet.Contains(edgeToCheck) || EdgesSet.Contains(edgeToCheckReversed))
                {
                    adjacencyMatrix[row, column] = true;
                }
                else
                {
                    adjacencyMatrix[row, column] = false;
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
                    adjacencyMatrix[row, column] = false;
                }
                else
                {
                    adjacencyMatrix[row, column] = adjacencyMatrix[column, row]; 
                }
            }
        }
        
        return adjacencyMatrix;
    }
    
    public void PrintAdjacencyMatrix()
    {
        // Print the header row with vertex names
        Console.Write(" \t");
        foreach (var vertex in VertexesList)
        {
            Console.Write($"{vertex.Name}\t");
        }
        Console.WriteLine();

        // Print the matrix
        for (int row = 0; row < VertexesList.Count; row++)
        {
            Console.Write($"{VertexesList[row].Name}\t");

            for (int column = 0; column < VertexesList.Count; column++)
            {
                Console.Write($"{AdjacencyMatrix[row, column]}\t");
            }

            Console.WriteLine();
        }
    }


    public void PrintAdjacencyLists()
    {
        Console.WriteLine("\nAdjacency lists:");
        foreach (var vertex in AdjacencyLists.Keys)
        {
            Console.Write($"{vertex.Name} | ");
            foreach (var neighbour in AdjacencyLists[vertex])
                Console.Write($"{neighbour.Name} ");
    
            Console.WriteLine();
        }
    }

 
    public class Node // nested class Node
        (string name)  // primary constructor to ensure that each vertex has a name
    {
        private readonly HashSet<Node> _adjacencySet = new();
        private readonly List<Node> _adjacencyList = new();
        public string Name { get; } = name;

        public void AddNeighbor(Node neighbor)
        {
            if (!_adjacencySet.Add(neighbor))
            {
                throw new Exception("The Vertex has two or more same neighbours");
            }
            _adjacencyList.Add(neighbor);
        }
        
        public HashSet<Node> GetAdjacencySet()
        {
            return _adjacencySet;
        }

        public List<Node> GetAdjacencyList()
        {
            return _adjacencyList;
        }
    }
}
