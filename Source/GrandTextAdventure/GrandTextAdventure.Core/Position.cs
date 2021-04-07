using GrandTextAdventure.Core.Game;

namespace GrandTextAdventure.Core
{
    public record Position(int X, int Y)
    {
        public static Position ApplyDirection(Position pos, Direction dir)
        {
            //ToDo: Add Bound Check for Navigating
            switch (dir)
            {
                case Direction.North:
                    return new Position(pos.X, pos.Y - 1);

                case Direction.West:
                    return new Position(pos.X - 1, pos.Y);

                case Direction.East:
                    return new Position(pos.X + 1, pos.Y);

                case Direction.South:
                    return new Position(pos.X, pos.Y + 1);
            }

            return pos;
        }
    }
}
