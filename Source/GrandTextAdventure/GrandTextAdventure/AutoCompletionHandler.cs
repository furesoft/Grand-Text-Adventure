using System;

namespace GrandTextAdventure {
    class AutoCompletionHandler : IAutoCompleteHandler
    {
        // characters to start completion from
        public char[] Separators { get; set; } = new char[] { ' ', '.', '/' };

        // text - The current text entered in the console
        // index - The index of the terminal cursor within {text}
        public string[] GetSuggestions(string text, int index)
        {
            if (text.StartsWith("git "))
                return index == 0 ? (new string[] { "init", "clone", "pull", "push" }) : (new string[] { "something", "other", "nothing", "special" });
            else
                return null;
        }
    }
}
