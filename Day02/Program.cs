// See https://aka.ms/new-console-template for more information

Console.WriteLine($"Running directory: {Directory.GetCurrentDirectory()}");

string[] input = File.ReadAllLines($"{Directory.GetCurrentDirectory()}/input.txt");

//input.ToList().ForEach(Console.WriteLine);

int[] checker = [12, 13, 14];

Console.WriteLine(GetGames());
Console.WriteLine(GetGames2());

int GetGames()
{
    int total = 0;
    foreach (var (line, index) in input.Select((line, index) => (line, index)))
    {
        var cubes = line.Split(":")[1].Split(";");
        bool valid = true;
        foreach (var cube in cubes)
        {
            var result = Eval(cube);
            for (int i = 0; i < 3; i++)
            {
                if (result[i] > checker[i])
                {
                    valid = false;
                }
            }
        }
        if (valid)
        {
            total+= index + 1;
        }
    }
    return total;
}

int GetGames2()
{
    var total = 0;
    foreach (var (line, index) in input.Select((line, index) => (line, index)))
    {
        var subtotal = 1;
        var cubes = line.Split(":")[1].Split(";");
        var max = new int[3];
        foreach (var cube in cubes)
        {
            var result = Eval(cube);
            for (var i = 0; i < 3; i++)
            {
                if (result[i] > max[i])
                {
                    max[i] = result[i];
                }
            }
        }
        for (var i = 0; i < 3; i++)
        {
            if (max[i] is not 0)
            {
                subtotal *= max[i]; 
            }
        }

        total += subtotal;
    }
    return total;
}

int[] Eval(string cube)
{
    var numbers = new Dictionary<string, int>
    {
        { "red", 0 },
        { "green", 1 },
        { "blue", 2 }
    };
    int[] result = new int[3];
    var color = cube.Split(",");
    foreach (var c in color)
    {
        var num = int.Parse(c.Split(" ")[1]);
        var pos = numbers.FirstOrDefault(pair => c.Split(" ")[2].Equals(pair.Key)).Value;
        result[pos] = num;
    }
   
    return result;
}

