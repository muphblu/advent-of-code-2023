class Task6A
{
    public static string Solve(string[] input)
    {
        var time = input[0].Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse);
        var distance = input[1].Split(' ', StringSplitOptions.RemoveEmptyEntries).Skip(1).Select(int.Parse);
        var races = time.Zip(distance, (t, d) => (Time: t, Distance: d));
        var result = 1;

        foreach (var (Time, Distance) in races)
        {
            for (int i = 1; i < Time; i++)
            {
                if (Distance < i * (Time - i))
                {
                    result *= Time - i - i + 1;
                    break;
                }
            }
        }
        return result.ToString();
    }
}