using System;

namespace GrandTextAdventure.Core.Entities
{
    public struct RoomID
    {
        public RoomID(string iD)
        {
            ID = iD;
        }

        public string ID { get; set; }

        public Room GetRoom()
        {
            throw new NotImplementedException();
        }
    }
}
