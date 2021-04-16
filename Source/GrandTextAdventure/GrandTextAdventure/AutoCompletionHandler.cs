using System;
using System.Collections.Generic;
using System.Linq;
using GrandTextAdventure.Core.CommandProcessing;

namespace GrandTextAdventure
{
    class AutoCompletionHandler : IAutoCompleteHandler
    {
        // characters to start completion from
        public char[] Separators { get; set; } = new char[] { ' ', '.', '/' };

        // text - The current text entered in the console
        // index - The index of the terminal cursor within {text}
        public string[] GetSuggestions(string text, int index)
        {
            var patterns = CommandProcessor.GetCommandPatterns().Select(_ =>
            {
                if (_.Contains("(") && _.Contains(")"))
                {

                    var pFrom = _.IndexOf("(") + "(".Length;
                    var pTo = _.LastIndexOf(")");

                    var result = _[pFrom..pTo];
                    var options = result.Split('|');

                    var posibilities = new List<string>();

                    foreach (var opt in options)
                    {
                        var withOption = _.Substring(0, pFrom - 1) + opt;

                        posibilities.Add(withOption);
                    }

                    return _;
                }
                else
                {
                    return _;
                }
            });

            return patterns.ToArray();
        }
    }
}
