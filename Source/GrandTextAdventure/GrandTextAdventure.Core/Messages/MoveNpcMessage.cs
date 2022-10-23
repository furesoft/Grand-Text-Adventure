using GrandTextAdventure.Core.Game;

namespace GrandTextAdventure.Core.Messages;

public class MoveNpcMessage : GameMessage
{
    public Direction Direction { get; set; }
    public int Speed { get; set; }
    public Position OldPosition { get; set; }
}
