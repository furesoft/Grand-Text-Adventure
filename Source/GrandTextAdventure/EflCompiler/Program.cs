using System;
using System.Collections.Generic;
using CommandLine;

namespace EflCompiler
{
    internal class Program
    {
        private static void HandleParseError(IEnumerable<Error> errors)
        {
            foreach (var err in errors)
            {
                Console.WriteLine(err); ;
            }
        }

        private static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed(RunOptions)
                .WithNotParsed(HandleParseError);
        }

        private static void RunOptions(Options opts)
        {
            var compiler = new Compiler(opts.InputFiles, opts.OutputName);
            compiler.Invoke();
        }
    }
}
