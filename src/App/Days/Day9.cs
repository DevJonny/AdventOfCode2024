using Xunit.Abstractions;

namespace App.Days;

public class Day9
{
    private readonly string _data;
    private readonly Queue<FreeSpace> _freeSpaces = new();
    private readonly Stack<FileOnDisk> _files = new();
    
    private string _defraggedData = string.Empty;

    private readonly ITestOutputHelper _testOutputHelper;
    
    public Day9(string dataFile, ITestOutputHelper testOutputHelper)
    {
        _data = File.ReadAllText(dataFile);
        _testOutputHelper = testOutputHelper;
    }
    
    public int Run()
    {
        ParseFile();
        
        _testOutputHelper.WriteLine(_defraggedData);
        
        return 0;
    }

    private void ParseFile()
    {
        var filesSpan = _data.ToCharArray();
        var file = new FileOnDisk(0, filesSpan[0].ToInt());
        
        _defraggedData += file.Expanded;
        
        for (var i = 0; i < filesSpan.Length; i++)
        {
            if (i % 2 == 0)
                _files.Push(new(i, filesSpan[i].ToInt()));
            else
                _freeSpaces.Enqueue(new(filesSpan[i].ToInt()));
        }
    }
}

public record FreeSpace(int Size);

public record FileOnDisk(int Id, int Size)
{
    public string Expanded => string.Concat(Enumerable.Repeat($"{Id}", Size));
}

public static class Extensions
{
    public static int ToInt(this char c) => int.Parse(c.ToString());
}