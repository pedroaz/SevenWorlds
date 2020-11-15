using SevenWorlds.Shared.Data.Gameplay;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteStorage : ScriptableObject
{
    public List<GameSprite> spriteObjects;

    public GameObject GetSprite(EquipmentId id)
    {
        return spriteObjects.Find(x => x.equipmentId == id).gameObject;
    }

    public GameObject GetSprite(CharacterType type)
    {
        return spriteObjects.Find(x => x.characterType == type).gameObject;
    }
}
