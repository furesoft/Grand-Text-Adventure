using System.Text.RegularExpressions;
using GrandTextAdventure.Core.CommandProcessing;
using GrandTextAdventure.Core.Entities;

namespace GrandTextAdventure.Commands
{
    [CommandPattern(@"Where am I\?")]
    public class WhereAmICommand : ICommand
    {
        public void Invoke(Match args)
        {
            var value = (Room)GameEngine.Instance.GetState("/CurrentMap");

            System.Console.WriteLine("You are at " + value.Name);
        }
    }
}
