// See https://aka.ms/new-console-template for more information

Console.WriteLine($"Running directory: {Directory.GetCurrentDirectory()}");

var input = File.ReadAllLines($"{Directory.GetCurrentDirectory()}/input.txt");

var cards = input.ToList()
    .Select(row => row.Split(":")[1].Split("|")[0].Split(" ", StringSplitOptions.RemoveEmptyEntries)
        .Select(int.Parse)
        .ToArray())
    .ToList();

var winners = input.ToList()
    .Select(row => row.Split(":")[1].Split("|")[1].Split(" ", StringSplitOptions.RemoveEmptyEntries)
        .Select(int.Parse)
        .ToArray())
    .ToList();

Console.WriteLine(Part1());
Console.WriteLine(Part2());
return;


int Part1()
{
    var total = 0;
    var won = cards
        .Select((game, index) => game.Intersect(winners[index]).ToArray())
        .ToList();
    foreach (var numbers in won)
    {
        total += (int)Math.Pow(2, numbers.Length - 1);
    }

    return total;
}

int Part2()
{
    var won = cards
        .Select((game, index) => game.Intersect(winners[index]).ToArray())
        .ToList();
    int[] tickets = Enumerable.Repeat(1, won.Count).ToArray();
    var index = 0;
    foreach (var numbers in won)
    {
        for (var i = 1; i <= numbers.Length && i + index < won.Count; i++)
        {
            tickets[i + index] += tickets[index];
        }

        index++;
    }
    
    return tickets.Sum();
}