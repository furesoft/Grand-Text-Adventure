using GrandTextAdventure.Core.Game;

namespace GrandTextAdventure.Core.Messages;

public class ChangeRoomMessage : GameMessage
{
    public Direction Direction { get; set; }
}
