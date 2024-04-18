using Reachability_matrix_bfs_dfs;

//  testing the functionality of graph generator
var graphGenerator = new GraphGenerator();

var graph = graphGenerator.GenerateGraph(20, 4);
var matrix = graph.GetAdjacencyMatrix();
var gfs = new DFS();
var reachMatrix = gfs.GetReachabilityMatrix(graph).Item1;

for (int row = 0; row < 4; row++)
{
    for (int column = 0; column < 4; column++)
    {
        Console.Write(reachMatrix[row, column]);
    }
    Console.WriteLine();
}

graph.PrintAdjacencyLists();



