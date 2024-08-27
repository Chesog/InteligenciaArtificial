using System;
using System.Collections.Generic;

public interface INode
{
    public bool IsBloqued();
    public void SetBlock(bool blockState);
    public void SetNodeCost(int cost);
    public int GetNodeCost();
}

public interface INode<Coorninate> : INode where  Coorninate : IEquatable<Coorninate>
{
    public Coorninate GetCoordinate();
    public void SetCoordinate(Coorninate coordinateType);
    public void MoveTo(Coorninate coorninate);
    public float CalculateDistanceTo(Coorninate coorninate);
    public void SetDistanceMethod(Func<Coorninate, float> DistanceTo);
    public ICollection<INode<Coorninate>> GetNeighbors();
    public void AddNeighbor(INode<Coorninate> neighbor);
}
