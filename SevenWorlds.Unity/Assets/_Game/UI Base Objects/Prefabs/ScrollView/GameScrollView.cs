using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameScrollView : MonoBehaviour
{
    public Transform contentContainer;

    public void Clear()
    {
        for (int i = contentContainer.childCount-1; i >= 0 ; i--) {
            Destroy(contentContainer.GetChild(i).gameObject);
        }
    }
}
