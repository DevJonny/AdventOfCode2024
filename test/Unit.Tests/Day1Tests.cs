using App.Days;

namespace Unit.Tests;

public class Day1Tests
{
    [Fact]
    public void DayOnePartOne()
    {
        Assert.Equal(2367773, Day1.Part1("../../../../../Files/DayOne.txt"));
    }

    [Fact]
    public void DayOnePartTwo()
    {
        Assert.Equal(21271939, Day1.Part2("../../../../../Files/DayOne.txt"));
    }
    
    
}