using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Traveler : MonoBehaviour
{
    public GrapfView grapfView;
    
    private AStarPathfinder<Node<Vector2Int>> Pathfinder;
    //private DijstraPathfinder<Node<Vector2Int>> Pathfinder;
    //private DepthFirstPathfinder<Node<Vector2Int>> Pathfinder;
    //private BreadthPathfinder<Node<Vector2Int>> Pathfinder;

    private Node<Vector2Int> startNode; 
    private Node<Vector2Int> destinationNode;

    void Start()
    {
        startNode = new Node<Vector2Int>();
        startNode.SetCoordinate(new Vector2Int(Random.Range(0, 10), Random.Range(0, 10)));

        destinationNode = new Node<Vector2Int>();
        destinationNode.SetCoordinate(new Vector2Int(Random.Range(0, 10), Random.Range(0, 10)));

        List<Node<Vector2Int>> path = Pathfinder.FindPath(startNode, destinationNode, grapfView.grapf.nodes);
        StartCoroutine(Move(path));
    }

    public IEnumerator Move(List<Node<Vector2Int>> path) 
    {
        foreach (Node<Vector2Int> node in path)
        {
            transform.position = new Vector3(node.GetCoordinate().x, node.GetCoordinate().y);
            yield return new WaitForSeconds(1.0f);
        }
    }
}
