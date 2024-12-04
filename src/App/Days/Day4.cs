namespace App.Days;

public class Day4
{
    private static char[] _xmas = ['X', 'M', 'A', 'S'];
    private readonly char[][] _data;

    public Day4(string? dataFile)
    {
        _data = dataFile is null ? [] : File.ReadAllLines(dataFile).Select(r => r.ToCharArray()).ToArray();
    }

    public int Part1()
    {
        // Loop through each row and stop when you find an 'X'
        // For each 'X' look around for a 'M' then recursively 
        // search along the path for the next letter in the 
        // sequence returning `false` if it's not found

        var numberOfXmases = 0;
        
        for (var row = 0; row < _data.Length; row++)
        for (var column = 0; column < _data[row].Length; column++)
        {
            if (_data[row][column] != 'X') continue; 
            
            // Found 'X'
            for (var x = -1; x <= 1; x++)
            for (var y = -1; y <= 1; y++)
            {
                (int x, int y) coord = (row + x, column + y);
                    
                if (IsNotInBounds(coord) || _data[coord.x][coord.y] != 'M') 
                    continue;
                    
                // Found 'M'
                coord = (coord.x + x, coord.y + y);

                if (IsNotInBounds(coord) || _data[coord.x][coord.y] != 'A') 
                    continue;
                    
                // Found 'A'
                coord = (coord.x + x, coord.y + y);
                    
                if (IsNotInBounds(coord) || _data[coord.x][coord.y] != 'S') 
                    continue;
                
                // Found 'S'
                numberOfXmases++;
            }
        }
        
        return numberOfXmases;
    }

    public int Part2()
    {
        var numberOfXmases = 0;
        
        for (var row = 0; row < _data.Length; row++)
        for (var column = 0; column < _data[row].Length; column++)
        {
            if (_data[row][column] != 'A') 
                continue; 
            
            // Found 'A'
            Coord[] coords =
            [
                new(row - 1, column - 1), 
                new(row + 1, column + 1),
                new(row - 1, column + 1),
                new(row + 1, column - 1)
            ];
            
            if (coords.Any(c => IsNotInBounds((c.X, c.Y))))
                continue;
            
            var corners = coords.Select(c => _data[c.X][c.Y]).ToArray();

            numberOfXmases += corners switch
            {
                ['M', 'S', 'M', 'S'] => 1,
                ['M', 'S', 'S', 'M'] => 1,
                ['S', 'M', 'S', 'M'] => 1,
                ['S', 'M', 'M', 'S'] => 1,
                _ => 0
            };
        }
        
        return numberOfXmases;
    }

    private bool IsNotInBounds((int x, int y) coord)
        => coord.x < 0 || coord.y < 0 || coord.x >= _data.Length || coord.y >= _data[0].Length;
}

public record Coord(int X, int Y);