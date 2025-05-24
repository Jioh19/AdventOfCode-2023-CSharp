// See https://aka.ms/new-console-template for more information

Console.WriteLine($"Running directory: {Directory.GetCurrentDirectory()}");

var input = File.ReadAllLines($"{Directory.GetCurrentDirectory()}/test.txt");
input.ToList().ForEach(Console.WriteLine);

Part1();
return;

int GetValue(string line)
{
    var first = 0;
    var second = 0;
    var characters = line.Split(" ")[0].Distinct().ToArray(); 
    foreach (var t in characters)
    {
        var count = line.Count(c => c == t);
        if (first > count) continue;
        //Console.WriteLine(count);
        second = first;
        first = count;
    }

    return (first, second) switch
    {
        (5, _) => 7,
        (4, _) => 6,
        (3, 2) => 5,
        (3, _) => 4,
        (2, 2) => 3,
        (2, _) => 2,
        _ => 1
    };
}

List<int> GetHigh(string line)
{
    var result = new List<int>();
    foreach (var c in line)
    { 
        switch (c)
        {
            case >= '0' and <= '9':
                var num = int.Parse(c.ToString());
                result.Add(num);
                break;
            case 'T':
                result.Add(10);
                break;
            case 'J':
                result.Add(11);
                break;
            case 'Q':
                result.Add(12);
                break;
            case 'K':
                result.Add(13);
                break;
            case 'A':
                result.Add(14);
                break;
        }
    }
    return result;
}
void Part1()
{
    foreach (var line in input.ToList().OrderByDescending(GetValue))
    {
        Console.WriteLine(GetValue(line));
    }
}

public class ArrayComparer : IComparer<int[]>
{
    public int Compare(int[]? x, int[]? y)
    {
        ArgumentNullException.ThrowIfNull(x);
        ArgumentNullException.ThrowIfNull(y);
        var minLength = Math.Min(x.Length, y.Length);
        
        for (var i = 0; i < minLength; i++)
        {
            var comparison = x[i].CompareTo(y[i]);
            if (comparison != 0)
                return comparison;
        }
        return x.Length.CompareTo(y.Length);
    }
}