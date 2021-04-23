using System;

namespace GrandTextAdventure.Core.Entities
{
    public class NPC : Charackter
    {
        public override void Init() => OnDead += OnDeadHandler;

        public int AgressionLevel { get; set; }

        private void OnDeadHandler(string property, object value)
        {
            Console.WriteLine((Gender == Gender.Male ? "He" : "She") + " is Dead");

            var inventory = GameEngine.Instance.GetState().Player.Inventory;

            Inventory.Transfer(inventory);
        }
    }
}
