internal class Program
{
    private const int Cleared = -1;
    private const int OutOfBounds = 0;

    private static void Main(string[] args)
    {
        int[,] values = new int[,]
        {
            {2, 3 , Cleared, 5, 6, 7, 7, 3, 1},
            {9, 3 , Cleared, 5, 6, 7, 7, 3, 1}
        };

        (int, int) start = (1, 0);
        if (ScanRightLinear(values, start))
        {
            Console.WriteLine("It is a match or sum is 10.");
        }
        else if (ScanLeftLinearForSumOfTen(values, start))
        {
            Console.WriteLine("Sum is equal to 10");
        }
        else
        {
            Console.WriteLine("Not a match.");
        }
    }

    private static bool ScanRightLinear(int[,] values, (int row, int column) start)
    {
        (int row, int column) inspect = (start.row, start.column + 1);

        int startValue = values[start.row, start.column];

        while (true)
        {
            // If we hit beyond last row, we return false
            if (inspect.row >= values.GetLength(0))
            {
                return false;
            }

            // If we hit last column, wrap it up to the beggining of next column
            if (inspect.column >= values.GetLength(1))
            {
                inspect.row = start.row + 1;
                inspect.column = 0;
                continue;
            }

            int value = values[inspect.row, inspect.column];
            if (value == Cleared)
            {
                inspect.column++;
                continue;
            }

            if (value >= 1 && value <= 10)
            {
                Console.WriteLine($"Found not cleared value {value}");
                if (startValue + value == 10 && startValue == value)
                {
                    Console.WriteLine($"Found match at {inspect.row} {inspect.column}");
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }

    private static bool ScanLeftLinear(int[,] values, (int row, int column) start)
    {
        (int row, int column) inspect = (start.row, start.column - 1);

        int startValue = values[start.row, start.column];

        while (true)
        {
            // If we hit before first row, we return false
            if (inspect.row < 0)
            {
                return false;
            }

            // If we hit first column, wrap it up to the beggining of next column
            if (inspect.column < 0)
            {
                inspect.row = start.row - 1;
                inspect.column = values.GetLength(1) - 1;
                continue;
            }

            int value = values[inspect.row, inspect.column];
            if (value == Cleared)
            {
                inspect.column--;
                continue;
            }

            if (startValue + value == 10 && startValue == value)
            {
                Console.WriteLine($"Found not cleared value {value}");
                if (startValue + value == 10)
                {
                    Console.WriteLine($"Found match at {inspect.row} {inspect.column}");
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}