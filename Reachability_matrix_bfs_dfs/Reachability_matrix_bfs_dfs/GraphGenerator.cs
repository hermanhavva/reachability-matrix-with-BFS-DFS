namespace Reachability_matrix_bfs_dfs;

public class GraphGenerator  // this class will automate the process of generating the graph
{

    public Graph GenerateGraph(int density, int vertexAmount)
    {
        var vertexSet = GenerateVertexSet(vertexAmount);
        var edgeSet = GenerateEdgeSet(density, vertexSet);
        var graph = new Graph(vertexSet, edgeSet);

        return graph;
    }
    private HashSet<Tuple<Graph.Node,Graph.Node>> GenerateEdgeSet(int density, HashSet<Graph.Node> vertexSet)
    {
        HashSet<Tuple<Graph.Node,Graph.Node>> edgeSet = new();
        foreach (var startVertex in vertexSet)
        {
            foreach (var finishVertex in vertexSet)
            {
                var edge = Tuple.Create(startVertex, finishVertex);
                var edgeReversed = Tuple.Create(finishVertex, startVertex);
                
                if (!(edgeSet.Contains(edge) || edgeSet.Contains(edgeReversed)) && startVertex != finishVertex)
                {
                    if (GenerateWithProbability(density))
                        edgeSet.Add(edge);
                }
            }
        }

        return edgeSet;
    }
    
    private HashSet<Graph.Node> GenerateVertexSet(int vertexNumber)
    {
        HashSet <Graph.Node> vertexSet= new();
        for (int i = 0; i < vertexNumber; i++)
        {
            var node = new Graph.Node();
            vertexSet.Add(node);
        }

        return vertexSet;
    }
    private bool GenerateWithProbability(int probability)
    {
        Random random = new Random();
        double randomValue = random.Next(1,101); // generates a random number between 0 and 101
        return randomValue < probability;
    }
}