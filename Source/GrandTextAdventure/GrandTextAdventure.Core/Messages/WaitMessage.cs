using Actress;

namespace GrandTextAdventure.Core.Messages;

public class WaitMessage : GameMessage
{
    public bool IsDone { get; set; }
    public WaitMessage(IReplyChannel<GameMessage> channel, int waitTime)
    {
        Channel = channel;
        WaitTime = waitTime;
    }

    public IReplyChannel<GameMessage> Channel { get; }
    public int WaitTime { get; }
}
