using GrandTextAdventure.Core.Parser;
using GrandTextAdventure.Core.Parser.Visitors;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class ParserTests
    {
        private readonly EflDefinitionParser _parser = new();
        private readonly string modelSrc = "entitymodel \"baseVehicle\" property key = 3.14 property awsner = 42 property str = \"hello\" end entity \"hello\" is vehicle applymodel \"baseVehicle\" end";

        [TestMethod]
        public void ParseEntityModel_Should_Pass()
        {
            var ast = _parser.Parse(modelSrc);

            var visitor = new EntityDefinitionVisitor();
            ast.Accept(visitor);

            var textVisitor = new PrintVisitor();
            ast.Accept(textVisitor);

            Assert.IsNotNull(ast);
            Assert.IsFalse(_parser.Diagnostics.Any());
        }
    }
}