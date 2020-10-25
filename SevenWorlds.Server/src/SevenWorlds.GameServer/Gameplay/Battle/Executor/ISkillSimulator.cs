using SevenWorlds.Shared.Data.Gameplay;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SevenWorlds.GameServer.Gameplay.Battle.Executor
{
    public interface ISkillSimulator
    {
        void SimulateSkill(CombatData caster, List<CombatData> targets, WorldResourcesData resources);
    }
}
