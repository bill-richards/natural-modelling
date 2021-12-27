namespace evolution;

public interface ICellFactory
{
    ICell Create(int size, int identifier);
    ICell Create(int size, string identifier);
}