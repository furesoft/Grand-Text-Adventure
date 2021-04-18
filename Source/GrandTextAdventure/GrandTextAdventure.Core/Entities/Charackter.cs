using System;

namespace GrandTextAdventure.Core.Entities
{
    public enum Gender
    {
        Male,
        Female,
        Divers,
    }
    public class Charackter : GameObject
    {
        public Charackter()
        {
            Type = GameObjectType.Charackter;
            ObserverProperty<int>("Health", HealthDeadHandler);
        }

        private void HealthDeadHandler(int health)
        {
            if (health >= 0)
            {
                OnDead?.Invoke("Health", health);
            }
        }

        public event GameObjectEventHandler OnDead;

        public Inventory Inventory { get; } = new();
        public bool IsDead => GetValue<int>("Health") <= 0;

        public Position Position
        {
            get { return GetValue<Position>(nameof(Position)); }
            set { SetOrAddValue(nameof(Position), value); }
        }

        public Gender Gender
        {
            get { return GetValue<Gender>(nameof(Gender)); }
            set { SetOrAddValue(nameof(Gender), value); }
        }
    }
}
