using System.Text.RegularExpressions;

namespace App.Days;

public abstract partial class Day3
{
    private static readonly Regex Ops = OperationRegex();
    
    public static int Part1(string dataFile)
    {
        var data = File.ReadAllText(dataFile);
        
        return Ops
            .Matches(data)
            .Select(m => int.Parse(m.Groups[1].ToString()) * int.Parse(m.Groups[2].ToString()))
            .Sum();
    }

    public static int Part2(string dataFile)
    {
        var data = File.ReadAllText(dataFile);
        var ends = data.Split("don't()");

        List<string> stringsToParse = [ends[0]]; 
        
        foreach (var end in ends)
        {
            var starts = end.IndexOf("do()", StringComparison.Ordinal);
            
            if (starts > -1)
                stringsToParse.Add(end[starts..]);
        }

        return stringsToParse
            .Sum(block => Ops
                .Matches(block)
                .Select(m => int.Parse(m.Groups[1].ToString()) * int.Parse(m.Groups[2].ToString()))
                .Sum());
    }
    
    [GeneratedRegex("""mul\((\d\d?\d?),(\d\d?\d?)\)""", RegexOptions.Compiled)]
    private static partial Regex OperationRegex();
}