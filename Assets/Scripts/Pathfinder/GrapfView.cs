using System;
using System.IO;
using UnityEngine;

public class GrapfView : MonoBehaviour
{
    public Vector2IntGrapf<Node<Vector2Int>,PathfinderFlags> grapf;
    public PathfinderFlags pathfinder_flag;

    private void Start()
    {
        grapf = new Vector2IntGrapf<Node<Vector2Int>, PathfinderFlags>(10, 10, pathfinder_flag);
    }
    
    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;
        
        foreach (Node<Vector2Int> node in grapf.nodes)
        {
            if (node == null)
                return;
            
            Vector3 nodeCordinates = new Vector3(node.GetCoordinate().x, node.GetCoordinate().y);
            foreach (INode<Vector2Int> neighbor in node.GetNeighbors())
            {
                Gizmos.color = Color.white;
                Vector2Int neighborCordinates = neighbor.GetCoordinate();
                Gizmos.DrawLine(nodeCordinates,new Vector3(neighborCordinates.x,neighborCordinates.y));
            } 
        }
    }
}
