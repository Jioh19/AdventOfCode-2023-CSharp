// See https://aka.ms/new-console-template for more information

Console.WriteLine($"Running directory: {Directory.GetCurrentDirectory()}");

string[] input = File.ReadAllLines($"{Directory.GetCurrentDirectory()}/input.txt");

var result = CalculateSum1(input.ToList());
Console.WriteLine(result);

result = CalculateSum2(input.ToList());
Console.WriteLine(result);

int CalculateSum1(List<string> lines)
{
    if (lines.Count < 1)
    {
        return 0;
    }
    var line = lines.First();
    string num = line.FirstOrDefault(c => char.IsDigit(c)).ToString();
    num += line.LastOrDefault(c => char.IsDigit(c));
    
    lines.RemoveAt(0);
    return int.Parse(num) + CalculateSum1(lines);
}

int CalculateSum2(List<string> lines)
{
    if (lines.Count < 1)
    {
        return 0;
    }
    var num = GetStringNumber(lines.First(), false) * 10;
    num += GetStringNumber(lines.First(), true);
    lines.RemoveAt(0);
    return num + CalculateSum2(lines);
}

int GetStringNumber(string line, bool reverse)
{
    int num;
    if (line.Length is 0)
    {
        return -1;
    }
    var numbers = new Dictionary<string, int>
    {
        { "zero", 0 },
        { "one", 1 },
        { "two", 2 },
        { "three", 3 },
        { "four", 4 },
        { "five", 5 },
        { "six", 6 },
        { "seven", 7 },
        { "eight", 8 },
        { "nine", 9 }
    };
    if (reverse)
    {
        if (char.IsDigit(line[line.Length - 1]))
        {
            return int.Parse(line[line.Length - 1].ToString());
        }
        num = numbers.Where(pair => line.EndsWith(pair.Key))
            .FirstOrDefault(defaultValue: new KeyValuePair<string, int>(string.Empty, -1)).Value;
        if (num is -1)
        {
            num = GetStringNumber(line.Remove(line.Length - 1), reverse);
        }
        else
        {
            return num;
        }
    }
    else
    {
        if (char.IsDigit(line[0]))
        {
            return int.Parse(line[0].ToString());
        }
        num = numbers.Where(pair => line.StartsWith(pair.Key))
            .FirstOrDefault(defaultValue: new KeyValuePair<string, int>(string.Empty, -1)).Value;
        if (num is -1)
        {
            num = GetStringNumber(line.Substring(1), reverse);
        }
        else
        {
            return num;
        }
    }
    return num;
}