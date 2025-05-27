// See https://aka.ms/new-console-template for more information

Console.WriteLine($"Running directory: {Directory.GetCurrentDirectory()}");

var input = File.ReadAllLines($"{Directory.GetCurrentDirectory()}/input.txt");

//input[2..].ToList().ForEach(Console.WriteLine);

var nav = input[0];

var map = new Dictionary<string, Node>();

InsertData();

// Console.WriteLine(nav);
// foreach (var keyValuePair in map)
// {
//     Console.WriteLine($"{keyValuePair.Key} - {keyValuePair.Value.Left?.Key} - {keyValuePair.Value.Right?.Key}");
// }

Console.WriteLine(Part1());
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
        //  Console.WriteLine($"{key} {left} {right}");
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
   // Console.WriteLine(node.Key);
    while (node?.Key is not "ZZZ")
    {
        var direction = nav[total % nav.Length];
        node = direction is 'L' ? node?.Left : node?.Right;
       // Console.WriteLine(node?.Key );
        total++;
    }

    return total;
}

internal class Node(string key)
{
    public string Key { get; set; } = key;
    public Node? Left { get; set; }
    public Node? Right { get; set; }
}