using System.Text.RegularExpressions;
using Xunit.Abstractions;

namespace AdventOfCode2024;

public partial class DayThree(ITestOutputHelper testOutputHelper)
{
    private readonly Regex _ops = OperationRegex();
    
    [Fact]
    public void DayThreePartOne()
    {
        var data = File.ReadAllText("Files/DayThreePartOne.txt");

        var total = _ops
            .Matches(data)
            .Select(m => int.Parse(m.Groups[1].ToString()) * int.Parse(m.Groups[2].ToString()))
            .Sum();
        
        Assert.Equal(156388521, total);
    }

    [Fact]
    public void DayThreePartTwo()
    {
        var data = File.ReadAllText("Files/DayThreePartTwo.txt");
        var ends = data.Split("don't()");

        List<string> stringsToParse = [ends[0]]; 
        
        foreach (var end in ends)
        {
            var starts = end.IndexOf("do()", StringComparison.Ordinal);
            
            if (starts > -1)
                stringsToParse.Add(end[starts..]);
        }

        var total = stringsToParse
            .Sum(block => _ops
                .Matches(block)
                .Select(m => int.Parse(m.Groups[1].ToString()) * int.Parse(m.Groups[2].ToString()))
                .Sum());
        
        Assert.Equal(75920122, total);
    }

    [GeneratedRegex("""mul\((\d\d?\d?),(\d\d?\d?)\)""", RegexOptions.Compiled)]
    private static partial Regex OperationRegex();
}