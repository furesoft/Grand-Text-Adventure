using System;

namespace GrandTextAdventure.Core.Entities
{
    public class PlayerCharackter : Charackter
    {
        public PlayerCharackter()
        {
            OnDead += OnDead_Handler;
        }

        private void OnDead_Handler(uint health)
        {
            Console.WriteLine("You are dead. You will spawn near a hospital");

            

            //ToDo spawn near hospital after death
        }
    }
}
