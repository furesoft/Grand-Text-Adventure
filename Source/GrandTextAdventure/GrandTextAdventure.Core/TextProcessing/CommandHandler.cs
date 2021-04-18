using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GrandTextAdventure.Core.TextProcessing.Interfaces;
using GrandTextAdventure.Core.TextProcessing.Synonyms;

namespace GrandTextAdventure.Core.TextProcessing
{
    public static class CommandHandler
    {
        private static readonly Dictionary<VerbCodes, ICommandHandler> s_handlers = new();

        public static void Collect()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(_ => _.GetTypes());
            foreach (var t in types)
            {
                if (typeof(ICommandHandler).IsAssignableFrom(t) && !t.IsInterface)
                {
                    var attr = t.GetCustomAttribute<CommandHandlerAttribute>();
                    var instance = (ICommandHandler)Activator.CreateInstance(t);

                    s_handlers.Add(attr.Verb, instance);
                }
            }
        }

        public static void Invoke(string command)
        {
            var parser = new Parser();
            var cmd = parser.ParseCommand(command);

            if (s_handlers.ContainsKey(cmd.Verb))
            {
                s_handlers[cmd.Verb].Invoke(cmd);
            }
            else
            {
                throw new Exception($"No Handler for Verb '{cmd.Verb}' found");
            }
        }
    }
}
