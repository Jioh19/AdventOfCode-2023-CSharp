// See https://aka.ms/new-console-template for more information

Console.WriteLine($"Running directory: {Directory.GetCurrentDirectory()}");

string[] input = File.ReadAllLines($"{Directory.GetCurrentDirectory()}/test.txt");

input.ToList().ForEach(Console.WriteLine);