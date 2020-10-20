namespace SevenWorlds.Shared.Data.Gameplay.ActionDatas
{
    public class StartBattleActionData : PlayerActionData
    {
        private MonsterType MonsterType { get; set; }

        public StartBattleActionData(MonsterType monsterType)
        {
            MonsterType = monsterType;
        }
    }
}
