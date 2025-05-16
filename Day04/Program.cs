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

Console.WriteLine(Part1(cards, winners));


int Part1(List<int[]> cards, List<int[]> winners)
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
