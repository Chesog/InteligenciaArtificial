using System.Collections.Generic;

public interface INode
{
    public bool EqualsTo(INode other);
    public bool IsBloqued();
    public void SetBlock(bool blockState);
    public void SetNodeCost(int cost);
    public int GetNodeCost();
}

public interface INode<Coorninate> 
{
    public Coorninate GetCoordinate();
    public void SetCoordinate(Coorninate coordinateType);
    public void MoveTo(Coorninate coorninate);
    public ICollection<Node<Coorninate>> neighbors { get; set; }
}
