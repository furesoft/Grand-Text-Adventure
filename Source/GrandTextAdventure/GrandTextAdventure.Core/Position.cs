using GrandTextAdventure.Core.Game;

namespace GrandTextAdventure.Core
{
    public record Position(int X, int Y)
    {
        public static Position ApplyDirection(Position pos, Direction dir)
        {
            //ToDo: Add Bound Check for Navigating
            return dir switch
            {
                Direction.North => new Position(pos.X, pos.Y - 1),
                Direction.West => new Position(pos.X - 1, pos.Y),
                Direction.East => new Position(pos.X + 1, pos.Y),
                Direction.South => new Position(pos.X, pos.Y + 1),
                _ => pos,
            };
        }
    }
}
