namespace Reachability_matrix_bfs_dfs;
// the class ReachabilityMatrix will handle all additional processes with creating a reachability matrix  
public static class ReachabilityMatrix
{
    public static bool[,]? FillWithOnes(int amountOfVertices)
    {
        bool[,]? reachabilityMatrix = new bool[amountOfVertices,amountOfVertices];
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
    public static bool[,]? FillReachabilityMatrix(bool[,]? reachabilityMatrix, HashSet<Graph.Node> reachableVertSet, List<Graph.Node> verticesList, int vertexAmount)
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
                    reachabilityMatrix[column, row] = true;  // for filling both sides of matrix
                }
            }
        }
        return reachabilityMatrix;
    }
}