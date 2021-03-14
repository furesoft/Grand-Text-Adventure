using GrandTextAdventure.Core.Parsers.EntityParser;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTests
{
    [TestClass]
    public class ParserTests
    {
        private EntityDefinitionParser _parser = new();
        private string modelSrc = "entitymodel \"hello\" property key = 12 property awsner = 42 end";

        [TestMethod]
        public void ParseEntityModel_Should_Pass()
        {
            var ast = _parser.Parse(modelSrc);

            Assert.IsNotNull(ast);
        }
    }
}