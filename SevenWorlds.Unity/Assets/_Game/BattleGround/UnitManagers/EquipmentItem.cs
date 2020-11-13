using SevenWorlds.Shared.Data.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AttackAnimation
{
    None,
    Swing
}

[CreateAssetMenu(menuName = "SevenWorlds/EquipmentItem")]
public class EquipmentItem : ScriptableObject
{
    public EquipmentId equipmentId;
    public GameObject prefab;
    public Vector2 parentPosition;
    public Vector2 equipmentPosition;
    public AttackAnimation attackAnimation; 
}
