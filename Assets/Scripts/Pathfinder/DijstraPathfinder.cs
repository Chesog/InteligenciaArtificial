using System.Collections.Generic;

public class DijstraPathfinder<NodeType> : Pathfinder<NodeType> where NodeType : INode
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
        throw new System.NotImplementedException();
    }

    protected override int MoveToNeighborCost(NodeType A, NodeType b)
    {
        throw new System.NotImplementedException();
    }

    protected override bool NodesEquals(NodeType A, NodeType B)
    {
        throw new System.NotImplementedException();
    }
}
