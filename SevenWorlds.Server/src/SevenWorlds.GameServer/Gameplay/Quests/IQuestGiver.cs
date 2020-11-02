using SevenWorlds.Shared.Data.Gameplay.Quests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Quests
{
    public interface IQuestGiver
    {
        List<QuestData> GetQuests(string playerName, QuestStatus status);
    }
}
