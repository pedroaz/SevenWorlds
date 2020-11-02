using SevenWorlds.GameServer.Gameplay.GameState;
using SevenWorlds.GameServer.Utils.Log;
using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.Data.Gameplay.Quests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Quests
{
    public class QuestGiver : IQuestGiver
    {
        private readonly ILogService logService;
        private readonly IGameStateService gameStateService;
        private readonly IQuestFactory questFactory;

        public QuestGiver(ILogService logService, IGameStateService gameStateService, IQuestFactory questFactory)
        {
            this.logService = logService;
            this.gameStateService = gameStateService;
            this.questFactory = questFactory;
        }

        public List<QuestData> GetQuests(string playerName)
        {
            logService.Log($"Getting quest from player: {playerName}");
            var player = RefreshPlayerQuests(playerName);
            if (player == null) return null;
            return player.Quests;
        }

        public PlayerData RefreshPlayerQuests(string playerName)
        {
            var player = gameStateService.PlayerCollection.FindByPlayerName(playerName);
            if (player == null) return null;
            GiveNewAvailableQuestsToPlayer(player);
            RefreshStatus(player);
            return player;
        }

        private void GiveNewAvailableQuestsToPlayer(PlayerData player)
        {
            if(!player.Quests.FindAll(x => x.Description.QuestId == QuestId.InitialQuest).Any()) {
                player.Quests.Add(questFactory.CreateNewQuest(QuestId.InitialQuest));
            }
        }

        private static void RefreshStatus(PlayerData player)
        {
            foreach (var quest in player.Quests) {
                quest.RefreshStatus();
            }
        }
    }
}
