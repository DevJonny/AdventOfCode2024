namespace App.Days;

public abstract class Day2
{
    public static int Part1(string dataFile)
    {
        var reports = File.ReadAllLines(dataFile);

        return reports.Sum(PartOneCheckReportsAreSafe);
    }
    
    public static int Part2(string dataFile)
    {
        var reports = File.ReadAllLines(dataFile);
        
        return reports.Sum(PartTwoCheckReportsAreSafe);
    }
    
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
    
    private static int PartTwoCheckReportsAreSafe(string report)
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
}