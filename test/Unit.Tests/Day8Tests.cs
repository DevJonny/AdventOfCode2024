using App.Days;
using Xunit.Abstractions;

namespace Unit.Tests;

public class Day8Tests
{
    private readonly ITestOutputHelper _testOutputHelper;

    public Day8Tests(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
    }
    
    [Fact]
    public void Day8Part1()
    {
        Assert.Equal(
            2437272016585, 
            new Day8("../../../../../Files/Day8.txt", _testOutputHelper).Run());
    }
    
    [Fact]
    public void Day8Part2()
    {
        Assert.Equal(
            162987117690649, 
            new Day8("../../../../../Files/Day8.txt", _testOutputHelper).Run());
    }
    
    /// <summary>
    /// Antennas at:
    /// 0: (8,1), (5,2), (7,3), (4,4)
    /// A: (6,5), (8,8), (9,9)
    ///
    /// 0  ......#....#
    /// 1  ...#....0...
    /// 2  ....#0....#.
    /// 3  ..#....0....
    /// 4  ....0....#..
    /// 5  .#....A.....
    /// 6  ...#........
    /// 7  #......#....
    /// 8  ........A...
    /// 9  .........A..
    /// 10 ..........#.
    /// 11 ..........#.
    /// </summary>
    
    [Fact]
    public void TestDataPart1()
    {
        Assert.Equal(
            14, 
            new Day8("Files/Day8.txt", _testOutputHelper).Run());
    }

}