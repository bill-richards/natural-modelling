using gsdc.common;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace evolution.models;

public class Cell : ICell, ICellFactory
{
    public event PropertyChangedEventHandler? PropertyChanged;

    private int _size;

    public ICell Create(int size, int identifier) => new Cell(size, identifier);
    public ICell Create(int size, string identifier) => new Cell(size, identifier);

    public Cell() 
        => Id = 0.IntegerToHexString();

    private Cell(int size, int identifier)
    {
        Id = identifier.IntegerToHexString();
        Size = size;
    }
    private Cell(int size, string identifier)
    {
        Id = identifier;
        Size = size;
    }

    public int Size
    {
        get => _size;
        private set { _size = value; OnPropertyChanged(); }
    }

    public string Id { get; }

    public void Grow(int steps = 1) => Size += steps;

    public void Shrink(int steps = 1) => Size -= steps;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null) 
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
}