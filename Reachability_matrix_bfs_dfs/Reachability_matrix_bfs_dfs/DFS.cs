namespace Reachability_matrix_bfs_dfs;

public class DFS
{
    public (int[,] ReachabilityMatrix, List<Graph.Node> ListOfVertexes)GetReachabilityMatrix (Graph graph)
    {
        List<Graph.Node> vertexList = graph.VertexesSet.ToList();
        int amountOfVertexes = graph.VertexesSet.Count;
        int[,] reachabilityMatrix = new int[amountOfVertexes, amountOfVertexes];
        int amountOfNodesExplored = 0;
        for (int i = 0; i < amountOfVertexes; i++)  // the worst scenario is performing DFS for each vertex in the graph
        {
            var reachabilitySet = StartDFS(vertexList[i]);
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

    private int[,] FillWithOnes(int amountOfVertices)
    {
        int[,] reachabilityMatrix = new int[amountOfVertices,amountOfVertices];
        for (int row = 0; row < amountOfVertices; row++)
        {
            for (int column = 0; column < amountOfVertices; column++)
            {
                if (row == column)
                    reachabilityMatrix[row, column] = 0;
                else
                    reachabilityMatrix[row, column] = 1;
            }
        }
        return reachabilityMatrix;
    }
    
    private int[,] FillReachabilityMatrix(int[,] reachabilityMatrix, HashSet<Graph.Node> reachableVertSet, List<Graph.Node> verticesList, int vertexAmount)
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
                    reachabilityMatrix[row, column] = 1;
                    reachabilityMatrix[column, row] = 1;
                }
            }
        }
        return reachabilityMatrix;
    }
    private HashSet<Graph.Node> StartDFS(Graph.Node startVertex)
    {
        Stack<Graph.Node> vertexStack = new();
        HashSet<Graph.Node> checkedVertexes = new();
        vertexStack.Push(startVertex);
        while (vertexStack.Count > 0)
        {
            var curVertex = vertexStack.Pop();
            checkedVertexes.Add(curVertex);
            foreach (var neighbourVertex in curVertex.GetAdjacencySet())
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
}