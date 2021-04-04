using System;

namespace GrandTextAdventure.Core.CommandProcessing
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class CommandPattern : Attribute
    {
        public CommandPattern(string pattern)
        {
            Pattern = pattern;
        }

        public string Pattern { get; set; }
    }
}
