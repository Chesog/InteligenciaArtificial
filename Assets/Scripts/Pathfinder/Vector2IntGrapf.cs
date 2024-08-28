using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Vector2IntGrapf<NodeType,pathType>
    where NodeType : INode<Vector2Int>, INode, new()
    where pathType : Enum
{
    /*
     * TODO : Hacer un grafo generico
     */
    public List<NodeType> nodes = new List<NodeType>();

    public Vector2IntGrapf(int x, int y,pathType pathType)
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
                node.SetNodeCost(Random.Range(0, 10));
                node.SetBlock(false);
                nodes.Add(node);
            }
        }

        foreach (NodeType node in nodes)
        {
            AddNodeNeighbors(node,pathType);
        }
    }
    
    private void AddNodeNeighbors(NodeType currentNode , pathType path)
    {
        foreach (NodeType neighbor in nodes)
        {
            if (neighbor.GetCoordinate().x == currentNode.GetCoordinate().x &&
                Math.Abs(neighbor.GetCoordinate().y - currentNode.GetCoordinate().y) == 1)
                currentNode.AddNeighbor(neighbor);

            else if (neighbor.GetCoordinate().y == currentNode.GetCoordinate().y &&
                Math.Abs(neighbor.GetCoordinate().x - currentNode.GetCoordinate().x) == 1)
                currentNode.AddNeighbor(neighbor);
            
            //if (Math.Abs(neighbor.GetCoordinate().y - currentNode.GetCoordinate().y) == 1 && Math.Abs(neighbor.GetCoordinate().x - currentNode.GetCoordinate().x) == 1)
            //    currentNode.AddNeighbor(neighbor);
            //Debug.Log(Traveler.pathfinder_flag);
            if (path.Equals(PathfinderFlags.Dijstra_Pf) || path.Equals(PathfinderFlags.AStar_Pf))
            {
                if (Math.Abs(neighbor.GetCoordinate().y - currentNode.GetCoordinate().y) == 1 && Math.Abs(neighbor.GetCoordinate().x - currentNode.GetCoordinate().x) == 1)
                    currentNode.AddNeighbor(neighbor);
            }
        }
    }
}