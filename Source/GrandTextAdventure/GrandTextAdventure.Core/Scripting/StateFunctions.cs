using Darlek.Core.RuntimeLibrary;
using Darlek.Scheme;
using GrandTextAdventure.Core.Entities;
using GrandTextAdventure.Core.Game;

namespace GrandTextAdventure.Core.Scripting
{
    [RuntimeType]
    public class StateFunctions
    {
        [RuntimeMethod("get-player")]
        public static PlayerCharackter GetPlayer(GameState state)
        {
            return state.Player;
        }

        [RuntimeMethod("get-inventory")]
        public static Inventory GetInventory(PlayerCharackter player)
        {
            return player.Inventory;
        }

        [RuntimeMethod("get-state")]
        public static GameState GetState()
        {
            return GameEngine.Instance.GetState();
        }

        [RuntimeMethod("start-dialog")]
        public static object StartDialog(DialogItem dialog)
        {
            Dialog.Start(dialog);

            return None.Instance;
        }
    }
}
