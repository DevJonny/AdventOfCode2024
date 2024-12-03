using App.Days;

namespace Unit.Tests;

public class DayThreeTests
{
    
    [Fact]
    public void DayThreePartOne()
    {
        Assert.Equal(156388521, DayThree.PartOne("../../../../../Files/DayThree.txt"));
    }

    [Fact]
    public void DayThreePartTwo()
    {
        Assert.Equal(75920122, DayThree.PartTwo("../../../../../Files/DayThree.txt"));
    }
}