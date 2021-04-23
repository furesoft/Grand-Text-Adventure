namespace GrandTextAdventure.Core
{
    public interface IEnterable
    {
        void OnEnter(Position pos);
        void OnExit(Position pos);
    }
}
