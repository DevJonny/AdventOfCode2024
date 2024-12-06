namespace App.Models;

public record Coord(int X, int Y)
{
    public override string ToString() => $"{X}, {Y}";
}