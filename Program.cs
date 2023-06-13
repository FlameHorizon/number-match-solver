using System.Linq;
using Model;

internal class Program
{
  private const int Cleared = -1;
  private const int Blocked = -2;

  public static int maxRow = 0;
  public static int maxColumn = 0;

  private static void Main(string[] args)
  {
    var game = new Game(new int[,]
    {
      {9,5,8,7,9,7,Blocked,5,9},
      {1,6,9,6,8,4,Blocked,Blocked,8}
    });

    Cords start = new() { X = 0, Y = 0 };
    Cords found = game.SearchForPair(start);

    System.Console.WriteLine($"Found at {found.X}:{found.Y}");



    // int[,] values = new int[,]
    // {
    //   {9,5,8,7,9,7,Blocked,5,9},
    //   {8,6,9,6,8,4,Blocked,Blocked,8},
    //   {1,5,2,7,1,7,5,Blocked,Blocked},
    //   {Blocked,3,9,5,4,9,5,8,7},
    //   {9,7,5,9,8,6,9,6,8},
    //   {4,8,1,5,2,7,1,7,5},
    //   {3,9,5,4,Blocked,Blocked,Blocked,Blocked,Blocked}

    // };

    // List<((int row, int column), (int, int), int, int)> pairs = new();
    // bool FoundAtleastOnePair = true;
    // maxRow = values.GetLength(0);
    // maxColumn = values.GetLength(1);

    // while (FoundAtleastOnePair == true)
    // {
    //   FoundAtleastOnePair = false;
    //   for (int row = 0; row < maxRow; row++)
    //   {
    //     for (int column = 0; column < maxColumn; column++)
    //     {
    //       System.Console.WriteLine($"Start {row},{column}");

    //       (int row, int column) start = (row, column);
    //       if (values.GetLength(0) == 0)
    //       {
    //         break;
    //       }
    //       int value = values[start.row, start.column];
    //       if (value == Cleared || value == Blocked)
    //       {
    //         System.Console.WriteLine("Found cleared/Blocked cell. Moving to next");
    //         continue;
    //       }

    //       (int, int) clear;

    //       if (ScanRightLinear(values, start, out clear)
    //         || ScanLeftLinear(values, start, out clear)
    //         || ScanTopLinear(values, start, out clear)
    //         || ScanBottomLinear(values, start, out clear)
    //         || ScanTopLeftDiagonal(values, start, out clear)
    //         || ScanTopRightDiagonal(values, start, out clear)
    //         || ScanBottomLeftDiagonal(values, start, out clear)
    //         || ScanBottomRightDiagonal(values, start, out clear))
    //       {
    //         pairs.Add((start, clear, value, values[clear.Item1, clear.Item2]));
    //         values[clear.Item1, clear.Item2] = Cleared;
    //         values[start.row, start.column] = Cleared;
    //         FoundAtleastOnePair = true;

    //         // We have to remove entire row if all values in the row are Cleared or blocked
    //         if (RowContainsClearedOrBlockedCellsOnly(values))
    //         {
    //           values = RemoveClearedOrBlockedRow(values);
    //         }
    //         PrintState(values);
    //       }
    //       else
    //       {
    //         Console.WriteLine("Not a match.");
    //       }
    //     }
    //   }
    // }

    // PrintPairs(pairs);
    // PrintState(values);
  }

  // private static int[,] RemoveClearedOrBlockedRow(int[,] values)
  // {
  //   int[,] result = new int[values.GetLength(0) - 1, values.GetLength(1)];
  //   int lastResultRow = 0;

  //   bool allValuesClearedOrBlocked = true;
  //   for (int i = 0; i < values.GetLength(0); i++)
  //   {
  //     allValuesClearedOrBlocked = true;
  //     for (int j = 0; j < values.GetLength(1); j++)
  //     {
  //       int value = values[i, j];
  //       if (value != Cleared && value != Blocked)
  //       {
  //         allValuesClearedOrBlocked = false;
  //         break;
  //       }

  //     }

  //     if (allValuesClearedOrBlocked == false)
  //     {
  //       var row = GetRow(values, i);
  //       int k = 0;
  //       foreach (int v in row)
  //       {
  //         result[lastResultRow, k] = v;
  //         k++;
  //       }

  //       lastResultRow++;
  //     }
  //     else
  //     {
  //       maxRow--;
  //     }
  //   }
  //   ReplaceZeroValues(result);
  //   return result;
  // }

  // private static void ReplaceZeroValues(int[,] result)
  // {
  //   for (int i = 0; i < result.GetLength(0); i++)
  //   {
  //     for (int j = 0; j < result.GetLength(1); j++)
  //     {
  //       if (result[i, j] == 0)
  //       {
  //         result[i, j] = Blocked;
  //       }
  //     }
  //   }
  // }

