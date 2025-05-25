// See https://aka.ms/new-console-template for more information

Console.WriteLine($"Running directory: {Directory.GetCurrentDirectory()}");

var input = File.ReadAllLines($"{Directory.GetCurrentDirectory()}/test.txt");

input.ToList().ForEach(Console.WriteLine);

internal class Node
{
    public string Key;
    Node left, right;

    public Node(string key, Node left, Node right)
    {
        Key = key;
        this.left = left;
        this.right = right;
    }
}