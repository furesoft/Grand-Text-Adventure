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
            ObserverProperty<uint>("Health", HealthDeadHandler);
        }

        private void HealthDeadHandler(uint health)
        {
            if (health >= 0)
            {
                OnDead?.Invoke(health);
            }
        }

        public event Action<uint> OnDead;

        public Inventory Inventory { get; } = new();
        public Vehicle Vehicle { get; set; }
        public bool IsDead => GetValue<uint>("Health") <= 0;

        public Position Position
        {
            get { return GetValue<Position>(nameof(Position)); }
            set { SetOrAddValue(nameof(Position), value); }
        }

        public Money Money
        {
            get { return GetValue<Money>(nameof(Money)); }
            set { SetOrAddValue(nameof(Money), value); }
        }

        public Gender Gender
        {
            get { return GetValue<Gender>(nameof(Gender)); }
            set { SetOrAddValue(nameof(Gender), value); }
        }

        public override void Deinit()
        {
            base.Deinit();

            OnDead = null;
        }
    }
}
