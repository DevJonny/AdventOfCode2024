using App.Days;
using Xunit.Abstractions;

namespace Unit.Tests;

public class Day7Tests
{
    private readonly ITestOutputHelper _testOutputHelper;
    
    private readonly string[] _partOneOperators = ["+", "*"];
    private readonly string[] _partTwoOperators = ["+", "*", "|"];

    public Day7Tests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }
    
    [Fact]
    public void Day7Part1()
    {
        Assert.Equal(
            2437272016585, 
            new Day7("../../../../../Files/Day7.txt", _testOutputHelper).Run(_partOneOperators));
    }
    
    [Fact]
    public void Day7Part2()
    {
        Assert.Equal(
            162987117690649, 
            new Day7("../../../../../Files/Day7.txt", _testOutputHelper).Run(_partTwoOperators));
    }
    
    [Fact]
    public void TestDataPart1()
    {
        Assert.Equal(
            3749, 
            new Day7("Files/Day7.txt", _testOutputHelper).Run(_partOneOperators));
    }
    
    [Fact]
    public void TestDataPart2()
    {
        Assert.Equal(
            11387, 
            new Day7("Files/Day7.txt", _testOutputHelper).Run(_partTwoOperators));
    }
    
    [Theory]
    [InlineData("10 19", 190)]
    [InlineData("81 40 27", 3267)]
    [InlineData("17 5", 0)]
    [InlineData("15 6", 0)]
    [InlineData("6 8 6 15", 0)]
    [InlineData("16 10 13", 0)]
    [InlineData("17 8 14", 0)]
    [InlineData("9 7 18 13", 0)]
    [InlineData("11 6 16 20", 292)]
    public void TestIndividualsPartOne(string equation, long expectedResult)
    {
        var rightHandSide = equation.Split(" ").Select(long.Parse).ToList();

        Assert.Equal(
            expectedResult,
            new Day7("Files/Day7.txt", _testOutputHelper)
                .SetEquations((expectedResult, rightHandSide))
                .Run(_partOneOperators));
    }
    
    [Theory]
    [InlineData("10 19", 190)]
    [InlineData("81 40 27", 3267)]
    [InlineData("17 5", 0)]
    [InlineData("15 6", 156)]
    [InlineData("6 8 6 15", 7290)]
    [InlineData("16 10 13", 0)]
    [InlineData("17 8 14", 192)]
    [InlineData("9 7 18 13", 0)]
    [InlineData("11 6 16 20", 292)]
    public void TestIndividualsPartTwo(string equation, long expectedResult)
    {
        var rightHandSide = equation.Split(" ").Select(long.Parse).ToList();
        
        Assert.Equal(
            expectedResult, 
            new Day7("Files/Day7.txt", _testOutputHelper)
                .SetEquations((expectedResult, rightHandSide))
                .Run(_partTwoOperators));
    }

}