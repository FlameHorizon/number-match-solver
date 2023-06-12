internal class Program
{
  private const int Cleared = -1;
  private const int OutOfBounds = 0;

  private static void Main(string[] args)
  {
    int[,] values = new int[,]
    {
      {7,2,7,1,4,2,6,8,9},
      {4,6,5,2,5,7,1,4,5},
      {1,7,3,3,6,8,5,2,7},
      {2,5,2,9,3,Cleared,Cleared,Cleared,Cleared},
    };

    foreach (int row in Enumerable.Range(0, values.GetLength(0)))
    {
      foreach (int column in Enumerable.Range(0, values.GetLength(1)))
      {
        (int, int) start = (row, column);
        (int, int) clear;

        if (ScanRightLinear(values, start, out clear)
          || ScanLeftLinear(values, start, out clear)
          || ScanTopLinear(values, start, out clear)
          || ScanBottomLinear(values, start, out clear)
          || ScanTopLeftDiagonal(values, start, out clear)
          || ScanTopRightDiagonal(values, start, out clear)
          || ScanBottomLeftDiagonal(values, start, out clear)
          || ScanBottomRightDiagonal(values, start, out clear))
        {
          Console.WriteLine("It is a match or sum is 10.");
          values[clear.Item1, clear.Item2] = Cleared;
        }
        else
        {
          Console.WriteLine("Not a match.");
        }
      }
    }
  }

  private static bool ScanBottomRightDiagonal(int[,] values, (int row, int column) start)
  {

    (int row, int column) inspect = (start.row + 1, start.column + 1);
    int startValue = values[start.row, start.column];

    while (true)
    {
      // If we hit beyond last row, we return false
      if (inspect.row >= values.GetLength(0)
        || inspect.column >= values.GetLength(1))
      {
        return false;
      }

      int value = values[inspect.row, inspect.column];
      if (value == Cleared)
      {
        inspect.row++;
        inspect.column++;
        continue;
      }

      if (value >= 1 && value <= 10)
      {
        if (startValue + value == 10 || startValue == value)
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

  private static bool ScanBottomLeftDiagonal(int[,] values, (int row, int column) start)
  {

    (int row, int column) inspect = (start.row + 1, start.column - 1);
    int startValue = values[start.row, start.column];

    while (true)
    {
      // If we hit beyond last row, we return false
      if (inspect.row >= values.GetLength(0)
        || inspect.column < 0)
      {
        return false;
      }

      int value = values[inspect.row, inspect.column];
      if (value == Cleared)
      {
        inspect.row++;
        inspect.column++;
        continue;
      }

      if (value >= 1 && value <= 10)
      {
        if (startValue + value == 10 || startValue == value)
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

  private static bool ScanTopRightDiagonal(int[,] values, (int row, int column) start)
  {
    (int row, int column) inspect = (start.row - 1, start.column + 1);
    int startValue = values[start.row, start.column];

    while (true)
    {
      // If we hit beyond last row, we return false
      if (inspect.row < 0 || inspect.column >= values.GetLength(1))
      {
        return false;
      }

      int value = values[inspect.row, inspect.column];
      if (value == Cleared)
      {
        inspect.row--;
        inspect.column++;
        continue;
      }

      if (value >= 1 && value <= 10)
      {
        if (startValue + value == 10 || startValue == value)
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

  private static bool ScanTopLeftDiagonal(int[,] values, (int row, int column) start,
  out (int row, int column) found)
  {
    (int row, int column) inspect = (start.row - 1, start.column - 1);
    int startValue = values[start.row, start.column];

    while (true)
    {
      // If we hit beyond last row, we return false
      if (inspect.row < 0 || inspect.column < 0)
      {
        found = (0, 0);
        return false;
      }

      int value = values[inspect.row, inspect.column];
      if (value == Cleared)
      {
        inspect.row--;
        inspect.column--;
        continue;
      }

      if (value >= 1 && value <= 10)
      {
        if (startValue + value == 10 || startValue == value)
        {
          Console.WriteLine($"Found match at {inspect.row} {inspect.column}");
          found = inspect;
          return true;
        }
        else
        {
          found = (0, 0);
          return false;
        }
      }
    }
  }

  private static bool ScanBottomLinear(int[,] values, (int row, int column) start,
   out (int row, int column) found)
  {
    (int row, int column) inspect = (start.row + 1, start.column);
    int startValue = values[start.row, start.column];

    while (true)
    {
      // If we hit beyond last row, we return false
      if (inspect.row >= values.GetLength(0))
      {
        found = (0, 0);
        return false;
      }

      int value = values[inspect.row, inspect.column];
      if (value == Cleared)
      {
        inspect.row++;
        continue;
      }

      if (value >= 1 && value <= 10)
      {
        if (startValue + value == 10 || startValue == value)
        {
          Console.WriteLine($"Found match at {inspect.row} {inspect.column}");
          found = inspect;
          return true;
        }
        else
        {
          found = (0, 0);
          return false;
        }
      }
    }
  }

  private static bool ScanTopLinear(int[,] values,
                                    (int row, int column) start,
                                    (int row, int column) found)
  {
    (int row, int column) inspect = (start.row - 1, start.column);
    int startValue = values[start.row, start.column];

    while (true)
    {
      // If we hit beyond last row, we return false
      if (inspect.row < 0)
      {
        found = (0, 0);
        return false;
      }

      int value = values[inspect.row, inspect.column];
      if (value == Cleared)
      {
        inspect.row--;
        continue;
      }

      if (value >= 1 && value <= 10)
      {
        if (startValue + value == 10 || startValue == value)
        {
          Console.WriteLine($"Found match at {inspect.row} {inspect.column}");
          found = inspect;
          return true;
        }
        else
        {
          found = (0, 0);
          return false;
        }
      }
    }
  }

  private static bool ScanRightLinear(int[,] values,
                                      (int row, int column) start,
                                      out (int row, int column) found)
  {
    (int row, int column) inspect = (start.row, start.column + 1);

    int startValue = values[start.row, start.column];

    while (true)
    {
      // If we hit beyond last row, we return false
      if (inspect.row >= values.GetLength(0))
      {
        found = (0, 0);
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
        if (startValue + value == 10 || startValue == value)
        {
          Console.WriteLine($"Found match at {inspect.row} {inspect.column}");
          found = inspect;
          return true;
        }
        else
        {
          found = (0, 0);
          return false;
        }
      }
    }
  }

  private static bool ScanLeftLinear(int[,] values,
                                    (int row, int column) start,
                                    out (int row, int column) found)
  {
    (int row, int column) inspect = (start.row, start.column - 1);
    int startValue = values[start.row, start.column];

    const int rowLimit = 0;
    const int columnLimit = 0;

    while (true)
    {
      // If we hit before first row, we return false
      if (inspect.row < rowLimit)
      {
        found = (0, 0);
        return false;
      }

      // If we hit first column, wrap it up to the beggining of next column
      if (inspect.column < columnLimit)
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

      if (IsReadyForComparison(value))
      {
        if (IsMatch(startValue, value))
        {
          Console.WriteLine($"Found match at {inspect.row} {inspect.column}");
          found = inspect;
          return true;
        }
        else
        {
          found = (0, 0);
          return false;
        }
      }
    }
  }

  private static bool IsReadyForComparison(int value)
  {
    return value >= 1 && value <= 10;
  }

  private static bool IsMatch(int startValue, int value)
  {
    return startValue + value == 10 || startValue == value;
  }
}