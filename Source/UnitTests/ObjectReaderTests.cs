using GrandTextAdventure.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;

namespace UnitTests
{
    [TestClass]
    public class ObjectReaderTests
    {
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

            Assert.AreSame(reader.Count, 2);
        }
    }
}