namespace Model
{
    public class Game
    {
        private const int Cleared = -1;
        private const int Blocked = -2;

        private readonly int[,] _board;
        private readonly int _lowerRowLimit = 0;
        private readonly int _upperRowLimit;
        private readonly int _lowerColumnLimit = 0;
        private readonly int _upperColumnLimit;


        public Game(int[,] board)
        {
            _board = board;
            _upperRowLimit = _board.GetLength(0);
            _upperColumnLimit = _board.GetLength(1);
        }

        /// <summary>
        /// Seraches for either the same value as in start or
        /// for value where sum of two is ten.
        /// </summary>
        /// <param name="start">Search start location.</param>
        /// <returns>Location where pair was found. If pair wasn't found
        /// return Cords.Empty.</returns>
        public Cords SearchForPair(Cords start)
        {
            if (SerachLinearRight(start, out Cords found)
            || SearchLinearLeft(start, out found)
            || SerachLinearTop(start, out found)
            || SerachLinearBottom(start, out found))
            {
                return found;
            }
            else
            {
                return Cords.Empty;
            }
        }

        private bool SerachLinearBottom(Cords start, out Cords found)
        {
            return SearchLinear(start, out found, Direction.Bottom);
        }

        private int GetValue(Cords cords)
        {
            return _board[cords.X, cords.Y];
        }

        private bool SerachLinearTop(Cords start, out Cords found)
        {
            return SearchLinear(start, out found, Direction.Top);
        }

        private bool SearchLinearLeft(Cords start, out Cords found)
        {
            return SearchLinear(start, out found, Direction.Left);
        }

        private bool SerachLinearRight(Cords start, out Cords found)
        {
            return SearchLinear(start, out found, Direction.Right);
        }

        private bool SearchLinear(Cords start, out Cords found, Direction direction)
        {
            Cords inspecting;
            Cords offset = direction switch
            {
                Direction.Right => new Cords { X = 0, Y = 1 },
                Direction.Left => new Cords { X = 0, Y = -1 },
                Direction.Top => new Cords { X = -1, Y = 0 },
                _ => new Cords { X = 1, Y = 0 },
            };
            inspecting = start.AddOffset(offset);

            while (true)
            {
                if (inspecting.X > _upperRowLimit || inspecting.X < _lowerRowLimit)
                {
                    found = Cords.Empty;
                    return false;
                }

                if (inspecting.Y > _upperColumnLimit || inspecting.Y < _lowerColumnLimit)
                {
                    if (direction == Direction.Right)
                    {
                        inspecting = new Cords() { X = inspecting.X + 1, Y = 0 };
                    }
                    else
                    {
                        inspecting = new Cords() { X = inspecting.X - 1, Y = _upperColumnLimit - 1 };
                    }
                    continue;
                }

                int inspectValue = _board[inspecting.X, inspecting.Y];
                if (inspectValue == Cleared)
                {
                    inspecting = inspecting.AddOffset(offset);
                    continue;
                }

                if (inspectValue == Blocked)
                {
                    found = Cords.Empty;
                    return false;
                }

                if (Enumerable.Range(1, 10).Contains(inspectValue))
                {
                    return CompareValues(start, inspecting, out found);
                }
            }
        }

        public enum Direction
        {
            Left,
            Right,
            Top,
            Bottom
        }

        private bool CompareValues(Cords start, Cords inspecting, out Cords found)
        {
            int startValue = GetValue(start);
            int inspectValue = GetValue(inspecting);
            if (startValue + inspectValue == 10 || startValue == inspectValue)
            {
                found = inspecting;
                return true;
            }
            else
            {
                found = Cords.Empty;
                return false;
            }
        }
    }

    public class Cords
    {
        public static Cords Empty = new() { X = int.MinValue, Y = int.MinValue };

        /// <summary>
        /// Row.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Column
        /// </summary>
        public int Y { get; set; }

        public Cords AddOffset(int X, int Y)
        {
            return new Cords() { X = this.X + X, Y = this.Y + Y };
        }

        public Cords AddOffset(Cords cords)
        {
            return AddOffset(cords.X, cords.Y);
        }
    }
}

