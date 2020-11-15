using SevenWorlds.Shared.Data.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    [Header("Data")]
    public CombatData combatData;

    [Header("Objects")]
    public GameText cooldownText;
    public GameText levelText;
    public GameText hpText;
    public Image hpBarImage;

    public void SyncCombatData(CombatData data)
    {
        combatData = data;
        cooldownText.SetText(data.SkillCooldown);
        levelText.SetText(data.Level);
        hpText.SetText($"{data.CurrentHp}/{data.MaxHp}");
        float hpPercentage = data.CurrentHp / data.MaxHp;
        hpBarImage.fillAmount = hpPercentage;
    }
}
