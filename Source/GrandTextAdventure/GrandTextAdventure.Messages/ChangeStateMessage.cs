using System;
using GrandTextAdventure.Core.Game;

namespace GrandTextAdventure.Messages
{
    public class ChangeStateMessage : GameMessage
    {
        public GameState State { get; set; }
    }
}
