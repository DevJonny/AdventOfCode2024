using Xunit.Abstractions;

namespace AdventOfCode2024;

public class DayOne(ITestOutputHelper testOutputHelper)
{
    [Fact]
    public void DayOnePartOne()
    {
        var data = File.ReadAllLines("Files/DayOnePartOne.csv");
    
        var teamOne = data.Select(d => int.Parse(d.Split(",")[0])).OrderBy(d => d).ToArray();
        var teamTwo = data.Select(d => int.Parse(d.Split(",")[1])).OrderBy(d => d).ToArray();
        
        testOutputHelper.WriteLine($"Found {teamOne.Length} points on the left, and {teamTwo.Length} points on the right");

        List<int> diffs = [];

        for (var i = 0; i < teamOne.Length; i++)
        {
            if (teamOne[i] > teamTwo[i])
                diffs.Add(teamOne[i] - teamTwo[i]);
            else
                diffs.Add(teamTwo[i] - teamOne[i]);
        }
        
        Assert.Equal(2367773, diffs.Sum());
    }

    [Fact]
    public void DayOnePartTwo()
    {
        var data = File.ReadAllLines("Files/DayOnePartTwo.csv");
        
        var teamOne = data.Select(d => int.Parse(d.Split(",")[0])).ToArray();
        var teamTwo = data
            .Select(d => int.Parse(d.Split(",")[1]))
            .GroupBy(d => d)
            .ToDictionary(grp => grp.Key, grp => grp.Count());

        var counts = teamOne.Select(t => t * teamTwo.GetValueOrDefault(t, 0));

        Assert.Equal(21271939, counts.Sum());
    }
}