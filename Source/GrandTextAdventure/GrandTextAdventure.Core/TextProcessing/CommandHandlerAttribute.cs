using System;
using GrandTextAdventure.Core.TextProcessing.Synonyms;

namespace GrandTextAdventure.Core.TextProcessing
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public class CommandHandlerAttribute : Attribute
    {
        public CommandHandlerAttribute(VerbCodes verb)
        {
            Verb = verb;
        }

        public VerbCodes Verb { get; }
    }
}
