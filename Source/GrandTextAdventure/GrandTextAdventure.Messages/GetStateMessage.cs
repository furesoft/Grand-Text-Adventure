using Actress;

namespace GrandTextAdventure.Messages
{
    public class GetStateMessage : GameMessage
    {
        public GetStateMessage(IReplyChannel<GameMessage> channel, string path)
        {
            Channel = channel;
            Path = path;
        }

        public IReplyChannel<GameMessage> Channel { get; }
        public string Path { get; set; }
        public object Value { get; set; }
    }
}
