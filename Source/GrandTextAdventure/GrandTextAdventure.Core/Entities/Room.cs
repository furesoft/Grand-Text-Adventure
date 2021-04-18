using System.Collections.Generic;

namespace GrandTextAdventure.Core.Entities
{
    public class Room : GameObject
    {
        public GameObject[] Entries { get; set; }
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

        public Dictionary<Position, GameObject> PlacingItems { get; set; }

        public override void Init()
        {
            foreach (var entry in Entries)
            {
                entry.Init();
            }

            var state = GameEngine.Instance.GetState();

            foreach (var item in PlacingItems)
            {
                //ToDo: implement init ObjectLayer from room.placingitems
            }
        }

        public override void Deinit()
        {
            foreach (var entry in Entries)
            {
                entry.Deinit();
            }

            Entries = null;
            Exits = null;
        }
    }
}
