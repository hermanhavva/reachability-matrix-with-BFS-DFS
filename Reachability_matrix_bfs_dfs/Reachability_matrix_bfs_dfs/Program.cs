using Reachability_matrix_bfs_dfs;

//  testing the functionality of graph generator
var graphGenerator = new GraphGenerator();

var graph = graphGenerator.GenerateGraph(100, 20);
var matrix = graph.GetAdjacencyMatrix();


//graph.PrintAdjacencyMatrix();
graph.PrintAdjacencyLists();


