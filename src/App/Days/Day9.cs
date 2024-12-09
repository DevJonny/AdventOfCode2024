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

        var currentFile = _files.Pop();
        var freeSpace = _freeSpaces.Dequeue();
        
        while (_freeSpaces.Count != 0)
        {
            _defraggedData += currentFile.Allocate(freeSpace.UnallocatedSpace, out var allocatedSize);
            freeSpace.Allocate(allocatedSize);
            
            // If the file has more data, pop a free space and continue
            if (currentFile.HasMoreData)
            {
                freeSpace = _freeSpaces.Dequeue();
                continue;
            }
           
            // If the file has no more data AND there is more free space, pop a new file and continue
            if (freeSpace.UnallocatedSpace > 0)
            {
                currentFile = _files.Pop();
                continue;
            }
            
            freeSpace = _freeSpaces.Dequeue();
            currentFile = _files.Pop();
        }
        
        _testOutputHelper.WriteLine($"Final data: {_defraggedData}");
        
        return 0;
    }

    private void ParseFile()
    {
        var filesSpan = _data.ToCharArray();
        var file = new FileOnDisk(0, filesSpan[0].ToInt());
        
        _defraggedData += file.ToString();

        var nextFileId = 0;
        
        for (var i = 0; i < filesSpan.Length; i++)
        {
            if (i % 2 == 0)
            {
                _files.Push(new(nextFileId, filesSpan[i].ToInt()));
                nextFileId++;
            }
            else
                _freeSpaces.Enqueue(new(filesSpan[i].ToInt()));
        }
    }
}

public class FreeSpace(int size)
{
    public int OriginalSize { get; } = size;
    public int UnallocatedSpace { get; private set; } = size;

    public int Allocate(int size)
        => UnallocatedSpace -= size;
}

public class FileOnDisk(int id, int size)
{
    public int Id => id;
    public int OriginalSize => Size;
    public int Size {get; private set;} = size;

    public string Allocate(int allocationSize, out int allocatedSize)
    {
        if (Size >= allocationSize)
        {
            Size -= allocationSize;
            allocatedSize = allocationSize;
        }
        else
        {
            allocatedSize = Size;
            Size = 0;
        }
        
        return string.Concat(Enumerable.Repeat($"{id}", allocatedSize));
    }
    
    public bool HasMoreData => Size > 0;
    
    public override string ToString() 
        => string.Concat(Enumerable.Repeat($"{id}", Size));
}

public static class Extensions
{
    public static int ToInt(this char c) => int.Parse(c.ToString());
}