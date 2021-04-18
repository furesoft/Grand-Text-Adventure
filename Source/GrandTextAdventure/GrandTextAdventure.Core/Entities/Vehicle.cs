namespace GrandTextAdventure.Core.Entities
{
    [EntityInstance]
    public class Vehicle : GameObject
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

        public bool Speed
        {
            get { return GetValue<bool>(nameof(Speed)); }
            set { SetOrAddValue(nameof(Speed), value); }
        }
    }
}
