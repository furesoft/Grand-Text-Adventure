using GrandTextAdventure.Core.Parser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class ParserTests
    {
        private readonly EflDefinitionParser _parser = new();
        private readonly string modelSrc = "entitymodel \"hello\" property key = 12 property awsner = 42 end";

        [TestMethod]
        public void ParseEntityModel_Should_Pass()
        {
            var ast = _parser.Parse(modelSrc);

            var visitor = new EntityDefinitionVisitor();
            ast.Accept(visitor);

            Assert.IsNotNull(ast);
            Assert.IsTrue(_parser.Diagnostics.Any());
        }
    }
}