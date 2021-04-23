using System;

namespace GrandTextAdventure.Core.Entities
{
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

        public void OnEnter(Position pos)
        {
            var state = GameEngine.Instance.GetState();

            state.Player.Position = pos;
            state.Player.Vehicle = this;

            Console.WriteLine("You now drive with " + Name);

            GameEngine.Instance.SetState(state);
        }

        public void OnExit(Position pos)
        {
            var state = GameEngine.Instance.GetState();

            state.Player.Vehicle = null;
            state.Player.Position = Position.ApplyDirection(state.Player.Position, Game.Direction.Left);

            GameEngine.Instance.SetState(state);
        }
    }
}
