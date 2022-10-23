namespace GrandTextAdventure.Core.Entities;

[EntityInstance]
public class Building : Room, IEnterable
{
    public Building()
    {
        Type = GameObjectType.Building;
    }

    public bool IsEnterable()
    {
        return true;
    }
    Position _oldPosition;
    public void OnEnter(Position pos)
    {
        var state = GameEngine.Instance.GetState();
        state.Player.Position = new Position(0, 0); // ToDo: set start position by property
        _oldPosition = pos;
        GameEngine.Instance.SetState(state);
    }

    public void OnExit(Position pos)
    {
        var state = GameEngine.Instance.GetState();
        state.Player.Position = _oldPosition;

        GameEngine.Instance.SetState(state);
    }
}
