using GrandTextAdventure.Core.Game;

namespace GrandTextAdventure.Core
{
    public record Position(int X, int Y)
    {
        public static Position ApplyDirection(Position pos, Direction dir, byte speed = 1)
        {
            //ToDo: Add Bound Check for Navigating
            return dir switch
            {
                Direction.North => new Position(pos.X, pos.Y - speed),
                Direction.West => new Position(pos.X - speed, pos.Y),
                Direction.East => new Position(pos.X + speed, pos.Y),
                Direction.South => new Position(pos.X, pos.Y + speed),

                Direction.NorthEast => new Position(pos.X + speed, pos.Y - speed),
                Direction.NorthWest => new Position(pos.X - speed, pos.Y - speed),
                Direction.SouthWest => new Position(pos.X - speed, pos.Y + speed),
                Direction.SouthEast => new Position(pos.X + speed, pos.Y + speed),
                _ => pos,
            };
        }
    }
}
