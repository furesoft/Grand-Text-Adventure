using Darlek.Core.RuntimeLibrary;
using GrandTextAdventure.Core.Scripting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class ScriptTests
    {
        [TestMethod]
        public void ParseEntityModel_Should_Pass()
        {
            var script = new GrandTextAdventure.Core.Script
            {
                Source =
                "(define baseVehicle (entity-model (list (property \"speed\" 100)(property \"protection\" 50)))) (vehicle \"lambo\" (list (applymodel baseVehicle) (property \"speed\" 150)))"
            };
            SchemeCliLoader.Apply(typeof(EntityFunctions).Assembly, script.interpreter);

            var result = script.Execute();

            Assert.IsNotNull(script);
        }
    }
}