using System;
using System.Collections.Generic;
using UnityEngine;

public class DijstraPathfinder<NodeType,CoordinateType> : Pathfinder<NodeType,CoordinateType>
    where NodeType : INode , INode<CoordinateType>
    where CoordinateType : IEquatable<CoordinateType>
{
    protected override int Distance(NodeType A, NodeType B)
    {
        return (int)A.CalculateDistanceTo(B.GetCoordinate());
    }

    protected override ICollection<NodeType> GetNeighbors(NodeType node)
    {
        ICollection<NodeType> neighbors = new List<NodeType>();
        if (node == null)
        {
            Debug.LogError("this node is null");
            return null;
        }

        foreach (NodeType Neighbor in node.GetNeighbors())
        {
            neighbors.Add(Neighbor);
        }

        return neighbors;
    }

    protected override bool IsBloqued(NodeType node)
    {
        return node.IsBloqued();
    }

    protected override int MoveToNeighborCost(NodeType A, NodeType B)
    {
        return 0;
    }

    protected override bool NodesEquals(NodeType A, NodeType B)
    {
        return A.Equals(B);
    }
}
