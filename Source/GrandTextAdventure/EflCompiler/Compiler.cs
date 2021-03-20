using System;
using System.Collections.Generic;
using System.IO;
using GrandTextAdventure.Core;

namespace EflCompiler
{
    internal class Compiler
    {
        private readonly IEnumerable<string> _inputFiles;
        private readonly GameObjectWriter _writer;

        public Compiler(IEnumerable<string> inputFiles, string outputName)
        {
            _inputFiles = inputFiles;

            if (!outputName.EndsWith(".ced"))
            {
                outputName += ".ced";
            }

            _writer = new(File.OpenWrite(outputName));
        }

        public void Invoke()
        {
            foreach (var file in _inputFiles)
            {
                var parser = new GrandTextAdventure.Core.Parser.EflDefinitionParser();
                var ast = parser.Parse(File.ReadAllText(file));

                var compilerVisitor = new CompilerAstVisitor(_writer);
                ast.Accept(compilerVisitor);
            }

            _writer.Close();
        }
    }
}
