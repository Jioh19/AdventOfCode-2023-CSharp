// See https://aka.ms/new-console-template for more information

Console.WriteLine($"Running directory: {Directory.GetCurrentDirectory()}");

var input = File.ReadAllLines($"{Directory.GetCurrentDirectory()}/input.txt");

var nav = input[0];

var map = new Dictionary<string, Node>();

InsertData();

Console.WriteLine(Part1());

Console.WriteLine(LcmArray(Part2(input[2..]
    .Where(n => n[2] is 'A')
    .Select(str => str
        .Split(" = ")[0])
    .ToArray())));
return;

void InsertData()
{
    input[2..].ToList().ForEach(str =>
    {
        var key = str.Split(" = ")[0];
        var node = AddGetNode(key);
        var next = str.Split(" = ")[1];
        var left = next.Split(", ")[0].Replace("(", "");
        var right = next.Split(", ")[1].Replace(")", "");
        node.Left = AddGetNode(left);
        node.Right = AddGetNode(right);
    });
}

Node AddGetNode(string key)
{
    if (map.TryGetValue(key, out var value)) return value;
    var node = new Node(key);
    map.Add(node.Key, node);

    return node;
}

int Part1()
{
    var total = 0;
    var node = map[input[2].Split(" = ")[0]];

    while (node?.Key is not "ZZZ")
    {
        var direction = nav[total % nav.Length];
        node = direction is 'L' ? node?.Left : node?.Right;
        total++;
    }
    return total;
}

long[] Part2(string[] starts)
{
    var total = new long[starts.Length];

    Parallel.ForEach(
        starts.Select((start, index) => (start, index)),
        pair =>
        {
            var (start, index) = pair;
            var node = map[start];
            var steps = total[index]; 

            while (node?.Key[2] is not 'Z')
            {
                var direction = nav[(Index)(steps % nav.Length)];
                node = direction is 'L' ? node?.Left : node?.Right;
                steps++;
            }

            total[index] = steps;
        });
    return total;
}

long Gcd(long a, long b)
{
    while (b != 0)
    {
        var temp = b;
        b = a % b;
        a = temp;
    }
    return Math.Abs(a);
}

long Lcm(long a, long b)
{
    return Math.Abs(a / Gcd(a, b) * b);
}

long LcmArray(long[] numbers)
{
    return numbers.Aggregate(Lcm);
}
internal class Node(string key)
{
    public string Key { get; set; } = key;
    public Node? Left { get; set; }
    public Node? Right { get; set; }
}