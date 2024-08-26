using System.Collections.Generic;
using UnityEngine;

public class DepthFirstPathfinder<NodeType> : Pathfinder<NodeType> where NodeType : INode
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
        return node.neighbors;
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
