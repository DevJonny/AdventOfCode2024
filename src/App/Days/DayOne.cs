namespace App.Days;

public abstract class DayOne
{
    public static int PartOne(string dataFile)
    {
        var data = File.ReadAllLines(dataFile);
    
        var teamOne = data.Select(d => int.Parse(d.Split("   ")[0])).OrderBy(d => d).ToArray();
        var teamTwo = data.Select(d => int.Parse(d.Split("   ")[1])).OrderBy(d => d).ToArray();

        List<int> diffs = [];

        for (var i = 0; i < teamOne.Length; i++)
        {
            if (teamOne[i] > teamTwo[i])
                diffs.Add(teamOne[i] - teamTwo[i]);
            else
                diffs.Add(teamTwo[i] - teamOne[i]);
        }
        
        return diffs.Sum();
    }

    public static int PartTwo(string dataFile)
    {
        var data = File.ReadAllLines(dataFile);
        
        var teamOne = data.Select(d => int.Parse(d.Split("   ")[0])).ToArray();
        var teamTwo = data
            .Select(d => int.Parse(d.Split("   ")[1]))
            .GroupBy(d => d)
            .ToDictionary(grp => grp.Key, grp => grp.Count());

        var counts = teamOne.Select(t => t * teamTwo.GetValueOrDefault(t, 0));
        
        return counts.Sum();
    }
}