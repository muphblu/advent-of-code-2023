class Task2A
{
    public static string Solve(string[] input)
    {

        var possibleGamesSum = 0;
        foreach (var gameInput in input)
        {
            //     if(!(gameInput.Split(new[] {';', ':'}) is [var _, .. var subsets]))
            //         throw new Exception("Invalid input");
            var gameInputParts = gameInput.Split(new[] { ';', ':' });
            var gameId = int.Parse(gameInputParts.First().Split(' ').Last());
            var subsets = gameInputParts.Skip(1).ToArray();
            var possibleGame = subsets.Select(s => s
                        .Split(',')
                        .Select(x =>
                            x.Trim().Split(' ') is [var count, var color]
                                ? (Count: int.Parse(count), Color: color)
                                : throw new Exception("Invalid input")
                        )
                    ).SelectMany(x => x).All(x => x.Count <= GetMaxCubesByColor(x.Color));
            if (possibleGame)
                possibleGamesSum += gameId;
        }

        return possibleGamesSum.ToString();

        static int GetMaxCubesByColor(string color) => color switch
        {
            "red" => 12,
            "green" => 13,
            "blue" => 14,
            _ => throw new Exception("Invalid color")
        };
    }
}