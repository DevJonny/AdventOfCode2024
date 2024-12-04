using App.Days;
using Xunit.Abstractions;

namespace Unit.Tests;

public class Day4Tests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void Day4Part1()
    {
        Assert.Equal(2454, new Day4("../../../../../Files/Day4.txt").Part1());
    }
    
    [Fact]
    public void Day4Part2()
    {
        Assert.Equal(1858, new Day4("../../../../../Files/Day4.txt").Part2());
    }

    [Fact]
    public void Day4AlgorithmPart1()
    {
        Assert.Equal(18, new Day4("Files/Day4.txt").Part1());
    }

    [Fact]
    public void Day4AlgorithmPart2()
    {
        Assert.Equal(9, new Day4("Files/Day4.txt").Part2());
    }
}