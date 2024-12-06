using App.Days;
using Xunit.Abstractions;

namespace Unit.Tests;

public class Day6Tests(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void Day6Part1()
    {
        Assert.Equal(4647, new Day6("../../../../../Files/Day6.txt", testOutputHelper).Part1());
    }
    
    [Fact]
    public void Day6Part2()
    {
        Assert.Equal(1858, new Day6("../../../../../Files/Day6.txt", testOutputHelper).Part2());
    }

    [Fact]
    public void Day6AlgorithmPart1()
    {
        Assert.Equal(41, new Day6("../../../../../Files/Day6.txt", testOutputHelper).SetGrid("Files/Day6.txt").Part1());
    }

    [Fact]
    public void Day6AlgorithmPart2()
    {
        Assert.Equal(
            6,
            new Day6("../../../../../Files/Day6.txt", testOutputHelper)
                .SetGrid("Files/Day6.txt")
                .Part2(true));
    }

    [Theory]
    [InlineData(3,6)]
    [InlineData(6,7)]
    [InlineData(7,7)]
    [InlineData(1,8)]
    [InlineData(3,8)]
    [InlineData(7,9)]
    public void TestLoopBreaking(int x, int y)
    {
        var day6 = new Day6("../../../../../Files/Day6.txt", testOutputHelper).SetGrid("Files/Day6.txt");
        
        Assert.NotEqual(1, day6.SetObstacle(new(x, y)).Part2());
    }
}