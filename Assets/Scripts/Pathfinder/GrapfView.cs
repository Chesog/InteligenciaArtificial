using UnityEngine;

public class GrapfView : MonoBehaviour
{
    public Vector2IntGrapf<Node<Vector2Int>> grapf;

    void Start()
    {
        grapf = new Vector2IntGrapf<Node<Vector2Int>>(10, 10);
    }

    private void OnDrawGizmos()
    {
        if (!Application.isPlaying)
            return;
        
        foreach (Node<Vector2Int> node in grapf.nodes)
        {
            if (node == null)
                return;
            
            if (node.IsBloqued())
                Gizmos.color = Color.red;
            else
                Gizmos.color = Color.green;

            Vector3 nodeCordinates = new Vector3(node.GetCoordinate().x, node.GetCoordinate().y);
            Gizmos.DrawWireSphere(nodeCordinates, 0.1f);
            foreach (INode<Vector2Int> neighbor in node.GetNeighbors())
            {
                Vector2Int neighborCordinates = neighbor.GetCoordinate();
                Gizmos.DrawLine(nodeCordinates,new Vector3(neighborCordinates.x,neighborCordinates.y));
            } 
        }
    }
}