  // private static int[] GetRow(int[,] matrix, int rowNumber)
  // {
  //   return Enumerable.Range(0, matrix.GetLength(1))
  //           .Select(x => matrix[rowNumber, x])
  //           .ToArray();
  // }


  // private static bool RowContainsClearedOrBlockedCellsOnly(int[,] values)
  // {
  //   bool allValuesClearedOrBlocked = true;
  //   for (int i = 0; i < values.GetLength(0); i++)
  //   {
  //     allValuesClearedOrBlocked = true;
  //     for (int j = 0; j < values.GetLength(1); j++)
  //     {
  //       int value = values[i, j];
  //       if (value != Cleared && value != Blocked)
  //       {
  //         allValuesClearedOrBlocked = false;
  //         break;
  //       }
  //     }

  //     if (allValuesClearedOrBlocked)
  //     {
  //       return true;
  //     }
  //   }

  //   return allValuesClearedOrBlocked;
  // }

  // private static void PrintPairs(List<((int row, int column), (int, int), int, int)> pairs)
  // {
  //   foreach (var x in pairs)
  //   {
  //     (int x, int y) vector = (x.Item1.column - x.Item2.Item2, x.Item1.row - x.Item2.Item1);

  //     string direction = string.Empty;
  //     if (vector.x <= -1 && vector.y == 0)
  //     {
  //       direction = "→";
  //     }
  //     else if (vector.x >= 1 && vector.y == 0)
  //     {
  //       direction = "←";
  //     }
  //     else if (vector.x == 0 && vector.y >= 1)
  //     {
  //       direction = "↑";
  //     }
  //     else if (vector.x == 0 && vector.y <= -1)
  //     {
  //       direction = "↓";
  //     }
  //     else if (vector.x <= -1 && vector.y <= -1)
  //     {
  //       direction = "↘";
  //     }
  //     else if (vector.x <= -1 && vector.y >= 1)
  //     {
  //       direction = "↗";
  //     }
  //     else if (vector.x >= 1 && vector.y >= 1)
  //     {
  //       direction = "↖";
  //     }
  //     else
  //     {
  //       direction = "↙";
  //     }

  //     Console.WriteLine(
  //       $"Start: [{x.Item1.row}, {x.Item1.column}, {x.Item3}], " +
  //       $"End: [{x.Item2.Item1}, {x.Item2.Item2}, {x.Item4}], {direction}");
  //   }

  // }

  // private static void PrintState(int[,] values)
  // {
  //   IEnumerable<int> rows = Enumerable.Range(0, values.GetLength(0));
  //   IEnumerable<int> columns = Enumerable.Range(0, values.GetLength(1));
  //   for (int i = 0; i <= rows.Count() - 1; i++)
  //   {
  //     Console.Write("{");
  //     for (int j = 0; j <= 8; j++)
  //     {
  //       string value;
  //       if (values[i, j] == Cleared)
  //       {
  //         value = ",Cleared";
  //       }
  //       else if (values[i, j] == Blocked)
  //       {
  //         value = ",Blocked";
  //       }
  //       else
  //       {
  //         value = "," + Convert.ToString(values[i, j]);
  //       }

  //       Console.Write(value);
  //     }
  //     Console.Write("},");
  //     Console.WriteLine();
  //   }
  // }

  // private static bool ScanBottomRightDiagonal(int[,] values,
  //                                           (int row, int column) start,
  //                                           out (int row, int column) found)
  // {
  //   System.Console.WriteLine("In ScanBottomRightDiagonal");
  //   (int row, int column) inspect = (start.row + 1, start.column + 1);
  //   int startValue = values[start.row, start.column];

  //   while (true)
  //   {
  //     // If we hit beyond last row, we return false
  //     if (inspect.row >= values.GetLength(0)
  //       || inspect.column >= values.GetLength(1))
  //     {
  //       found = (0, 0);
  //       return false;
  //     }

  //     int value = values[inspect.row, inspect.column];
  //     if (value == Cleared)
  //     {
  //       inspect.row++;
  //       inspect.column++;
  //       continue;
  //     }

  //     if (value == Blocked)
  //     {
  //       found = (0, 0);
  //       return false;
  //     }

  //     if (value >= 1 && value <= 10)
  //     {
  //       if (startValue + value == 10 || startValue == value)
  //       {
  //         Console.WriteLine($"Found match at {inspect.row} {inspect.column}");
  //         found = inspect;
  //         return true;
  //       }
  //       else
  //       {
  //         found = (0, 0);
  //         return false;
  //       }
  //     }
  //   }
  // }

