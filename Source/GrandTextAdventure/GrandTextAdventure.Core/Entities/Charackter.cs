namespace GrandTextAdventure.Core.Entities
{
    public class Charackter : GameObject
    {
        public Inventory Inventory = new();

        public Charackter()
        {
            Type = GameObjectType.Charackter;
            Name = "Michael";
        }

        public bool IsDead => GetValue<int>("Health") <= 0;
        public override string Name { get => GetValue<string>(nameof(Name)); set => SetOrAddValue(nameof(Name), value); }
    }
}
