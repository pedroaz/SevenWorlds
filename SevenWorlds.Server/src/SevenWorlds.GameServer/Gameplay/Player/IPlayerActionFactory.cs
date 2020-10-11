using SevenWorlds.Shared.Data.Gameplay;

namespace SevenWorlds.GameServer.Gameplay.Player
{
    public interface IPlayerActionFactory
    {
        CharacterAction GenerateAction(PlayerActionData data);
    }
}
