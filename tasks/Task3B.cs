using System.Diagnostics;

class Task3B
{
    private static readonly HashSet<Point> knownParts = new HashSet<Point>();
    private static readonly HashSet<(Point Point, int Number)> potentialGears = new();
    private static int width;
    private static int height;
    private static string[] map;
    public static string Solve(string[] input)
    {
        map = input;
        width = input[0].Length;
        height = input.Length;
        var countNonDetails = 0;
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                if (input[i][j] is var element && char.IsDigit(element) &&
                    new Point(i, j) is var point && !knownParts.Contains(point))
                {
                    var strNumber = element.ToString();
                    var rightPointer = j + 1;
                    while (point with { Y = rightPointer } is var pointRight && pointRight.Y < width && char.IsDigit(input[pointRight.X][pointRight.Y]))
                    {
                        strNumber += input[pointRight.X][pointRight.Y];
                        rightPointer++;
                    }

                    var number = int.Parse(strNumber);
                    if (IsDetail(new Point(i, j), number))
                    {
                        Debug.WriteLine($"{number} {point}");
                        countNonDetails += number;
                    }
                }
            }
        }


        var gears = potentialGears.GroupBy(x => x.Point, x => x.Number).Where(x => x.Count() == 2);
        Debug.WriteLine(string.Join(", ", gears.Select(x => $"{x.Key} {x.First()} {x.Last()}")));
        return gears.Sum(x => x.First() * x.Last()).ToString();
    }

    static bool IsDetail(Point point, int number)
    {
        if (knownParts.Contains(point) || map[point.X][point.Y] == '.')
            return false;

        if (map[point.X][point.Y] is var element && !char.IsDigit(element) is var isDetail && isDetail)
        {
            if (element == '*') 
                potentialGears.Add((point, number));
            return true;
        }

        knownParts.Add(point);
        if (point with { X = point.X + 1 } is var pointDown && pointDown.X < height)
            isDetail |= IsDetail(pointDown, number);
        if (point with { X = point.X - 1 } is var pointUp && pointUp.X >= 0)
            isDetail |= IsDetail(pointUp, number);
        if (point with { Y = point.Y + 1 } is var pointRight && pointRight.Y < width)
            isDetail |= IsDetail(pointRight, number);
        if (point with { Y = point.Y - 1 } is var pointLeft && pointLeft.Y >= 0)
            isDetail |= IsDetail(pointLeft, number);

        // Diagonals
        if (point with { X = point.X + 1, Y = point.Y + 1 } is var pointDownRight && pointDownRight.X < height && pointDownRight.Y < width)
            isDetail |= IsDetail(pointDownRight, number);
        if (point with { X = point.X + 1, Y = point.Y - 1 } is var pointDownLeft && pointDownLeft.X < height && pointDownLeft.Y >= 0)
            isDetail |= IsDetail(pointDownLeft, number);
        if (point with { X = point.X - 1, Y = point.Y + 1 } is var pointUpRight && pointUpRight.X >= 0 && pointUpRight.Y < width)
            isDetail |= IsDetail(pointUpRight, number);
        if (point with { X = point.X - 1, Y = point.Y - 1 } is var pointUpLeft && pointUpLeft.X >= 0 && pointUpLeft.Y >= 0)
            isDetail |= IsDetail(pointUpLeft, number);

        return isDetail;
    }

    record Point(int X, int Y);
}