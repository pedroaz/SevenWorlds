using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIFinderService : GameService<UIFinderService>
{

    private List<GameText> gameTextDict = new List<GameText>();

    private void Awake()
    {
        Object = this;
        GetAllGameTexts();
    }

    private void GetAllGameTexts()
    {
        foreach (var gameText in Resources.FindObjectsOfTypeAll<GameText>()) {

            if (gameText.Id != GameTextId.None) {
                gameTextDict.Add(gameText);
            }
        }
    }

    public List<GameText> FindText(GameTextId id)
    {
        return gameTextDict.FindAll(x => x.Id == id);
    }

    public List<GameText> SearchText(GameTextId id)
    {
        GetAllGameTexts();
        return FindText(id);
    }
}
