namespace Reachability_matrix_bfs_dfs;

public class BreadthFirstSearch
{
    private readonly int _amountOfVertices;
    private int _amountOfNodesExplored;
    private bool[,] _reachabilityMatrix;  //  reachability matrix will be of bool values to boost the speed
    
    public BreadthFirstSearch(Graph graph)  // initialising from constructor
    {
        _amountOfVertices = graph.VertexesSet.Count;
        _reachabilityMatrix = new bool[_amountOfVertices, _amountOfVertices];
        _amountOfNodesExplored = 0;
    }
    
    public bool[,] GetReachabilityMatrixAdjacencyLists (Graph graph)
    {
        for (int i = 0; i < _amountOfVertices; i++)  // the worst scenario is performing BFS for each vertex in the graph
        {
            var reachabilitySet = StartBfsWithAdjacencyList(graph.VertexesList[i]);
            _amountOfNodesExplored += reachabilitySet.Count;
            if (reachabilitySet.Count == _amountOfVertices)
            {
                return ReachabilityMatrix.FillWithOnes(_amountOfVertices);
            }
            _reachabilityMatrix = ReachabilityMatrix.FillReachabilityMatrix(_reachabilityMatrix, reachabilitySet, graph.VertexesList, _amountOfVertices);
            
            if (_amountOfNodesExplored == _amountOfVertices)
                return _reachabilityMatrix;
        }
        return _reachabilityMatrix;
    }
    public bool[,] GetReachabilityMatrixAdjacencyMatrix (Graph graph)
    {
        for (int i = 0; i < _amountOfVertices; i++)  // the worst scenario is performing BFS for each vertex in the graph
        {
            var reachabilitySet = StartBfsWithAdjacencyMatrix(graph.VertexesList[i], graph);
            _amountOfNodesExplored += reachabilitySet.Count;
            if (reachabilitySet.Count == _amountOfVertices)
            {
                return ReachabilityMatrix.FillWithOnes(_amountOfVertices);
            }
            _reachabilityMatrix = ReachabilityMatrix.FillReachabilityMatrix(_reachabilityMatrix, reachabilitySet, graph.VertexesList, _amountOfVertices);
            
            if (_amountOfNodesExplored == _amountOfVertices)
                return _reachabilityMatrix;
        }
        return _reachabilityMatrix;
    }
    
    private HashSet<Graph.Node> StartBfsWithAdjacencyList(Graph.Node startVertex)
    {
        Queue<Graph.Node> vertexStack = new();
        HashSet<Graph.Node> checkedVertexes = new();
        vertexStack.Enqueue(startVertex);
        while (vertexStack.Count > 0)
        {
            var curVertex = vertexStack.Dequeue();
            checkedVertexes.Add(curVertex);
            foreach (var neighbourVertex in curVertex.GetAdjacencyList())
            {
                if (!checkedVertexes.Contains(neighbourVertex))
                {
                    vertexStack.Enqueue(neighbourVertex);
                    checkedVertexes.Add(neighbourVertex);
                }
            }
        }

        return checkedVertexes;
    }
    private HashSet<Graph.Node> StartBfsWithAdjacencyMatrix(Graph.Node startVertex, Graph graph)
    {
        Queue<Graph.Node> vertexStack = new();
        HashSet<Graph.Node> checkedVertexes = new();
        vertexStack.Enqueue(startVertex);
        while (vertexStack.Count > 0)
        {
            var curVertex = vertexStack.Dequeue();
            checkedVertexes.Add(curVertex);
            for(int index = 0; index < _amountOfVertices; index++)  // for each vertex check neighbours 
            {
                if (graph.AdjacencyMatrix[graph.VertexesList.IndexOf(curVertex), index])  // if there is a neighbour
                {
                    var neighbourVertex = graph.VertexesList[index];  
                    if (!checkedVertexes.Contains(neighbourVertex))
                    {
                        vertexStack.Enqueue(neighbourVertex);  // add to queue
                        checkedVertexes.Add(neighbourVertex);
                    }
                }
            }
        }
        
        return checkedVertexes;
    }
}