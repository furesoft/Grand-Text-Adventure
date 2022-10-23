using Darlek.Core.RuntimeLibrary;
using GrandTextAdventure.Core.Entities;
using GrandTextAdventure.Core.Game;

namespace GrandTextAdventure.Core.Scripting;

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
}
