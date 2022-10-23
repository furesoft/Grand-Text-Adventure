using Darlek.Core.RuntimeLibrary;

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
