using App.Days;

namespace Unit.Tests;

public class Day5Tests
{
    [Fact]
    public void Day4Part1()
    {
        Assert.Equal(4996, new Day5("../../../../../Files/Day5.txt").Part1());
    }
    
    [Fact]
    public void Day4Part2()
    {
        Assert.Equal(6311, new Day5("../../../../../Files/Day5.txt").Part2());
    }
    
    [Fact]
    public void TestDataPart1()
    {
        Assert.Equal(143, new Day5("Files/Day5.txt").Part1());
    }
    
    [Fact]
    public void TestDataPart2()
    {
        Assert.Equal(123, new Day5("Files/Day5.txt").Part2());
    }
    
    [Theory]
    [InlineData("75,47,61,53,29", 61)]
    [InlineData("97,61,53,29,13", 53)]
    [InlineData("75,29,13", 29)]
    [InlineData("75,97,47,61,53", 0)]
    [InlineData("61,13,29", 0)]
    [InlineData("97,13,75,29,47", 0)]
    public void TestIndividualsPartOne(string manual, int expectedResult)
    {
        List<int[]> manuals = [ manual.Split(",").Select(int.Parse).ToArray() ];
        
        Assert.Equal(expectedResult, new Day5("Files/Day5.txt").Part1(manuals));
    }
    
    [Theory]
    [InlineData("75,47,61,53,29", 0)]
    [InlineData("97,61,53,29,13", 0)]
    [InlineData("75,29,13", 0)]
    [InlineData("75,97,47,61,53", 47)]
    [InlineData("61,13,29", 29)]
    [InlineData("97,13,75,29,47", 47)]
    public void TestIndividualsPartTwo(string manual, int expectedResult)
    {
        List<int[]> manuals = [ manual.Split(",").Select(int.Parse).ToArray() ];
        
        Assert.Equal(expectedResult, new Day5("Files/Day5.txt").Part2(manuals));
    }
}