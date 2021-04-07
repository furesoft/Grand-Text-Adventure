namespace GrandTextAdventure.Core.Entities
{
    public class Charackter : GameObject
    {
        public Charackter()
        {
            Type = GameObjectType.Charackter;
        }

        public Inventory Inventory { get; } = new();
        public bool IsDead => GetValue<int>("Health") <= 0;

        public Position Position
        {
            get { return GetValue<Position>(nameof(Position)); }
            set { SetOrAddValue(nameof(Position), value); }
        }
    }
}
