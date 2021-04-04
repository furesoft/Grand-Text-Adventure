namespace GrandTextAdventure.Messages
{
    public class ChangeStateMessage : GameMessage
    {
        public string Path { get; set; }
        public object Value { get; set; }
    }
}
