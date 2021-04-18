using System;

namespace GrandTextAdventure.Core.Entities
{
    public class NPC : Charackter
    {
        public override void Init() => OnDead += OnDeadHandler;

        private void OnDeadHandler(string property, object value)
        {
            Console.WriteLine("NPC is Dead");

            var inventory = GameEngine.Instance.GetState().Player.Inventory;

            Inventory.Transfer(inventory);
        }
    }
}
