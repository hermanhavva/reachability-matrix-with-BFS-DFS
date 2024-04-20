using Reachability_matrix_bfs_dfs;

//  analysing and gathering execution time per algorithm
var graphGenerator = new GraphGenerator();
TimeSpan totalTimeBfsOnMatrix = new();
TimeSpan totalTimeBfsOnLists = new();
TimeSpan totalTimeDfsOnMatrix = new();
TimeSpan totalTimeDfsOnLists = new();


BreadthFirstSearch bfs = new();
DepthFirstSearch dfs = new();
int numberOfExperiments = 40;  // 40 повторів експериментів

for (int counter = 1; counter <= numberOfExperiments; counter++)
{
    Graph graph = graphGenerator.GenerateGraph(50, 100);   //  сюди вводиш параметри графа
    
    totalTimeBfsOnMatrix += bfs.GetReachabilityMatrixAdjacencyMatrix(graph).executanceTime;
    totalTimeBfsOnLists += bfs.GetReachabilityMatrixAdjacencyLists(graph).executanceTime;

    totalTimeDfsOnMatrix += dfs.GetReachabilityMatrixAdjacencyMatrix(graph).executanceTime;
    totalTimeDfsOnLists += dfs.GetReachabilityMatrixAdjacencyLists(graph).executanceTime;
    if (counter == numberOfExperiments)
    {
        Console.WriteLine($"Average time for BFS on Matrix: {totalTimeBfsOnMatrix/numberOfExperiments}\n");
        Console.WriteLine($"Average time for BFS on Lists: {totalTimeBfsOnLists/numberOfExperiments}\n");
        
        Console.WriteLine($"Average time for DFS on Matrix: {totalTimeDfsOnMatrix/numberOfExperiments}\n");
        Console.WriteLine($"Average time for DFS on Lists: {totalTimeDfsOnLists/numberOfExperiments}\n");
    }
} 





