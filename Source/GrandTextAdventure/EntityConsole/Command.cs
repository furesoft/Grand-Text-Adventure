using System;
using System.Linq;

namespace EntityConsole
{
    public record Command(string Name, string[] Args)
    {
        public static Command FromString(string input)
        {
            var spl = input.Split('"')
                     .Select((element, index) => index % 2 == 0  // If even index
                                           ? element.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)  // Split the item
                                           : new string[] { element })  // Keep the entire item
                     .SelectMany(element => element).ToArray();

            var name = spl[0];
            var args = spl[1..];

            return new Command(name, args);
        }
    }
}
