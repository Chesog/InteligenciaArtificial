using System;
using System.Collections.Generic;

public class DijstraPathfinder<NodeType,CoordinateType> : Pathfinder<NodeType> 
    where NodeType : INode , INode<CoordinateType>
    where CoordinateType : IEquatable<CoordinateType>
{
    protected override int Distance(NodeType A, NodeType B)
    {
        throw new System.NotImplementedException();
    }

    protected override ICollection<NodeType> GetNeighbors(NodeType node)
    {
        throw new System.NotImplementedException();
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