  // private static bool ScanBottomLeftDiagonal(int[,] values, (int row, int column) start, out (int row, int column) found)
  // {
  //   System.Console.WriteLine("In ScanBottomLeftDiagonal");
  //   (int row, int column) inspect = (start.row + 1, start.column - 1);
  //   int startValue = values[start.row, start.column];

  //   while (true)
  //   {
  //     // If we hit beyond last row, we return false
  //     if (inspect.row >= values.GetLength(0)
  //       || inspect.column < 0
  //       || inspect.column >= values.GetLength(1))
  //     {
  //       found = (0, 0);
  //       return false;
  //     }

  //     int value = values[inspect.row, inspect.column];
  //     if (value == Cleared)
  //     {
  //       inspect.row++;
  //       inspect.column--;
  //       continue;
  //     }

  //     if (value == Blocked)
  //     {
  //       found = (0, 0);
  //       return false;
  //     }

  //     if (value >= 1 && value <= 10)
  //     {
  //       if (startValue + value == 10 || startValue == value)
  //       {
  //         Console.WriteLine($"Found match at {inspect.row} {inspect.column}");
  //         found = inspect;
  //         return true;
  //       }
  //       else
  //       {
  //         found = (0, 0);
  //         return false;
  //       }
  //     }
  //   }
  // }

  // private static bool ScanTopRightDiagonal(int[,] values, (int row, int column) start, out (int row, int column) found)
  // {
  //   System.Console.WriteLine("In ScanTopRightDiagonal");

  //   (int row, int column) inspect = (start.row - 1, start.column + 1);
  //   int startValue = values[start.row, start.column];

  //   while (true)
  //   {
  //     // If we hit beyond last row, we return false
  //     if (inspect.row < 0 || inspect.column >= values.GetLength(1))
  //     {
  //       found = (0, 0);
  //       return false;
  //     }

  //     int value = values[inspect.row, inspect.column];
  //     if (value == Cleared)
  //     {
  //       inspect.row--;
  //       inspect.column++;
  //       continue;
  //     }

  //     if (value == Blocked)
  //     {
  //       found = (0, 0);
  //       return false;
  //     }

  //     if (value >= 1 && value <= 10)
  //     {
  //       if (startValue + value == 10 || startValue == value)
  //       {
  //         Console.WriteLine($"Found match at {inspect.row} {inspect.column}");
  //         found = inspect;
  //         return true;
  //       }
  //       else
  //       {

  //         found = (0, 0);
  //         return false;
  //       }
  //     }
  //   }
  // }

  // private static bool ScanTopLeftDiagonal(int[,] values,
  //                                         (int row, int column) start,
  //                                         out (int row, int column) found)
  // {
  //   System.Console.WriteLine("In ScanTopLeftDiagonal");

  //   (int row, int column) inspect = (start.row - 1, start.column - 1);
  //   int startValue = values[start.row, start.column];

  //   while (true)
  //   {
  //     // If we hit beyond last row, we return false
  //     if (inspect.row < 0 || inspect.column < 0)
  //     {
  //       found = (0, 0);
  //       return false;
  //     }

  //     int value = values[inspect.row, inspect.column];
  //     if (value == Cleared)
  //     {
  //       inspect.row--;
  //       inspect.column--;
  //       continue;
  //     }

  //     if (value == Blocked)
  //     {
  //       found = (0, 0);
  //       return false;
  //     }

  //     if (value >= 1 && value <= 10)
  //     {
  //       if (startValue + value == 10 || startValue == value)
  //       {
  //         Console.WriteLine($"Found match at {inspect.row} {inspect.column}");
  //         found = inspect;
  //         return true;
  //       }
  //       else
  //       {
  //         found = (0, 0);
  //         return false;
  //       }
  //     }
  //   }
  // }

  // private static bool ScanBottomLinear(int[,] values, (int row, int column) start,
  //  out (int row, int column) found)
  // {
  //   System.Console.WriteLine("In ScanBottomLinear");

  //   (int row, int column) inspect = (start.row + 1, start.column);
  //   int startValue = values[start.row, start.column];

  //   while (true)
  //   {
  //     // If we hit beyond last row, we return false
  //     if (inspect.row >= values.GetLength(0))
  //     {
  //       found = (0, 0);
  //       return false;
  //     }

  //     int value = values[inspect.row, inspect.column];
  //     if (value == Cleared)
  //     {
  //       inspect.row++;
  //       continue;
  //     }

  //     if (value == Blocked)
  //     {
  //       found = (0, 0);
  //       return false;
  //     }

