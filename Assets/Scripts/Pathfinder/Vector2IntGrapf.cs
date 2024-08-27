using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Vector2IntGrapf<NodeType> 
    where NodeType : INode<Vector2Int>, INode, new()
{ 
    
    /*
     * TODO : Hacer un grafo generico
     */
    public List<NodeType> nodes = new List<NodeType>();
    
    public Vector2IntGrapf(int x, int y) 
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                NodeType node = new NodeType();
                node.SetCoordinate(new Vector2Int(i, j));
                node.SetDistanceMethod((Vector2Int other) =>
                {
                    return Vector2Int.Distance(node.GetCoordinate(), other);
                });
                node.SetNodeCost(Random.Range(0,10));
                node.SetBlock(false);
                nodes.Add(node);
            }
        }

        foreach (NodeType node in nodes)
        {
            AddNodeNeighbors(node,x,y);
        }
    }

    private void AddNodeNeighbors(NodeType currentNode,int gridWidth,int gridHeight)
    {
        // Up Node
        if (currentNode.GetCoordinate().y + 1 < gridHeight)
            currentNode.AddNeighbor(GetNode(currentNode.GetCoordinate().x,currentNode.GetCoordinate().y + 1));
        // Down Node
        if (currentNode.GetCoordinate().y - 1 >= 0)
            currentNode.AddNeighbor(GetNode(currentNode.GetCoordinate().x,currentNode.GetCoordinate().y - 1));
        // Left Node
        if (currentNode.GetCoordinate().x - 1 >= 0)
            currentNode.AddNeighbor(GetNode(currentNode.GetCoordinate().x - 1,currentNode.GetCoordinate().y));
        // Right Node
        if (currentNode.GetCoordinate().x + 1 < gridWidth)
            currentNode.AddNeighbor(GetNode(currentNode.GetCoordinate().x + 1,currentNode.GetCoordinate().y));
    }

    private NodeType GetNode(int nodeX , int nodeY)
    {
        NodeType desiredNode = new NodeType();
        foreach (NodeType node in nodes)
        {
            if (node.GetCoordinate().x == nodeX && node.GetCoordinate().y == nodeY)
                desiredNode = node;
        }
        return desiredNode;
    }
}
