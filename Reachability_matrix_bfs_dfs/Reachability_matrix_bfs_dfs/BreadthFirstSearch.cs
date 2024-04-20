namespace Reachability_matrix_bfs_dfs;

public class BreadthFirstSearch
{
    private int _amountOfVertices;
    private int _amountOfNodesExplored;
    private bool[,]? _reachabilityMatrix;  //  reachability matrix will be of bool values to boost the speed
    private readonly Queue<Graph.Node> _vertexQueue = new();
    private readonly HashSet<Graph.Node> _checkedVertexes = new();
    private HashSet<Graph.Node> _reachabilitySet = new();
    
    
    private bool[,]? GetReachabilityMatrix (Graph graph, string mode)
    {
        for (int i = 0; i < _amountOfVertices; i++)  // the worst scenario is performing BFS for each vertex in the graph
        {
            _checkedVertexes.Clear();      
            if (mode == "MATRIX")
                _reachabilitySet = StartBfsWithAdjacencyMatrix(graph.VertexesList[i], graph);
            else
                _reachabilitySet = StartBfsWithAdjacencyList(graph.VertexesList[i]);
            
            _amountOfNodesExplored += _reachabilitySet.Count;
            if (_reachabilitySet.Count == _amountOfVertices)
            {
                return ReachabilityMatrix.FillWithOnes(_amountOfVertices);
            }
            _reachabilityMatrix = ReachabilityMatrix.FillReachabilityMatrix(_reachabilityMatrix, _reachabilitySet, graph.VertexesList, _amountOfVertices);
            
            if (_amountOfNodesExplored == _amountOfVertices)
                return _reachabilityMatrix;
        }
        return _reachabilityMatrix;
    }

    private void ResetVariablesForNewGraph(Graph graph)
    {
        _amountOfVertices = graph.VertexesSet.Count;
        _reachabilityMatrix = new bool[_amountOfVertices, _amountOfVertices];
        _amountOfNodesExplored = 0;
        _reachabilitySet.Clear();
    }
    public bool[,]? GetReachabilityMatrixAdjacencyLists(Graph graph)
    {
        ResetVariablesForNewGraph(graph);
        return GetReachabilityMatrix(graph, "LISTS");
    }
    
    public bool[,]? GetReachabilityMatrixAdjacencyMatrix(Graph graph)
    {
        ResetVariablesForNewGraph(graph);
        return GetReachabilityMatrix(graph, "MATRIX");
    }
    
    private HashSet<Graph.Node> StartBfsWithAdjacencyList(Graph.Node startVertex)
    {
        _vertexQueue.Enqueue(startVertex);
        while (_vertexQueue.Count > 0)
        {
            var curVertex = _vertexQueue.Dequeue();
            _checkedVertexes.Add(curVertex);
            foreach (var neighbourVertex in curVertex.GetAdjacencyList())
            {
                if (!_checkedVertexes.Contains(neighbourVertex))
                {
                    _vertexQueue.Enqueue(neighbourVertex);
                    _checkedVertexes.Add(neighbourVertex);
                }
            }
        }

        return _checkedVertexes;
    }
    private HashSet<Graph.Node> StartBfsWithAdjacencyMatrix(Graph.Node startVertex, Graph graph)
    {
        _vertexQueue.Enqueue(startVertex);
        while (_vertexQueue.Count > 0)
        {
            var curVertex = _vertexQueue.Dequeue();
            _checkedVertexes.Add(curVertex);
            for(int index = 0; index < _amountOfVertices; index++)  // for each vertex check neighbours 
            {
                if (graph.AdjacencyMatrix[graph.VertexesList.IndexOf(curVertex), index])  // if there is a neighbour
                {
                    var neighbourVertex = graph.VertexesList[index];  
                    if (!_checkedVertexes.Contains(neighbourVertex))
                    {
                        _vertexQueue.Enqueue(neighbourVertex);  // add to queue
                        _checkedVertexes.Add(neighbourVertex);
                    }
                }
            }
        }
        return _checkedVertexes;
    }
}