using Reachability_matrix_bfs_dfs;

//  testing the functionality of graph generator
var graphGenerator = new GraphGenerator();
var graph = graphGenerator.GenerateGraph(50, 10);
var matrix = graph.GetAdjacencyMatrix();

for (int row = 0; row < 10; row++)
{
    for (int column = 0; column < 10; column++)
    {
        Console.Write(matrix[row,column]);
    }
    Console.Write("\n");
}
/*
var node1 = new Graph.Node();
var node2 = new Graph.Node();
var node3 = new Graph.Node();
var node4 = new Graph.Node();

HashSet<Graph.Node> vertrxSet = new();
HashSet<Tuple<Graph.Node,Graph.Node>> edgesSet = new();
vertrxSet.Add(node1);
vertrxSet.Add(node2);
vertrxSet.Add(node3);
vertrxSet.Add(node4);
var e1 = Tuple.Create(node1, node2);
var e2 = Tuple.Create(node2, node3);
var e3 = Tuple.Create(node1, node3);
edgesSet.Add(e1);
edgesSet.Add(e2);
edgesSet.Add(e3);

var graph = new Graph(vertrxSet, edgesSet);
var matrix = graph.GetAdjacencyMatrix();
for (int row = 0; row < vertrxSet.Count; row++)
{
    for (int column = 0; column < vertrxSet.Count; column++)
    {
        Console.Write(matrix[row,column]);
    }
    Console.Write("\n");
}
Console.WriteLine(node1.GetAdjacencySet());*/
