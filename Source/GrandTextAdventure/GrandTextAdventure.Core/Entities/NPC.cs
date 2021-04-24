using System;
using GrandTextAdventure.Core.Game;

namespace GrandTextAdventure.Core.Entities
{
    public class NPC : Charackter
    {
        public override void Init() => OnDead += OnDeadHandler;

        public int AgressionLevel { get; set; }

        private void OnDeadHandler(uint value)
        {
            Console.WriteLine((Gender == Gender.Male ? "He" : "She") + " is Dead");

            var gameState = GameEngine.Instance.GetState();
            var player = gameState.Player;
            var inventory = player.Inventory;

            Inventory.Transfer(inventory);
            player.Money += Money;

            gameState.ObjectLayer[Position.X, Position.Y] = null;

            GameEngine.Instance.SetState(gameState);
        }
    }
}
