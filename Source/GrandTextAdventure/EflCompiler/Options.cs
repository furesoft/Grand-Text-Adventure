using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;

namespace EflCompiler
{
    internal class Options
    {
        [Option('f', "file", Required = true, HelpText = "Input files to be processed.")]
        public IEnumerable<string> InputFiles { get; set; }

        [Option('o', "output", Required = true, HelpText = "The name of the compiled output file.")]
        public string OutputName { get; set; }
    }
}
