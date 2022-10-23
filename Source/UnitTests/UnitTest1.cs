using Darlek.Core.RuntimeLibrary;
using GrandTextAdventure.Core.Scripting;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests;

[TestClass]
public class ScriptTests
{
    [TestMethod]
    public void ParseEntityModel_Should_Pass()
    {
        var script = new GrandTextAdventure.Core.Script
        {
            Source =
            "(start-dialog nil)"
        };
        SchemeCliLoader.Apply(typeof(EntityFunctions).Assembly, script.interpreter);

        var result = script.Execute();

        Assert.IsNotNull(script);
    }
}