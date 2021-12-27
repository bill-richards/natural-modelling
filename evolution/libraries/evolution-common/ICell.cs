using System.ComponentModel;

namespace evolution;

public interface ICell : INotifyPropertyChanged
{
    int Size { get; }
    string Id { get; }

    void Grow(int steps = 1);
    void Shrink(int steps = 1);
}