// See https://aka.ms/new-console-template for more information

Console.WriteLine($"Running directory: {Directory.GetCurrentDirectory()}");

var input = File.ReadAllLines($"{Directory.GetCurrentDirectory()}/input.txt");


var coords = input.SelectMany((row, y) =>
        row.Select((character, x) => (character, x, y)))
    .Where(item => !char.IsDigit(item.character) && item.character != '.')
    .Select(item => (item.x, item.y))
    .ToList();

var coords2 = input.SelectMany((row, y) =>
        row.Select((character, x) => (character, x, y)))
    .Where(item => item.character == '*')
    .Select(item => (item.x, item.y))
    .ToList();

//Console.WriteLine(Part1(input));
Console.WriteLine(Part2(input));
return;

string GetStrNumber(string[] grid, int x, int y)
{
    if (x < 0 || x >= grid.Length || !char.IsDigit(grid[y][x]))
    {
        return "";
    }

    var num = grid[y][x].ToString();
    var rowChars = grid[y].ToCharArray();
    rowChars[x] = '.';
    grid[y] = new string(rowChars);
    return GetStrNumber(grid, x - 1, y) + num + GetStrNumber(grid, x + 1, y);
}

int Part1(string[] grid)
{
    var total = 0;
    foreach (var coord in coords)
    {
        for (var i = -1; i <= 1; i++)
        {
            for (var j = -1; j <= 1; j++)
            {
                if (i + coord.x < 0 || i + coord.x >= grid[0].Length || j + coord.y < 0 || j + coord.y >= grid.Length ||
                    !char.IsDigit(grid[j + coord.y][i + coord.x]))
                {
                    continue;
                }

                total += int.Parse(GetStrNumber(grid, i + coord.x, j + coord.y));
            }
        }
    }

    return total;
}

int Part2(string[] grid)
{
    var total = 0;
    foreach (var coord in coords2)
    {
        var num1 = 0;
        for (var i = -1; i <= 1; i++)
        {
            for (var j = -1; j <= 1; j++)
            {
                if (i + coord.x < 0 || i + coord.x >= grid[0].Length || j + coord.y < 0 || j + coord.y >= grid.Length ||
                    !char.IsDigit(grid[j + coord.y][i + coord.x]))
                {
                    continue;
                }

                if (num1 == 0)
                {
                    num1 = int.Parse(GetStrNumber(grid, i + coord.x, j + coord.y));
                }
                else
                {
                    var num2 = int.Parse(GetStrNumber(grid, i + coord.x, j + coord.y));
                    total += num1 * num2;
                }
            }
        }
    }

    return total;
}