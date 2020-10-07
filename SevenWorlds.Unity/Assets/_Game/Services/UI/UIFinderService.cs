using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFinderService : GameService<UIFinderService>
{

    private Dictionary<GameTextId, GameText> gameTextDict = new Dictionary<GameTextId, GameText>();

    private void Awake()
    {
        Object = this;
        foreach (var gameText in FindObjectsOfType<GameText>()) {

            if(gameText.Id != GameTextId.None) {
                gameTextDict.Add(gameText.Id, gameText);
            }
        }
    }

    public GameText FindText(GameTextId id)
    {
        return gameTextDict[id];
    }

    public GameText SearchText(GameTextId id)
    {
        foreach (var gameText in FindObjectsOfType<GameText>()) {

            if (gameText.Id == id) {
                return gameText;
            }
        }

        return null;
    }
}
