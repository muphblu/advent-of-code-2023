class Task5A
{
    public static string Solve(string[] input)
    {
        var seeds = input[0].Split(' ').Skip(1).Select(long.Parse).ToArray();
        var mapIndex = -1;
        var maps = new List<(long Destination, long Source, long Range)>[7];
        for (var i = 1; i < input.Length; i++)
        {
            if (input[i] == string.Empty)
                continue;
            if (input[i].Split(new[] { " ", "-to-" }, StringSplitOptions.None) is var splitted && splitted is [var from, var to, "map:"])
            {
                mapIndex++;
                continue;
            }

            if (!(splitted is [var destinationStr, var sourceStr, var rangeStr] &&
                long.TryParse(rangeStr, out var range) &&
                long.TryParse(sourceStr, out var source) &&
                long.TryParse(destinationStr, out var destination)))
                throw new Exception("Invalid input");

            if (maps[mapIndex] == null) maps[mapIndex] = new List<(long Destination, long Source, long Range)>();
            maps[mapIndex].Add((destination, source, range));
        }

        for (int i = 0; i < maps.Length; i++)
        {
            for (int j = 0; j < seeds.Length; j++)
            {
                seeds[j] = maps[i].FirstOrDefault(x => x.Source <= seeds[j] && seeds[j] < x.Source + x.Range) is var map
                    ? map.Destination + seeds[j] - map.Source
                    : seeds[j];
            }
        }

        return seeds.Min().ToString();
    }
}