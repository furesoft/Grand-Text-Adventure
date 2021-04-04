namespace GrandTextAdventure.Core.CommandProcessing
{
    public interface ICommand
    {
        void Invoke(string[] args);
    }
}
