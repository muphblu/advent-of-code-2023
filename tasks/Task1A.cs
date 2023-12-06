class Task1A
{
    public static string Solve(string[] input)
    {
        return input.Select(l => (l.First(char.IsDigit)-'0') * 10 + l.Last(char.IsDigit)-'0').Sum().ToString();
    }
}