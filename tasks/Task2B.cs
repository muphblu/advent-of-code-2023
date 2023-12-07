using System.Drawing;

class Task2B
{
    public static string Solve(string[] input)
    {

        var possibleGamesSum = 0;
        foreach (var gameInput in input)
        {
            var gameInputParts = gameInput.Split(new[] { ';', ':' });
            var gameId = int.Parse(gameInputParts.First().Split(' ').Last());
            var subsets = gameInputParts.Skip(1).ToArray();
            possibleGamesSum += subsets.Select(s => s
                        .Split(',')
                        .Select(x =>
                            x.Trim().Split(' ') is [var count, var color]
                                ? (Count: int.Parse(count), Color: color)
                                : throw new Exception("Invalid input")
                        )
                    ).SelectMany(x => x)
                    .GroupBy(x => x.Color, x => x.Count)
                    .Select(g => g.Max())
                    .Aggregate(1, (a, b) => a * b);
        }

        return possibleGamesSum.ToString();
    }
}