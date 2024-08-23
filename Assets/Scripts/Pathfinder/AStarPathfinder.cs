using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinder<NodeType> : Pathfinder<NodeType> where NodeType : INode
{
    protected override int Distance(NodeType A, NodeType B)
    {
        int distace = 0;
        distace += (A as INode<Vector2Int>).GetCoordinate().x - (A as INode<Vector2Int>).GetCoordinate().x;
        distace += (A as INode<Vector2Int>).GetCoordinate().y - (B as INode<Vector2Int>).GetCoordinate().y;
        return distace;
    }

    protected override ICollection<NodeType> GetNeighbors(NodeType node)
    {
       return (ICollection<NodeType>)(node.GetType() as INode<NodeType>).neighbors;
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
        return A.EqualsTo(B);
    }
}
