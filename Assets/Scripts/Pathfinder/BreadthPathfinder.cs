using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BreadthPathfinder<NodeType> : Pathfinder<NodeType> where NodeType : INode
{
    protected override int Distance(NodeType A, NodeType B)
    {
        return 0;
    }

    protected override ICollection<INode> GetNeighbors(NodeType node)
    {
        if (node == null)
        {
            Debug.LogError("this node is null");
            return null;
        }
        return node.neighbors.Reverse().ToList();
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
