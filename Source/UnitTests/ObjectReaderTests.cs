using GrandTextAdventure.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

namespace UnitTests
{
    [TestClass]
    public class ObjectReaderTests
    {
        [TestMethod]
        public void Many_Should_Pass()
        {
            var reader = new GameObjectReader(File.OpenRead("many.ced"));
            var l = new List<GameObject>();

            while (reader.HasUnloadedObject)
            {
                var obj = reader.ReadObject();

                // l.Add(obj);
            }

            reader.Close();
        }

        [TestMethod]
        public void Read_Should_Pass()
        {
            var reader = new GameObjectReader(File.OpenRead("test.ced"));

            while (reader.HasUnloadedObject)
            {
                var obj = reader.ReadObject();

                Assert.IsNotNull(obj.Name);
            }

            reader.Close();
        }
    }
}