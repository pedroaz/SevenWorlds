﻿using SevenWorlds.Shared.Data.Gameplay;
using SevenWorlds.Shared.UnityLog;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class SelectWorldButton : GameButton
{
    [HideInInspector]
    public int WorldIndex;

    public GameText worldNameText;
    public GameText characterText;

    public override async Task OnClick()
    {
        try {
            GameState.SetCurrentWorldByWorldIndex(WorldIndex);
            UIEvents.ChangeGameText(GameTextId.WorldName, GameState.CurrentWorld.Name);

            var character = GameState.GetCharacterByWorldId(GameState.CurrentWorld.Id);

            if (character == null) {
                LOG.Log($"Player does not have a character on world: {GameState.CurrentWorld.Id}");
                await ScreenChangerService.ChangeScreen(ScreenId.CreateCharacter);
            }
            else {
                LOG.Log($"Found Character!");
                await ScreenChangerService.ChangeScreen(ScreenId.SelectCharacter);
            }
        }
        catch (System.Exception e) {

            LOG.Log(e);
            throw;
        }

        
    }

    public void Refresh(WorldData data, CharacterData characterData)
    {
        worldNameText.SetText(data.Name);
        if(characterData == null) {
            characterText.SetText("No Character");
        }
        else {
            characterText.SetText($"Character Lvl: {characterData.Level}");
        }
    }
}
