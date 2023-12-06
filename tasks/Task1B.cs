using System.Diagnostics;

class Task1B
{
    public static string Solve(string[] input)
    {
        var digits = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
        var result = 0;
        foreach (var line in input)
        {
            var pointer = 0;
            string? first = default;
            string? last = default;
            while (pointer < line.Length)
            {
                var increment = 1;
                foreach (var digit in digits)
                {
                    if (line.Length < pointer + digit.Length)
                        continue;
                    if (line.Substring(pointer, digit.Length) == digit)
                    {
                        first ??= digit;
                        last = digit;
                        // increment = digit.Length;
                        break;
                    }
                }

                pointer+=increment;
            }
            Debug.WriteLine($"{first} {last}");


            result += (DigitToInt(first) * 10) + DigitToInt(last);
        }

        return result.ToString();

        static int DigitToInt(string digit) => digit switch
        {
            "zero" or "0" => 0,
            "one" or "1" => 1,
            "two" or "2" => 2,
            "three" or "3" => 3,
            "four" or "4" => 4,
            "five" or "5" => 5,
            "six" or "6" => 6,
            "seven" or "7" => 7,
            "eight" or "8" => 8,
            "nine" or "9" => 9,
            _ => throw new Exception("Invalid digit")
        };
    }
}