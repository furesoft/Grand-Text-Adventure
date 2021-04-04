using System.Text.RegularExpressions;

namespace GrandTextAdventure.Core.CommandProcessing
{
    public interface ICommand
    {
        void Invoke(Match args);
    }
}
