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
return;

int Part1()
{
    var total = 1;
    for (var j = 0; j < times.Length; j++)
    {
        var subtotal = 0;
        for (var i = 0; i < times[j]; i++)
        {
            if (i * (times[j] - i) <= distances[j]) continue;
            subtotal++;
        }

        total *= subtotal;
    }

    return total;
}