namespace GrandTextAdventure.Core.Entities
{
    public class Charackter : GameObject
    {
        public Charackter()
        {
            Type = GameObjectType.Charackter;
            Name = "Michael";
        }

        public override string Name { get => GetValue<string>(nameof(Name)); set => SetOrAddValue(nameof(Name), value); }
    }
}
