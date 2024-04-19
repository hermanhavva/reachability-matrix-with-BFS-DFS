namespace Reachability_matrix_bfs_dfs;

public class DepthFirstSearch
{
    //  reachability matrix will be of bool values to increase speed
    public bool[,] GetReachabilityMatrix (Graph graph)
    {
        int amountOfVertexes = graph.VertexesSet.Count;
        bool[,] reachabilityMatrix = new bool[amountOfVertexes, amountOfVertexes];
        int amountOfNodesExplored = 0;
        for (int i = 0; i < amountOfVertexes; i++)  // the worst scenario is performing DFS for each vertex in the graph
        {
            var reachabilitySet = StartDfsWithAdjacencyList(graph.VertexesList[i]);
            amountOfNodesExplored += reachabilitySet.Count;
            if (reachabilitySet.Count == amountOfVertexes)
            {
                return ReachabilityMatrix.FillWithOnes(amountOfVertexes);
            }
            reachabilityMatrix = ReachabilityMatrix.FillReachabilityMatrix(reachabilityMatrix, reachabilitySet, graph.VertexesList, amountOfVertexes);
            if (amountOfNodesExplored == amountOfVertexes)
                return reachabilityMatrix;
        }

        return reachabilityMatrix;
    }
    
    private HashSet<Graph.Node> StartDfsWithAdjacencyList(Graph.Node startVertex)
    {
        Stack<Graph.Node> vertexStack = new();
        HashSet<Graph.Node> checkedVertexes = new();
        vertexStack.Push(startVertex);
        while (vertexStack.Count > 0)
        {
            var curVertex = vertexStack.Pop();
            checkedVertexes.Add(curVertex);
            foreach (var neighbourVertex in curVertex.GetAdjacencyList()) 
            {
                if (!checkedVertexes.Contains(neighbourVertex))
                {
                    vertexStack.Push(neighbourVertex);
                    checkedVertexes.Add(neighbourVertex);
                }
            }
        }

        return checkedVertexes;
    }

    /*
    private HashSet<Graph.Node> StartDfsWithAdjacencyMatrix(Graph.Node startVertex)
    {
        Stack<Graph.Node> vertexStack = new();
        HashSet<Graph.Node> checkedVertexes = new();
        vertexStack.Push(startVertex);
        while (vertexStack.Count > 0)
        {
            var curVertex = vertexStack.Pop();
            var neighbourList = curVertex.GetAdjacencySet().ToList();  
            checkedVertexes.Add(curVertex);
            for (int i = 0; i < curVertex.GetAdjacencySet().Count; i++)
            {
                if (!checkedVertexes.Contains(neighbourList[i]))  
                {
                    vertexStack.Push(neighbourList[i]);
                    checkedVertexes.Add(neighbourList[i]);
                }
            }
        }

        return checkedVertexes;
    }

    private Graph.Node GetNextNeighbour(Graph.Node parent, int neighbourIndex)
    {
        parent.GetAdjacencySet();
        return parent.GetAdjacencySet().;
    }
    */

}
