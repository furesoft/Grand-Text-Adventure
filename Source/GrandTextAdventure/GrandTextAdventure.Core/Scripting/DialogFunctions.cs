using Darlek.Core.RuntimeLibrary;
using GrandTextAdventure.Core.Dialogs;

namespace GrandTextAdventure.Core.Scripting;

[RuntimeType]
public class DialogFunctions
{
    [RuntimeMethod("start-dialog")]
    public static void StartDialog(DialogItem dialog)
    {
        Dialog.Start(dialog);
    }
}
