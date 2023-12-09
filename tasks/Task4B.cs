class Task4B
{
    public static string Solve(string[] input)
    {
        var cardCopies = new List<int>(Enumerable.Repeat(1, input.Length));
        foreach (var cardInput in input)
        {
            var cardInputParts = cardInput.Split(new[] { '|', ':' });
            var cardId = int.Parse(cardInputParts.First().Split(' ').Last());
            var winningNumbers = cardInputParts[1].Trim().Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToHashSet();
            var numbers = cardInputParts.Last().Trim().Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToArray();
            var cardMatches = numbers.Count(winningNumbers.Contains);
            if (cardMatches == 0)
                continue;

            for (int i = cardId; i < Math.Min(cardCopies.Count, cardId + cardMatches); i++)
            {
                cardCopies[i] += cardCopies[cardId - 1];
            }
        }

        return cardCopies.Sum().ToString();
    }
}