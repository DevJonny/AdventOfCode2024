using App.Days;
using Xunit.Abstractions;

namespace Unit.Tests;

public class Day9Tests
{
    private readonly ITestOutputHelper _testOutputHelper;
    
    public Day9Tests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }
    
    [Fact]
    public void Day9Part1()
    {
        Assert.Equal(
            0, 
            new Day9("../../../../../Files/Day9.txt", _testOutputHelper).Run());
    }
    
    [Fact]
    public void Day9Part2()
    {
        Assert.Equal(
            0, 
            new Day9("../../../../../Files/Day9.txt", _testOutputHelper).Run());
    }
    
    [Fact]
    public void TestDataPart1()
    {
        Assert.Equal(
            1928, 
            new Day9("Files/Day9.txt", _testOutputHelper).Run());
    }
    
    [Fact]
    public void TestDataPart2()
    {
        Assert.Equal(
            0, 
            new Day9("Files/Day9.txt", _testOutputHelper).Run());
    }
}