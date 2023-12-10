class Task6B
{
    public static string Solve(string[] input)
    {
        var Time = long.Parse(input[0].Replace("Time:", "").Replace(" ", ""));
        var Distance = long.Parse(input[1].Replace("Distance:", "").Replace(" ", ""));

        for (long i = 1; i < Time; i++)
        {
            if (Distance < i * (Time - i))
            {
                return (Time - i - i + 1).ToString();
            }
        }

        throw new System.Exception("No solution found");
    }
}