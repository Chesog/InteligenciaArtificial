public class Node<Coordinate> : INode, INode<Coordinate>
{
    private Coordinate coordinate;

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
        return false;
    }
}