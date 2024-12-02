using System.Runtime.InteropServices;
using Xunit.Abstractions;

namespace AdventOfCode2024;

public class DayTwo(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void PartOne()
    {
        var reports = File.ReadAllLines("./Files/DayTwoPartOne.txt");

        var safeReports = reports.Sum(PartOneCheckReportsAreSafe);

        testOutputHelper.WriteLine(safeReports.ToString());
        
        Assert.Equal(472, safeReports);
    }

    [Fact]
    public void PartTwo()
    {
        var reports = File.ReadAllLines("./Files/DayTwoPartTwo.txt");
        
        var safeReports = reports.Sum(PartTwoCheckReportsAreSafe);
        
        testOutputHelper.WriteLine(safeReports.ToString());
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
        var levels = report.Split(' ').Select(int.Parse).ToArray();

        var numberOfSafeLevels = 1;
        
        bool ascendingLevel = false;

        var ascendingLevels = 0;
        var descendingLevels = 0;
        
        for (var i = 1; i < levels.Length; i++)
        {
            var previousLevel = levels[i-1];
            var currentLevel = levels[i];

            if (previousLevel > currentLevel)
                descendingLevels++;

            if (previousLevel < currentLevel)
                ascendingLevels++;
        }
        
        ascendingLevel = ascendingLevels >= levels.Length - 2;

        List<int> badLevelIndexes = [];

        var checkAgainst = levels[0];
        
        for (var i = 1; i < levels.Length; i++)
        {
            var currentLevel = levels[i];

            if (ascendingLevel && currentLevel < checkAgainst)
            {
                checkAgainst = currentLevel;
                badLevelIndexes.Add(i - 1);
                continue;
            }
            
            if ((checkAgainst + 1 <= currentLevel && checkAgainst + 3 >= currentLevel && ascendingLevel) ||
                (checkAgainst - 1 >= currentLevel && checkAgainst - 3 <= currentLevel && !ascendingLevel))
            {
                checkAgainst = currentLevel;
                continue;
            }
            
            badLevelIndexes.Add(i);
        }

        var isBadReport = badLevelIndexes.Count >= 2;
        
        if (!isBadReport)
            testOutputHelper.WriteLine($"{report} is {(isBadReport ? "Unsafe" : "Safe")}");
        
        return isBadReport ? 0 : 1;
    }

    [Theory]
    [InlineData("7 6 4 2 1")]
    [InlineData("1 3 2 4 5")]
    [InlineData("8 6 4 4 1")]
    [InlineData("1 3 6 7 9")]
    [InlineData("43 40 41 44 45 46 48 51")]
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
    [InlineData("7 8 9 1 11 12")]
    public void PartTwoCheckReport_ReportIsUnsafe_ReturnZero(string report)
    {
        Assert.Equal(0, PartTwoCheckReportsAreSafe(report));
    }
    
    #endregion
}

public static class RangeExtensions
{
    public static bool Contains(this Range range, int value) => range.Start.Value <= value && value < range.End.Value;
}