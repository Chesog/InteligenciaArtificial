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
            if (node.IsBloqued())
                Gizmos.color = Color.red;
            else
                Gizmos.color = Color.green;
            
            Gizmos.DrawWireSphere(new Vector3(node.GetCoordinate().x, node.GetCoordinate().y), 0.1f);
        }
    }
}
