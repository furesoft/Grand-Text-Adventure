using System;

namespace GrandTextAdventure.Core.Entities;

[EntityInstance]
public class Vehicle : GameObject, IEnterable
{
    public Vehicle()
    {
        Type = GameObjectType.Vehicle;
    }

    public bool IsLocked
    {
        get { return GetValue<bool>(nameof(IsLocked)); }
        set { SetOrAddValue(nameof(IsLocked), value); }
    }

    public bool IsDriving
    {
        get { return GetValue<bool>(nameof(IsDriving)); }
        set { SetOrAddValue(nameof(IsDriving), value); }
    }

    public byte Speed
    {
        get { return GetValue<byte>(nameof(Speed)); }
        set { SetOrAddValue(nameof(Speed), value); }
    }

    public bool IsEnterable() => true;

    public void OnEnter(Position pos)
    {
        var state = GameEngine.Instance.GetState();

        state.Player.Position = pos;
        state.Player.Vehicle = this;
        state.ObjectLayer[pos.X, pos.Y] = null; //Delete Vehicle from Layer to avoud problems

        if (IsLocked)
        {
            Console.WriteLine(Name + " is locked. You are cracking it...");
            if (GameEngine.Instance.Wait(2000))
            {
                Console.WriteLine("The door is cracked");
            }
        }

        Console.WriteLine("You now drive with " + Name);

        GameEngine.Instance.SetState(state);
    }

    public void OnExit(Position pos)
    {
        var state = GameEngine.Instance.GetState();

        state.Player.Position = Position.ApplyDirection(state.Player.Position, Game.Direction.West);

        state.ObjectLayer[pos.X, pos.Y] = state.Player.Vehicle; //drop vehicle to object layer

        state.Player.Vehicle = null;

        GameEngine.Instance.SetState(state);
    }
}
