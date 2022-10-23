using Darlek.Core.RuntimeLibrary;
using Darlek.Scheme;

namespace GrandTextAdventure.Core.Scripting
{
    [RuntimeType]
    public class DialogFunctions
    {
        [RuntimeMethod("start-dialog")]
        public static object StartDialog(DialogItem dialog)
        {
            Dialog.Start(dialog);

            return None.Instance;
        }
    }
}
