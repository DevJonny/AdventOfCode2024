using System.Text.RegularExpressions;

namespace App.Days;

public partial class Day5
{
    private readonly Dictionary<int, HashSet<int>> _rules = new();
    private List<int[]> _manuals;
    
    public Day5(string dataFile)
    {
        var allData = File.ReadAllLines(dataFile);
        var separatorIndex = Array.FindIndex(allData, string.IsNullOrWhiteSpace);
        
        var rulesData = string.Join(Environment.NewLine, allData[..(separatorIndex - 1)]);

        foreach (var match in RulesRegex().Matches(rulesData).AsEnumerable())
        {
            var key = int.Parse(match.Groups[1].ToString());
            var value = int.Parse(match.Groups[2].ToString());
            
            if (_rules.TryGetValue(key, out var values))
                values.Add(value);
            else
                _rules.Add(key, [value]);
        }

        _manuals = allData[(separatorIndex + 1)..].Select(d => d.Split(',').Select(int.Parse).ToArray()).ToList();
    }

    public int Part1(List<int[]>? manuals = null)
    {
        // An override to help with testing individual scenarios
        if (manuals is not null)
            _manuals = manuals;
        
        List<int> values = [];
        
        foreach (var manual in _manuals)
        {
            var validManual = true;
            
            for (var page = 1; page < manual.Length; page++)
            {
                var hasRules = _rules.TryGetValue(manual[page], out var pageRules);

                for (var pageToCheck = 0; pageToCheck < manual[..page].Length && hasRules; pageToCheck++)
                    for (var pp = 0; pp < manual[..page].Length && hasRules; pp++)
                        if (pageRules!.Contains(manual[pp]))
                            validManual = false;

                if (page < manual.Length - 1 || !validManual)
                    continue;
                
                values.Add(manual[(int)Math.Round((decimal)manual.Length / 2, MidpointRounding.ToZero)]);
            }
        }

        return values.Sum();
    }

    public int Part2(List<int[]>? manuals = null)
    {
        // An override to help with testing individual scenarios
        if (manuals is not null)
            _manuals = manuals;
        
        List<int> values = [];
        
        foreach (var manual in _manuals)
        {
            var validManual = true;
            
            for (var page = 1; page < manual.Length; page++)
            {
                var hasRules = _rules.TryGetValue(manual[page], out var pageRules);

                // Check the previous pages
                for (var pageToCheck = 0; pageToCheck < manual[..page].Length && hasRules; pageToCheck++)
                {
                    // Loop through the rules for the current page
                    for (var pageRuleIndex = 0; pageRuleIndex < pageRules!.Count; pageRuleIndex++)
                    {
                        // Select the next rule
                        var currentRule = pageRules.ElementAt(pageRuleIndex);

                        // Check if the current previous page matches the rule
                        if (manual[pageToCheck] != currentRule)
                            continue;

                        // Hit a rule violation
                        validManual = false;

                        var currentPage = manual[page];
                        var swapPage = manual[pageToCheck];

                        manual[page] = swapPage;
                        manual[pageToCheck] = currentPage;
                        page = 1;
                    }
                }
            }
            
            if (!validManual)
                values.Add(manual[(int)Math.Round((decimal)manual.Length / 2, MidpointRounding.ToZero)]);
        }

        return values.Sum();
    }

    [GeneratedRegex(@"(\d{2})\|(\d{2})")]
    private static partial Regex RulesRegex();
}