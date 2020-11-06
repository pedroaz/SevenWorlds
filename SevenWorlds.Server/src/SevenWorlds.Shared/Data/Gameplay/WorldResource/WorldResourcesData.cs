using System.Collections.Generic;

namespace SevenWorlds.Shared.Data.Gameplay
{
    public enum WorldResourceType
    {
        Gold,
        Rock,
        Wood
    }

    public class WorldResourcesData
    {
        public Dictionary<WorldResourceType, int> Resources;

        public bool HasEnoughForSkill(Dictionary<WorldResourceType, int> cost)
        {
            foreach (var kvp in cost) {
                if (Resources[kvp.Key] < cost[kvp.Key]) return false;
            }
            return true;
        }

        public void ConsumeSkillCost(Dictionary<WorldResourceType, int> cost)
        {
            foreach (var kvp in cost) {
                Resources[kvp.Key] -= cost[kvp.Key];
            }
        }

        public WorldResourcesData()
        {
            Resources = new Dictionary<WorldResourceType, int>(){
                { WorldResourceType.Gold, 0 },
                { WorldResourceType.Rock, 0 },
                { WorldResourceType.Wood, 0 },
            };
        }
    }
}
