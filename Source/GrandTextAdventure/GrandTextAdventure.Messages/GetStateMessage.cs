using Actress;
using GrandTextAdventure.Core.Game;

namespace GrandTextAdventure.Messages
{
    public class GetStateMessage : GameMessage
    {
        public GetStateMessage(IReplyChannel<GameMessage> channel)
        {
            Channel = channel;
        }

        public IReplyChannel<GameMessage> Channel { get; }
        public GameState State { get; set; }
    }
}
