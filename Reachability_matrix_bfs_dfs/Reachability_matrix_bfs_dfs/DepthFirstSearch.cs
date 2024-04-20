namespace Reachability_matrix_bfs_dfs;
using System.Diagnostics;

public class DepthFirstSearch
{
    private  int _amountOfVertices;
    private int _amountOfNodesExplored;
    private bool[,]? _reachabilityMatrix;   //  reachability matrix will be of bool values to boost the speed
    private readonly Stack<Graph.Node> _vertexStack = new();
    private readonly HashSet<Graph.Node> _checkedVertexes = new();
    private HashSet<Graph.Node> _reachabilitySet = new();
    private readonly Stopwatch _stopwatch = new();   // will be used for retrieving exe time

    private void ResetVariablesForNewGraph(Graph graph)
    {
        _amountOfVertices = graph.VertexesSet.Count;
        _reachabilityMatrix = new bool[_amountOfVertices, _amountOfVertices];
        _amountOfNodesExplored = 0;
        _reachabilitySet.Clear();
    }
    
    //  reachability matrix will be of bool values to increase speed
    public (bool[,]? reachabilityMatrix, TimeSpan executanceTime) GetReachabilityMatrixAdjacencyLists(Graph graph)
    {
        ResetVariablesForNewGraph(graph);
        _stopwatch.Start();
        var reachabilityMatrix = GetReachabilityMatrix(graph, "LISTS");
        _stopwatch.Stop();
        return (reachabilityMatrix, _stopwatch.Elapsed);
    }
    
    public (bool[,]? reachabilityMatrix, TimeSpan executanceTime) GetReachabilityMatrixAdjacencyMatrix(Graph graph)
    {
        ResetVariablesForNewGraph(graph);
        _stopwatch.Start();
        var reachabilityMatrix = GetReachabilityMatrix(graph, "MATRIX");
        _stopwatch.Stop();
        return (reachabilityMatrix, _stopwatch.Elapsed);
    }

    private bool[,]? GetReachabilityMatrix(Graph graph, string mode)
    {
        for (int i = 0; i < _amountOfVertices; i++) // the worst scenario is performing DFS for each vertex in the graph
        {
            _checkedVertexes.Clear();       
            if (mode == "MATRIX")
                _reachabilitySet = StartDfsWithAdjacencyMatrix(graph.VertexesList[i], graph);
            else 
                _reachabilitySet = StartDfsWithAdjacencyList(graph.VertexesList[i]);
            
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
    
    private HashSet<Graph.Node> StartDfsWithAdjacencyList(Graph.Node startVertex)
    {
        _vertexStack.Push(startVertex);
        while (_vertexStack.Count > 0)
        {
            var curVertex = _vertexStack.Pop();
            _checkedVertexes.Add(curVertex);
            foreach (var neighbourVertex in curVertex.GetAdjacencyList())
            {
                if (!_checkedVertexes.Contains(neighbourVertex))
                {
                    _vertexStack.Push(neighbourVertex);
                    _checkedVertexes.Add(neighbourVertex);
                }
            }
        }

        return _checkedVertexes;
    }

    private HashSet<Graph.Node> StartDfsWithAdjacencyMatrix(Graph.Node startVertex, Graph graph)
    {
        _vertexStack.Push(startVertex);
        while (_vertexStack.Count > 0)
        {
            var curVertex = _vertexStack.Pop();
            _checkedVertexes.Add(curVertex);
            for (int index = 0; index < _amountOfVertices; index++) // for each vertex check neighbours 
            {
                if (graph.AdjacencyMatrix[graph.VertexesList.IndexOf(curVertex), index]) // if there is a neighbour
                {
                    var neighbourVertex = graph.VertexesList[index];
                    if (!_checkedVertexes.Contains(neighbourVertex))
                    {
                        _vertexStack.Push(neighbourVertex); // add to queue
                        _checkedVertexes.Add(neighbourVertex);
                    }
                }
            }
        }
        return _checkedVertexes;
    }
}