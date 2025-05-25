// See https://aka.ms/new-console-template for more information

Console.WriteLine($"Running directory: {Directory.GetCurrentDirectory()}");

var input = File.ReadAllLines($"{Directory.GetCurrentDirectory()}/input.txt");

Part1();
Part2();
return;

int GetValue(string line)
{
    var first = 0;
    var second = 0;
    var characters = line.Split(" ")[0].Distinct().ToArray();
    //Console.WriteLine(string.Join(" ", characters));
    foreach (var t in characters)
    {
        var count = line.Split(" ")[0].Count(c => c == t);
        if (first < count)
        {
            second = first;
            first = count;
        }
        else if (second < count)
        {
            second = count;
        }

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

int GetValue2(string line)
{
    var first = 0;
    var second = 0;
    var characters = line.Split(" ")[0].Distinct().ToArray();
    //Console.WriteLine(string.Join(" ", characters));
    foreach (var t in characters)
    {
        var count = line.Split(" ")[0].Count(c => c == t);
        if (t is not 'J')
        {
            count += line.Split(" ")[0].Count(c => c == 'J');
        }
        
        if (first < count)
        {
            second = first - line.Split(" ")[0].Count(c => c == 'J');
            first = count;
        }
        else if (second < count)
        {
            second = count - line.Split(" ")[0].Count(c => c == 'J');
        }

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
    if (string.IsNullOrEmpty(line))
        return [];

    var hand = line.Split(" ")[0];
    return hand.Select(c => c switch
    {
        >= '2' and <= '9' => int.Parse(c.ToString()),
        'T' => 10,
        'J' => 11,
        'Q' => 12,
        'K' => 13,
        'A' => 14,
        _ => 0
    }).ToList();
}

List<int> GetHigh2(string line)
{
    if (string.IsNullOrEmpty(line))
        return [];

    var hand = line.Split(" ")[0];
    return hand.Select(c => c switch
    {
        >= '2' and <= '9' => int.Parse(c.ToString()),
        'T' => 10,
        'J' => 1,
        'Q' => 12,
        'K' => 13,
        'A' => 14,
        _ => 0
    }).ToList();
}

void Part1()
{
    var orderedHands = input.OrderBy(GetValue).ThenBy(GetHigh, new ListComparer());

    var total = 0;

    foreach (var (line, index) in orderedHands.Select((value, i) => (value, i)))
    {
       // Console.WriteLine($"{line} index: {index} value: {GetValue(line)}");
        total += int.Parse(line.Split(" ")[1]) * (index + 1);
    }

    Console.WriteLine($"Total: {total}");
}

void Part2()
{
    var orderedHands = input.OrderBy(GetValue2).ThenBy(GetHigh2, new ListComparer());

    var total = 0;

    foreach (var (line, index) in orderedHands.Select((value, i) => (value, i)))
    {
        // Console.WriteLine($"{line} index: {index} value: {GetValue(line)}");
        total += int.Parse(line.Split(" ")[1]) * (index + 1);
    }

    Console.WriteLine($"Total: {total}");
}

internal class ListComparer : IComparer<List<int>>
{
    public int Compare(List<int>? x, List<int>? y)
    {
        if (x == null || y == null)
            return 0;

        var minLength = Math.Min(x.Count, y.Count);

        for (var i = 0; i < minLength; i++)
        {
            var comparison = x[i].CompareTo(y[i]);
            if (comparison != 0)
                return comparison;
        }

        return x.Count.CompareTo(y.Count);
    }
}