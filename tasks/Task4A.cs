class Task4A
{
    public static string Solve(string[] input)
    {
        var sumPoints = 0;
        foreach (var cardInput in input)
        {
            var cardInputParts = cardInput.Split(new[] { '|', ':' });
            var cardId = int.Parse(cardInputParts.First().Split(' ').Last());
            var winningNumbers = cardInputParts[1].Trim().Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToHashSet();
            var numbers = cardInputParts.Last().Trim().Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            var cardMatches = numbers.Count(winningNumbers.Contains);
            if (cardMatches == 0)
                continue;

            sumPoints += 1 << (cardMatches - 1);
        }

        return sumPoints.ToString();
    }
}