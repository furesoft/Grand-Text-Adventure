namespace GrandTextAdventure.Core.Game;

public enum Direction
{

    North = 0x02,
    West = 0x01,
    East = 0x04,
    South = 0x08,

    NorthEast = 0x10,
    NorthWest = 0x20,

    SouthEast = 0x40,

    SouthWest = 0x80,
    Around = 0x100,
}
