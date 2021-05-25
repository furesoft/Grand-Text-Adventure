using System;
using System.Collections.Generic;

namespace GrandTextAdventure.Core
{
    public static class Phone
    {
        private static readonly Dictionary<string, Action> s_numberHandlers = new();

        public static void Dial(string number)
        {
            if (s_numberHandlers.ContainsKey(number))
            {
                s_numberHandlers[number]();
            }
            else
            {
                Console.WriteLine("Number is not reachable or not registered");
            }
        }

        public static void AddNumber(string number, Action handler)
        {
            s_numberHandlers.Add(number, handler);
        }
    }
}