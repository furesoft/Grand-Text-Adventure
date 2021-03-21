using GrandTextAdventure.Core;
using GrandTextAdventure.Core.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace UnitTests
{
    [TestClass]
    public class ObjectWriterTests
    {
        [TestMethod]
        public void Write_Should_Pass()
        {
            dynamic lambo = new Vehicle
            {
                Name = "Lamborghini"
            };

            lambo.speed = 42;
            lambo.protection = 100;

            dynamic mg11 = new Weapon
            {
                Name = "mg11"
            };

            mg11.wrap = 3.14;
            mg11.speed = 12;
            mg11.protection = 0;

            mg11.blub = "hello world";

            var strm = File.Open("test.ced", FileMode.OpenOrCreate);
            var writer = new GameObjectWriter(strm);

            writer.WriteObject(mg11);
            writer.WriteObject(lambo);

            writer.Close();
        }
    }
}