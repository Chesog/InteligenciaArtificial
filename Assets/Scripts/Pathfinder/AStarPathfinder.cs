using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinder<NodeType> : Pathfinder<NodeType>
    where NodeType : INode
{
    protected override int Distance(NodeType A, NodeType B)
    {
        int xDistance =
            Mathf.Abs((A as INode<Vector2Int>).GetCoordinate().x - (A as INode<Vector2Int>).GetCoordinate().x);
        int yDistance =
            Mathf.Abs((A as INode<Vector2Int>).GetCoordinate().y - (B as INode<Vector2Int>).GetCoordinate().y);
        return Mathf.Abs(xDistance - yDistance);
    }

    protected override ICollection<INode> GetNeighbors(NodeType node)
    {
        if (node == null)
        {
            Debug.LogError("this node is null");
            return null;
        }
        return node.neighbors;
    }

    protected override bool IsBloqued(NodeType node)
    {
        return node.IsBloqued();
    }

    protected override int MoveToNeighborCost(NodeType A, NodeType B)
    {
        return (A.GetNodeCost() - B.GetNodeCost());
    }

    protected override bool NodesEquals(NodeType A, NodeType B)
    {
        return A.Equals(B);
    }
}