using System;
using GrandTextAdventure.Core.Game;

namespace GrandTextAdventure.Core.Messages;

public class ChangeStateMessage : GameMessage
{
    public GameState State { get; set; }
}
