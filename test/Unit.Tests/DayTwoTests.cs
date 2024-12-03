using App.Days;

namespace Unit.Tests;

public class DayTwoTests
{
    [Fact]
    public void PartOne()
    {
        Assert.Equal(472, DayTwo.PartOne("../../../../../Files/DayTwo.txt"));
    }

    [Fact]
    public void PartTwo()
    {
        Assert.Equal(520, DayTwo.PartTwo("../../../../../Files/DayTwo.txt"));
    }

    #region Part One
    
    private static int PartOneCheckReportsAreSafe(string report)
    {
        var levels = report.Split(' ').Select(int.Parse).ToArray();

        bool ascendingLevel;
            
        if (levels[0] < levels[1])
            ascendingLevel = true;
        else if (levels[0] > levels[1])
            ascendingLevel = false;
        else
            return 0;
            
        for (var i = 1; i < levels.Length; i++)
        {
            var previousLevel = levels[i - 1];
            var currentLevel = levels[i];
                
            // Should be ascending but is descending
            if (ascendingLevel && currentLevel < previousLevel)
                return 0;
                
            // Should be descending but is ascending
            if (!ascendingLevel && currentLevel > previousLevel)
                return 0;
                
            if (currentLevel == previousLevel)
                return 0;
        
            if (ascendingLevel && currentLevel - 3 > previousLevel)
                return 0;
            
            if (!ascendingLevel && previousLevel - 3 > currentLevel)
                return 0;
        }
        
        return 1;
    }

    [Theory]
    [InlineData("1 2 3 5 8")]
    [InlineData("8 5 3 2 1")]
    [InlineData("1 2 3 4 5")]
    [InlineData("5 4 3 2 1")]
    [InlineData("1 4 7 10 12")]
    [InlineData("12 10 7 4 1")]
    public void PartOneCheckReport_ReportIsSafe_ReturnOne(string report)
    {
        Assert.Equal(1, PartOneCheckReportsAreSafe(report));
    }

    [Theory]
    [InlineData("1 1 1 1 1")]
    [InlineData("1 5 6 7 8")]
    [InlineData("8 7 6 5 1")]
    [InlineData("1 -1 3 -5 -7")]
    public void PartOneCheckReport_ReportIsUnsafe_ReturnZero(string report)
    {
        Assert.Equal(0, PartOneCheckReportsAreSafe(report));
    }
    
    #endregion
    
    #region Part Two
    
    private int PartTwoCheckReportsAreSafe(string report)
    {
        var levels = report.Split(' ').Select(int.Parse).ToList();

        for (var i = 0; i < levels.Count; i++)
        {
            var newLevels = new List<int>(levels);
            newLevels.RemoveAt(i);
            
            var result = PartOneCheckReportsAreSafe(string.Join(" ", newLevels));

            if (result == 1)
                return 1;
        }

        return 0;
    }

    [Theory]
    [InlineData("7 6 4 2 1")]
    [InlineData("1 3 2 4 5")]
    [InlineData("8 6 4 4 1")]
    [InlineData("1 3 6 7 9")]
    [InlineData("43 40 41 44 45 46 48 51")]
    [InlineData("29 28 27 25 26 25 22 20")]
    [InlineData("48 46 47 49 51 54 56")]
    public void PartTwoCheckReport_ReportIsSafe_ReturnOne(string report)
    {
        Assert.Equal(1, PartTwoCheckReportsAreSafe(report));
    }

    [Theory]
    [InlineData("1 1 1 1 1")]
    [InlineData("1 2 6 7 18")]
    [InlineData("18 7 6 2 1")]
    [InlineData("1 -1 1 -1 1")]
    [InlineData("1 2 7 8 9")]
    [InlineData("9 7 6 2 1")]
    [InlineData("8 7 14 15 16 17")]
    public void PartTwoCheckReport_ReportIsUnsafe_ReturnZero(string report)
    {
        Assert.Equal(0, PartTwoCheckReportsAreSafe(report));
    }
    
    #endregion
}