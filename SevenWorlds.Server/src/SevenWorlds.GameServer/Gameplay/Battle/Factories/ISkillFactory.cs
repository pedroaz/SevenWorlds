using SevenWorlds.Shared.Data.Gameplay.Skills;
using System.Collections.Generic;

namespace SevenWorlds.GameServer.Gameplay.Battle.Factories
{
    public interface ISkillFactory
    {
        SkillData CreateNewSkillData(SkillId type);
        List<SkillData> CreateListOfSkillDatas(List<SkillId> types);
        void SetupStorage();
    }
}
