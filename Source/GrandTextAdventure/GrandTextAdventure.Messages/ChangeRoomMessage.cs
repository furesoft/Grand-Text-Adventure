using GrandTextAdventure.Core.Game;

namespace GrandTextAdventure.Messages
{
    public class ChangeRoomMessage : GameMessage
    {
        public Direction Direction { get; set; }
    }
}
