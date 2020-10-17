using SevenWorlds.GameServer.Gameplay.Loop;
using SevenWorlds.Shared.Data.Gameplay;

namespace SevenWorlds.GameServer.Gameplay.Player
{
    public interface IPlayerActionFactory
    {
        PlayerAction GenerateAction(PlayerActionData data, LoopSyncCoordinator syncCoordinator);
    }
}
