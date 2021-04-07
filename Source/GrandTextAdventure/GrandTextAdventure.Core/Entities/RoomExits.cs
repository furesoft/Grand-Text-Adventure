namespace GrandTextAdventure.Core.Entities
{
    public struct RoomExits
    {
        public RoomID East { get; set; }
        public RoomID North { get; set; }
        public RoomID South { get; set; }
        public RoomID West { get; set; }
    }
}
