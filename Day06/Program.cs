// See https://aka.ms/new-console-template for more information

Console.WriteLine($"Running directory: {Directory.GetCurrentDirectory()}");

var input = File.ReadAllLines($"{Directory.GetCurrentDirectory()}/input.txt");

var times = input[0].Split(":")[1]
    .Trim()
    .Split(" ")
    .Select(time => time.Trim())
    .Where(time => time.Length > 0)
    .Select(int.Parse)
    .ToArray();
var distances = input[1].Split(":")[1]
    .Trim()
    .Split(" ")
    .Select(time => time.Trim())
    .Where(time => time.Length > 0)
    .Select(int.Parse)
    .ToArray();

Console.WriteLine($"{Part1()}");
Console.WriteLine($"{Part2()}");
return;

int Part1()
{
    var total = 1;
    for (var j = 0; j < times.Length; j++)
    {
        var subtotal = 0;
        for (var i = 1; i < times[j]; i++)
        {
            if (i * (times[j] - i) <= distances[j]) continue;
            subtotal++;
        }
        total *= subtotal;
    }
    return total;
}

long Part2()
{
    var totalTime = long.Parse(string.Concat(times));
    var totalDistance = long.Parse(string.Concat(distances));
    
    long total = 0;
    for (long i = 1; i < totalTime; i++)
    {
        if (i * (totalTime - i) <= totalDistance) continue;
        total++;
    }
    return total;
}