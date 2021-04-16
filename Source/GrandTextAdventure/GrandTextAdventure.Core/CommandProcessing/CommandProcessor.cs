﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace GrandTextAdventure.Core.CommandProcessing
{
    public static class CommandProcessor
    {
        private static readonly Dictionary<string, ICommand> s_commands = new();

        public static void DisplayCommands()
        {
            foreach (var p in s_commands)
            {
                Console.WriteLine(p.Key);
            }
        }

        public static string[] GetCommandPatterns()
        {
            return s_commands.Keys.ToArray();
        }

        public static void Invoke(string src)
        {
            foreach (var pattern in s_commands)
            {
                var match = Regex.Match(src, pattern.Key, RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);

                if (match.Success)
                {
                    //ToDo:  argumente rausziehen
                    pattern.Value.Invoke(match);
                    return;
                }
            }

            Console.WriteLine("Can't understand what you meant to do");
        }

        public static void Register<T>()
                    where T : ICommand, new()
        {
            var type = typeof(T);

            var attrs = type.GetCustomAttributes<CommandPattern>(true);

            var instance = (ICommand)Activator.CreateInstance(type);

            foreach (var pattern in attrs)
            {
                s_commands.Add(pattern.Pattern, instance);
            }
        }

        public static void ScanForCommands(Assembly assembly)
        {
            var types = assembly.GetTypes().Where(_ => typeof(ICommand).IsAssignableFrom(_));

            foreach (var type in types)
            {
                var attrs = type.GetCustomAttributes<CommandPattern>(true);

                var instance = (ICommand)Activator.CreateInstance(type);

                foreach (var pattern in attrs)
                {
                    s_commands.Add(pattern.Pattern, instance);
                }
            }
        }
    }
}
