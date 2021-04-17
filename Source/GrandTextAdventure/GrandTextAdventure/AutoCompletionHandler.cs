using System;
using System.Collections.Generic;
using System.Linq;

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
            return Array.Empty<string>();
        }
    }
}
