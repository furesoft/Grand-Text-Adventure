namespace GrandTextAdventure.Core.Game
{
    public enum Direction
    {
        Left = 0x01,
        Right = 0x04,
        Behind = 0x08,
        Before = 0x02,

        North = 0x02,
        West = 0x01,
        East = 0x04,
        South = 0x08,

        NorthEast = 0x10,
        NorthWest = 0x20,

        SouthEast = 0x40,

        SouthWest = 0x80,
    }
}
