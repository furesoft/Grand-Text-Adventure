namespace GrandTextAdventure.Core.TextProcessing.Interfaces
{
    public interface ICommandHandler
    {
        void Invoke(Command cmd);
    }
}
