using System.Diagnostics;

class Task3A
{
    private static readonly HashSet<Point> knownParts = new HashSet<Point>();
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
                    if (IsDetail(new Point(i, j)))
                    {
                        Debug.WriteLine($"{number} {point}");
                        countNonDetails += number;
                    }
                }
            }
        }

        return countNonDetails.ToString();
    }

    static bool IsDetail(Point point)
    {
        if (knownParts.Contains(point) || map[point.X][point.Y] == '.')
            return false;

        if(!char.IsDigit(map[point.X][point.Y]) is var isDetail && isDetail)
            return true;

        knownParts.Add(point);
        if (point with { X = point.X + 1 } is var pointDown && pointDown.X < height)
            isDetail |= IsDetail(pointDown);
        if (point with { X = point.X - 1 } is var pointUp && pointUp.X >= 0)
            isDetail |= IsDetail(pointUp);
        if (point with { Y = point.Y + 1 } is var pointRight && pointRight.Y < width)
            isDetail |= IsDetail(pointRight);
        if (point with { Y = point.Y - 1 } is var pointLeft && pointLeft.Y >= 0)
            isDetail |= IsDetail(pointLeft);
        // Diagonals
        if (point with { X = point.X + 1, Y = point.Y + 1 } is var pointDownRight && pointDownRight.X < height && pointDownRight.Y < width)
            isDetail |= IsDetail(pointDownRight);
        if (point with { X = point.X + 1, Y = point.Y - 1 } is var pointDownLeft && pointDownLeft.X < height && pointDownLeft.Y >= 0)
            isDetail |= IsDetail(pointDownLeft);
        if (point with { X = point.X - 1, Y = point.Y + 1 } is var pointUpRight && pointUpRight.X >= 0 && pointUpRight.Y < width)
            isDetail |= IsDetail(pointUpRight);
        if (point with { X = point.X - 1, Y = point.Y - 1 } is var pointUpLeft && pointUpLeft.X >= 0 && pointUpLeft.Y >= 0)
            isDetail |= IsDetail(pointUpLeft);

        return isDetail;
    }

    record Point(int X, int Y);
}