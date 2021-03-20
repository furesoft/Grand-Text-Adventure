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
            var lambo = new Vehicle
            {
                Name = "Lamborghini"
            };

            lambo.SetOrAddValue("speed", 42);
            lambo.SetOrAddValue("protection", 100);

            var mg11 = new Weapon();
            mg11.Name = "mg11";

            mg11.SetOrAddValue("speed", 12);
            mg11.SetOrAddValue("protection", 0);

            var strm = File.OpenWrite("test.ced");
            var writer = new GameObjectWriter(strm);

            writer.WriteObject(lambo);
            writer.WriteObject(mg11);

            writer.Close();
        }
    }
}