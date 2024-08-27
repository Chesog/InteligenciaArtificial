using System;
using System.Collections.Generic;

public class Node<Coordinate> : INode, INode<Coordinate>
    , IEquatable<Node<Coordinate>> where Coordinate : IEquatable<Coordinate>
{
    public bool bloqued { get; private set; }
    private ICollection<INode<Coordinate>> neighbors = new List<INode<Coordinate>>();
    private Func<Coordinate, float> DistanceTo;
    private Coordinate coordinate;
    private int nodeCost;


    public ICollection<INode<Coordinate>> GetNeighbors()
    {
        return neighbors;
    }

    public void AddNeighbor(INode<Coordinate> neighbor)
    {
        neighbors.Add(neighbor);
    }
    
    public void MoveTo(Coordinate coorninate)
    {
        coordinate = coorninate;
    }

    public void SetDistanceMethod(Func<Coordinate, float> DistanceTo)
    {
        this.DistanceTo = DistanceTo;
    }

    public float CalculateDistanceTo(Coordinate coorninate)
    {
        return DistanceTo(coorninate);
    }

    public void SetCoordinate(Coordinate coordinate)
    {
        this.coordinate = coordinate;
    }

    public Coordinate GetCoordinate()
    {
        return coordinate;
    }

    public bool IsBloqued()
    {
        return bloqued;
    }

    public void SetBlock(bool blockState)
    {
        bloqued = blockState;
    }

    public void SetNodeCost(int cost)
    {
        nodeCost = cost;
    }

    public int GetNodeCost()
    {
        return nodeCost;
    }

    public bool Equals(Node<Coordinate> other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return EqualityComparer<Coordinate>.Default.Equals(coordinate, other.coordinate) &&
               nodeCost == other.nodeCost && bloqued == other.bloqued && Equals(neighbors, other.neighbors);
    }

    public override bool Equals(object obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;
        return Equals((Node<Coordinate>)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(coordinate, nodeCost, bloqued, neighbors);
    }
}