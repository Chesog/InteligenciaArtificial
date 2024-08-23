using System.Collections.Generic;

public class Node<Coordinate> : INode, INode<Coordinate>
{
    public bool bloqued { get;private set; }
    public ICollection<Node<Coordinate>> neighbors { get; set; }
    private Coordinate coordinate;
    private int nodeCost;

    public void SetCoordinate(Coordinate coordinate) { this.coordinate = coordinate; }

    public void MoveTo(Coordinate coorninate) { coordinate = coorninate; }

    public Coordinate GetCoordinate() { return coordinate; }

    public bool EqualsTo(INode other) { return coordinate.Equals((other as Node<Coordinate>).GetCoordinate()); }

    public bool IsBloqued() { return bloqued; }

    public void SetBlock(bool blockState) { bloqued = blockState; }

    public void SetNodeCost(int cost) { nodeCost = cost; }
    public int GetNodeCost() { return nodeCost; }
}