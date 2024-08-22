public interface INode
{
    public bool IsBloqued();
}

public interface INode<Coorninate> 
{
    public void SetCoordinate(Coorninate coordinateType);
    public Coorninate GetCoordinate();
}