  //     if (value >= 1 && value <= 10)
  //     {
  //       if (startValue + value == 10 || startValue == value)
  //       {
  //         Console.WriteLine($"Found match at {inspect.row} {inspect.column}");
  //         found = inspect;
  //         return true;
  //       }
  //       else
  //       {
  //         found = (0, 0);
  //         return false;
  //       }
  //     }
  //   }
  // }

  // private static bool ScanTopLinear(int[,] values,
  //                                   (int row, int column) start,
  //                                   out (int row, int column) found)
  // {
  //   System.Console.WriteLine("In ScanTopLinear");

  //   (int row, int column) inspect = (start.row - 1, start.column);
  //   int startValue = values[start.row, start.column];

  //   while (true)
  //   {
  //     // If we hit beyond last row, we return false
  //     if (inspect.row < 0)
  //     {
  //       found = (0, 0);
  //       return false;
  //     }

  //     int value = values[inspect.row, inspect.column];
  //     if (value == Cleared)
  //     {
  //       inspect.row--;
  //       continue;
  //     }

  //     if (value == Blocked)
  //     {
  //       found = (0, 0);
  //       return false;
  //     }

  //     if (value >= 1 && value <= 10)
  //     {
  //       if (startValue + value == 10 || startValue == value)
  //       {
  //         Console.WriteLine($"Found match at {inspect.row} {inspect.column}");
  //         found = inspect;
  //         return true;
  //       }
  //       else
  //       {
  //         found = (0, 0);
  //         return false;
  //       }
  //     }
  //   }
  // }

  // private static bool ScanRightLinear(int[,] values,
  //                                     (int row, int column) start,
  //                                     out (int row, int column) found)
  // {
  //   System.Console.WriteLine("In ScanRightLinear");

  //   (int row, int column) inspect = (start.row, start.column + 1);

  //   int startValue = values[start.row, start.column];

  //   while (true)
  //   {
  //     // If we hit beyond last row, we return false
  //     if (inspect.row >= values.GetLength(0))
  //     {
  //       found = (0, 0);
  //       return false;
  //     }

  //     // If we hit last column, wrap it up to the beggining of next column
  //     if (inspect.column >= values.GetLength(1))
  //     {
  //       inspect.row = start.row + 1;
  //       inspect.column = 0;
  //       continue;
  //     }

  //     int value = values[inspect.row, inspect.column];
  //     if (value == Cleared)
  //     {
  //       inspect.column++;
  //       continue;
  //     }

  //     if (value == Blocked)
  //     {
  //       found = (0, 0);
  //       return false;
  //     }

  //     if (value >= 1 && value <= 10)
  //     {
  //       if (startValue + value == 10 || startValue == value)
  //       {
  //         Console.WriteLine($"Found match at {inspect.row} {inspect.column}");
  //         found = inspect;
  //         return true;
  //       }
  //       else
  //       {
  //         found = (0, 0);
  //         return false;
  //       }
  //     }
  //   }
  // }

  // private static bool ScanLeftLinear(int[,] values,
  //                                   (int row, int column) start,
  //                                   out (int row, int column) found)
  // {
  //   System.Console.WriteLine("In ScanLeftLinear");

  //   (int row, int column) inspect = (start.row, start.column - 1);
  //   int startValue = values[start.row, start.column];
  //   System.Console.WriteLine($"Scanning {inspect.row}, {inspect.column}");

  //   const int rowLimit = 0;
  //   const int columnLimit = 0;

  //   while (true)
  //   {
  //     // If we hit before first row, we return false
  //     if (inspect.row < rowLimit)
  //     {
  //       found = (0, 0);
  //       return false;
  //     }

  //     // If we hit first column, wrap it up to the beggining of next column
  //     if (inspect.column < columnLimit)
  //     {
  //       inspect.row = inspect.row - 1;
  //       inspect.column = values.GetLength(1) - 1;
  //       continue;
  //     }

  //     int value = values[inspect.row, inspect.column];
  //     if (value == Cleared)
  //     {
  //       inspect.column--;
  //       continue;
  //     }

  //     if (value == Blocked)
  //     {
  //       found = (0, 0);
  //       return false;
  //     }

  //     if (IsReadyForComparison(value))
  //     {
  //       if (IsMatch(startValue, value))
  //       {
  //         Console.WriteLine($"Found match at {inspect.row} {inspect.column}");
  //         found = inspect;
  //         return true;
  //       }
  //       else
  //       {
  //         found = (0, 0);
  //         return false;
  //       }
  //     }
  //   }
  // }

  // private static bool IsReadyForComparison(int value)
  // {
  //   return value >= 1 && value <= 10;
  // }

  // private static bool IsMatch(int startValue, int value)
  // {
  //   return startValue + value == 10 || startValue == value;
  // }
}