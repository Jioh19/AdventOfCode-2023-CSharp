// See https://aka.ms/new-console-template for more information

Console.WriteLine($"Running directory: {Directory.GetCurrentDirectory()}");

var input = File.ReadAllLines($"{Directory.GetCurrentDirectory()}/input.txt");

var seeds = input[0].Split(':')[1].Trim().Split(' ').Select(long.Parse).ToArray();
var index = 2;
var soil = InsertChart();
var fertilizer = InsertChart();
var water = InsertChart();
var light = InsertChart();
var temp = InsertChart();
var humidity = InsertChart();
var location = InsertChart();

Part1();
Part2();
return;

List<long[]> InsertChart()
{
    index++;
    var charts = new List<long[]>();
    for (var i = index; i < input.Length && input[i] is not ""; i++)
    {
        var chart = input[i].Split(" ").Select(long.Parse).ToArray();
        charts.Add(chart);
        index++;
    }

    index++;
    return charts;
}

long Charting(List<long[]> charts, long seed)
{
    var result = seed;
    foreach (var chart in charts)
    {
        if (seed >= chart[1] && seed < chart[1] + chart[2])
        {
            result = chart[0] + seed - chart[1];
        }
    }

    return result;
}

void Part1()
{
    var min = long.MaxValue;

    foreach (var seed in seeds)
    {
        var s = Charting(soil, seed);
        s = Charting(fertilizer, s);
        s = Charting(water, s);
        s = Charting(light, s);
        s = Charting(temp, s);
        s = Charting(humidity, s);
        s = Charting(location, s);

        if (min > s)
        {
            min = s;
        }
    }

    Console.WriteLine($"{min}");
}

void Part2()
{
    var min = long.MaxValue;
    var lockObject = new object();
    for (var i = 0; i < seeds.Length; i += 2)
    {
        var seedStart = seeds[i];
        var rangeLength = seeds[i + 1];

        Parallel.For(0, rangeLength + 1, j =>
        {
            var s = Charting(soil, seedStart + j);
            s = Charting(fertilizer, s);
            s = Charting(water, s);
            s = Charting(light, s);
            s = Charting(temp, s);
            s = Charting(humidity, s);
            s = Charting(location, s);

            if (s >= min) return;
            lock (lockObject)
            {
                if (s >= min) return;
                min = s;
                Console.WriteLine($"New minimum: {min}");
            }
        });
    }
    Console.WriteLine($"{min}");
}