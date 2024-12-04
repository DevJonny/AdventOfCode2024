using App.Days;

namespace Unit.Tests;

public class Day3Tests
{
    
    [Fact]
    public void DayThreePartOne()
    {
        Assert.Equal(156388521, Day3.Part1("../../../../../Files/DayThree.txt"));
    }

    [Fact]
    public void DayThreePartTwo()
    {
        Assert.Equal(75920122, Day3.Part2("../../../../../Files/DayThree.txt"));
    }
}