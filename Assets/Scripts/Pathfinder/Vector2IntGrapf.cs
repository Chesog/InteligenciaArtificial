using System.Collections.Generic;

public class Vector2IntGrapf<NodeType> 
    where NodeType : INode<UnityEngine.Vector2Int>, INode, new()
{ 
    public List<NodeType> nodes = new List<NodeType>();

    public Vector2IntGrapf(int x, int y) 
    {
        for (int i = 0; i < x; i++)
        {
            for (int j = 0; j < y; j++)
            {
                NodeType node = new NodeType();
                node.SetCoordinate(new UnityEngine.Vector2Int(i, j));
                nodes.Add(node);
            }
        }
    }

}
