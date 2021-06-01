using System;
using System.Data.SqlTypes;
using GrandTextAdventure.Core.Entities;

namespace GrandTextAdventure.Core
{
    public static class Generator
    {
        private static Random s_random = new();

        private static readonly string[] _vehicleNames = new[] { "Bycicle", "MotorGP", "Lamborghini", "R8", "Truck", "PickUp" };
        private static readonly string[] _roomNames = new[] { "Downey Street", "Something Street", "Big Apple Street", "Manhattan Street" };

        public static GameObject NextVehicle()
        {
            var r = new Vehicle
            {
                Name = _vehicleNames[s_random.Next(0, _vehicleNames.Length)],
                Speed = (byte)s_random.Next(1, 3)
            };

            return r;
        }

        public static GameObject NextNPC()
        {
            var r = new NPC
            {
                Gender = s_random.Next(0, 1) == 0 ? Gender.Male : Gender.Female
            };

            return r;
        }

        public static Room GenerateRoom()
        {
            var r = new Room();
            r.Name = _roomNames[s_random.Next(0, _roomNames.Length)];
            r.Width = s_random.Next(25, 50);
            r.Heigth = s_random.Next(25, 50);

            //ToDo: Generate NPCS and Vehicles and position it on room

            return r;

        }
    }
}