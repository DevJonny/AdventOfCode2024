using App.Models;
using Xunit.Abstractions;

namespace App.Days;

public class Day6
{
    private char[][] _grid;
    private HashSet<Coord> _possibleObstacles = [];

    private readonly ITestOutputHelper _testOutputHelper;
    
    public Day6(string dataFile, ITestOutputHelper testOutputHelper)
    {
        _grid = File.ReadAllLines(dataFile).Select(l => l.ToCharArray()).ToArray();
        _testOutputHelper = testOutputHelper;
    }

    public Day6 SetGrid(string dataFile)
    {
        _grid = File.ReadAllLines(dataFile).Select(l => l.ToCharArray()).ToArray();

        return this;
    }

    public Day6 SetObstacle(Coord obstacle)
    {
        _grid[obstacle.Y][obstacle.X] = '#';
        return this;
    }

    public Day6 ClearObstacle(Coord obstacle)
    {
        _grid[obstacle.Y][obstacle.X] = '.';
        return this;
    }

    public int Part1()
    {
        var guard = FindStartPoint();
        HashSet<Coord> moves = [ guard ];
        
        MoveGuard(guard, Direction.Up, moves);
        
        return moves.Count;
    }

    public int Part2(bool bruteForce = false)
    {
        var guard = FindStartPoint();
        HashSet<Coord> moves = [ guard ];
        
        var numberOfLoops = 0;
        
        for (var x = 0; x < _grid.Length; x++)
        for (var y = 0; y < _grid[x].Length; y++)
            if (_grid[y][x] == '.')
            {
                Coord obstacle = new(x, y);
                
                _testOutputHelper.WriteLine($"Set obstacle {obstacle.X} {obstacle.Y}");
                
                if (bruteForce)
                    SetObstacle(obstacle);
                
                if (MoveGuard(guard, Direction.Up, moves))
                    numberOfLoops++;
                
                ClearObstacle(obstacle);
            }
        
        return numberOfLoops;
    }

    private Coord FindStartPoint()
    {
        for (var x = 0; x < _grid.Length; x++)
        for (var y = 0; y < _grid[x].Length; y++)
            if (_grid[y][x] == '^')
                return new(x,y);

        return new(0, 0);
    }

    private bool MoveGuard(Coord guard, Direction currentDirection, HashSet<Coord> moves)
    {
        var highestMoveCount = 0;
        var numberOfLoops = 0;
        
        do
        {
            var nextMove = NextMove(currentDirection, guard);

            if (IsObstacle(nextMove))
                (currentDirection, nextMove) = Turn(currentDirection, guard);

            _testOutputHelper.WriteLine($"{moves.Count} {currentDirection} {nextMove}");

            if (IsNotInBounds(nextMove))
                return false;
            
            moves.Add(nextMove);

            guard = nextMove;
            
            if (highestMoveCount == moves.Count)
                numberOfLoops++;
            else
                highestMoveCount = moves.Count;
            
        } while (numberOfLoops < highestMoveCount);
        
        return numberOfLoops == highestMoveCount;
    }

    private static Coord NextMove(Direction direction, Coord guard) => direction switch
    {
        Direction.Left => guard with { X = guard.X - 1 },
        Direction.Right => guard with { X = guard.X + 1 },
        Direction.Up => guard with { Y = guard.Y - 1 },
        Direction.Down => guard with { Y = guard.Y + 1 },
        _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
    };

    private bool IsObstacle(Coord coord)
        => !IsNotInBounds(coord) && _grid[coord.Y][coord.X] == '#';
    
    private bool IsNotInBounds(Coord coord)
        => coord.X < 0 || coord.Y < 0 || coord.Y >= _grid.Length || coord.X >= _grid[0].Length;

    private static (Direction nextDirection, Coord coord) Turn(Direction direction, Coord guard)
    {
        var nextDirection = direction switch
        {
            Direction.Up => Direction.Right,
            Direction.Right => Direction.Down,
            Direction.Down => Direction.Left,
            Direction.Left => Direction.Up,
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
        
        return (nextDirection, NextMove(nextDirection, guard));
    }
}

public enum Direction { Up, Down, Left, Right }