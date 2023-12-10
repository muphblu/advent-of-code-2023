class Task5B
{
    public static string Solve(string[] input)
    {
        #region Parse input
        var seedsInput = input[0].Split(' ').Skip(1).Select(long.Parse).ToArray();
        var seedPairsTemp = new List<(long Start, long Range)>();
        for (int i = 0; i < seedsInput.Length; i += 2)
        {
            seedPairsTemp.Add((seedsInput[i], seedsInput[i + 1]));
        }
        var seedPairs = new Stack<(long Start, long Range)>(seedPairsTemp.OrderByDescending(x => x.Start));

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
            maps[i] = maps[i].OrderBy(x => x.Source).ToList();
        }

        #endregion
        for (int i = 0; i < maps.Length; i++)
        {
            var newSeedPairs = new List<(long Start, long Range)>();
            while (seedPairs.TryPop(out var seedPair))
            {
                if (seedPair.Start >= maps[i].Last().Source + maps[i].Last().Range)
                {
                    newSeedPairs.Add(seedPair);
                    continue;
                }

                foreach (var map in maps[i])
                {
                    if (map.Source + map.Range <= seedPair.Start)
                        continue;

                    if (map.Source > seedPair.Start && map.Source + seedPair.Range < seedPair.Start is var IsBeforeInRange)
                    {
                        newSeedPairs.Add((seedPair.Start, Math.Min(map.Source - seedPair.Start, seedPair.Range)));
                        if (!IsBeforeInRange)
                            seedPairs.Push((map.Source, seedPair.Start + seedPair.Range - map.Source));

                        break;
                    }
                    if (map.Source <= seedPair.Start && seedPair.Start < map.Source + map.Range &&
                        seedPair.Start + seedPair.Range <= map.Source + map.Range is var IsInRange)
                    {
                        newSeedPairs.Add((map.Destination - map.Source + seedPair.Start,
                                          Math.Min(seedPair.Range, map.Source + map.Range - seedPair.Start)));
                        if (!IsInRange)
                            seedPairs.Push((map.Source + map.Range, seedPair.Start + seedPair.Range - map.Source - map.Range));

                        break;
                    }

                    // .FirstOrDefault(x => x.Source <= seeds[j] && seeds[j] < x.Source + x.Range) is var map
                    // ? map.Destination + seeds[j] - map.Source
                    // : seeds[j]
                }
            }
            seedPairs = new Stack<(long Start, long Range)>(newSeedPairs.OrderByDescending(x => x.Start));
        }

        return seedPairs.First().Start.ToString();
    }
}