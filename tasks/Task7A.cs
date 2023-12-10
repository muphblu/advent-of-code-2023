class Task7A
{
    public static string Solve(string[] input)
    {
        var hands = input.Select(x => x.Split(' ') is var parts
                                    ? new Hand(parts[0], int.Parse(parts[1]))
                                    : throw new Exception("Invalid input")).ToArray();
        Array.Sort(hands, (x, y) => x.Type.CompareTo(y.Type) is var typeCmp && typeCmp == 0
                                                ? x.CardsAlph.CompareTo(y.CardsAlph)
                                                : typeCmp);

        return hands.Select((x, i) => x.Bid * (i + 1)).Sum().ToString();
    }
    record Hand(string Cards, int Bid)
    {
        private string? cardsAlph;

        public string CardsAlph => cardsAlph ??= new string(Cards.Select(ToAlphabetCard).ToArray());

        private Type? type;
        public Type Type => type ??= CalculateType();

        private Type CalculateType()
        {
            return CardsAlph.GroupBy(x => x).ToArray() switch
            {
                { Length: 1 } => Type.FiveofAKind,
                var x when x.Length == 2 => x[0].Count() is var count && count is 1 or 4
                                                ? Type.FourOfAKind
                                                : Type.FullHouse,
                var x when x.Length == 3 => x.Any(y => y.Count() == 3) ? Type.ThreeOfAKind : Type.TwoPairs,
                { Length: 4 } => Type.Pair,
                { Length: 5 } => Type.HighCard,
                _ => throw new Exception("Invalid hand")
            };
        }

        private char ToAlphabetCard(char c) => c switch
        {
            'A' => 'E',
            'K' => 'D',
            'Q' => 'C',
            'J' => 'B',
            'T' => 'A',
            _ => c
        };
    }

    enum Type
    {
        HighCard,
        Pair,
        TwoPairs,
        ThreeOfAKind,
        Flush,
        FullHouse,
        FourOfAKind,
        FiveofAKind
    }
}