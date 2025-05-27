// See https://aka.ms/new-console-template for more information

Console.WriteLine($"Running directory: {Directory.GetCurrentDirectory()}");

var input = File.ReadAllLines($"{Directory.GetCurrentDirectory()}/test.txt");

input.ToList().ForEach(Console.WriteLine);

var line = input.ToList()[0].Split(' ').ToList();

var result = line.ToList().Select(int.Parse).Skip(1)
    .Aggregate(
        new { Prev = int.Parse(line.ToList()[0]), Diffs = new List<int>() },
        (acc, curr) => new {
            Prev = curr,
            Diffs = new List<int>(acc.Diffs) { curr - acc.Prev }
        },
        acc => acc.Diffs
    );

result.ToList().ForEach(Console.WriteLine);