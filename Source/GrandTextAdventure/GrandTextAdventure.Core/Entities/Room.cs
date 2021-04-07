using System;

namespace GrandTextAdventure.Core.Entities
{
    public class Room : GameObject
    {
        public RoomExits Exits { get; set; } = new();

        public int Heigth
        {
            get { return GetValue<int>(nameof(Heigth)); }
            set { SetOrAddValue(nameof(Heigth), value); }
        }

        public int Width
        {
            get { return GetValue<int>(nameof(Width)); }
            set { SetOrAddValue(nameof(Width), value); }
        }

        public bool IsInBounds(Position newPos)
        {
            return newPos.X >= 0 && newPos.X < Width
                 && newPos.Y >= 0 && newPos.Y < Heigth;
        }
    }
}
