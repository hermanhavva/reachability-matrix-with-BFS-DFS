namespace Reachability_matrix_bfs_dfs;

public class BreadthFirstSearch
{
    //  reachability matrix will be of bool values to increase speed
    public (bool[,] ReachabilityMatrix, List<Graph.Node> ListOfVertexes)GetReachabilityMatrix (Graph graph)
    {
        List<Graph.Node> vertexList = graph.VertexesSet.ToList();
        int amountOfVertexes = graph.VertexesSet.Count;
        bool[,] reachabilityMatrix = new bool[amountOfVertexes, amountOfVertexes];
        int amountOfNodesExplored = 0;
        for (int i = 0; i < amountOfVertexes; i++)  // the worst scenario is performing BFS for each vertex in the graph
        {
            var reachabilitySet = StartBfsWithAdjacencyList(vertexList[i]);
            amountOfNodesExplored += reachabilitySet.Count;
            if (reachabilitySet.Count == amountOfVertexes)
            {
                return (FillWithOnes(amountOfVertexes), vertexList);
            }
            reachabilityMatrix = FillReachabilityMatrix(reachabilityMatrix, reachabilitySet, vertexList, amountOfVertexes);
            if (amountOfNodesExplored == amountOfVertexes)
                return (reachabilityMatrix, vertexList);
        }

        return (reachabilityMatrix, vertexList);
    }

    private bool[,] FillWithOnes(int amountOfVertices)
    {
        bool[,] reachabilityMatrix = new bool[amountOfVertices,amountOfVertices];
        for (int row = 0; row < amountOfVertices; row++)
        {
            for (int column = 0; column < amountOfVertices; column++)
            {
                if (row == column)
                    reachabilityMatrix[row, column] = false;
                else
                    reachabilityMatrix[row, column] = true;
            }
        }
        return reachabilityMatrix;
    }
    
    private bool[,] FillReachabilityMatrix(bool[,] reachabilityMatrix, HashSet<Graph.Node> reachableVertSet, List<Graph.Node> verticesList, int vertexAmount)
    {
        HashSet<int> indexOfVerticesSet = new();
        foreach (var vertex in reachableVertSet)
        {
            indexOfVerticesSet.Add(verticesList.IndexOf(vertex));
        }
        
        for (int row = 0; row < vertexAmount; row++)
        {
            for (int column = row + 1; column < vertexAmount; column++)
            {
                if (indexOfVerticesSet.Contains(row) && indexOfVerticesSet.Contains(column)) 
                {
                    reachabilityMatrix[row, column] = true;
                    reachabilityMatrix[column, row] = true;
                }
            }
        }
        return reachabilityMatrix;
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
            foreach (var neighbourVertex in curVertex.GetAdjacencySet())
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
}