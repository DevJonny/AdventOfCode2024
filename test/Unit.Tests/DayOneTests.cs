using App.Days;

namespace Unit.Tests;

public class DayOneTests
{
    [Fact]
    public void DayOnePartOne()
    {
        Assert.Equal(2367773, DayOne.PartOne("../../../../../Files/DayOne.txt"));
    }

    [Fact]
    public void DayOnePartTwo()
    {
        Assert.Equal(21271939, DayOne.PartTwo("../../../../../Files/DayOne.txt"));
    }
}