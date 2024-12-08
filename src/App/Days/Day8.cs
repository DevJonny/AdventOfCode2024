using Xunit.Abstractions;

namespace App.Days;

public class Day8
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly HashSet<Antenna> _antennas = new();
    
    private char[][] _grid;
    
    public Day8(string dataFile, ITestOutputHelper testOutputHelper)
    {
        _grid = File.ReadAllLines(dataFile).Select(l => l.ToCharArray()).ToArray();
        _testOutputHelper = testOutputHelper;
    }

    public int Run()
    {
        FindAllAntennas();

        foreach (var antenna in _antennas)
        {
            _testOutputHelper.WriteLine($"{antenna.Letter} ({antenna.Coord.X},{antenna.Coord.Y})");
        }
        
        return 0;
    }

    private void FindAllAntennas()
    {
        for (var x = 0; x < _grid.Length; x++)
        for (var y = 0; y < _grid[x].Length; y++)
        {
            var letter = _grid[y][x];
            if (letter != '.' && !_antennas.Contains(new(letter, new(x, y))))
                _antennas.Add(new(letter, new(x, y)));
        }
    }

    private bool IsNotInBounds(Coord coord)
        => coord.X < 0 || coord.Y < 0 || coord.Y >= _grid.Length || coord.X >= _grid[0].Length;
}

public record Antenna(char Letter, Coord Coord);