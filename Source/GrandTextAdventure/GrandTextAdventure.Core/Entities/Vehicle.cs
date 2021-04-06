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
    }
}
