# Building a Reachability Matrix with BFS and DFS 
# Purpose 
To gather and analyse the time complexity of usage of BFS and DFS algorithms for building a Matrix of Reachability of a randomly generated Graph 

**We compare:**

**BFS** on 
Adjacency Matrix and Adjacency Lists

**DFS** on 
Adjacency Matrix and Adjacency Lists

# How we gather data and compare it
Perform 20+ iterations with different Graphs but same parameters(density, verticesAmount) and find average time for each algorithm and its modification.

# Code info 
`Graph` class is a reperesentation of an undirecred unweighted graph. It features methods like `.GetAdjacencyMatrix()`, `.GetAdjacencyLists()` for effective retrieval the representation of the `Graph` instance. There are also methods to print both things.

`Graph.Node` class is nested inside `Graph` class, essentialy a part of the `Graph` class. Each `Node` instance has a name and method `.AddNeighbour()` which enables building Adjacency List for each vertex in Graph.  

`BreadthFirstSearch` class represents all methods which are needed to traverse the Graph and return Matrix of Reachability.

`DepthFirstSearch` class the same as `BreadthFirstSearch`. They feature the same functionality.

`ReachabilityMatrix` class handles all formatting and filling logic for Reachability Matrix. 


# About 
This github repo is a part of a project on discrete mathematics at **Kyiv School of Economics**

The main idea behind it is to essentialy build a Reachability Matrix from an adjacency matrix of a random graph 

Coded by Herman Havva, statistics gathered by Yevhen Liesnikov


