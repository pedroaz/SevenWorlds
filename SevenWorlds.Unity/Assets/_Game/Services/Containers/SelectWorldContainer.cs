using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectWorldContainer : MonoBehaviour
{
    private void Awake()
    {
        var buttons = Resources.FindObjectsOfTypeAll<SelectWorldButton>();
        var parent = buttons[0].transform.parent;

        for (int i = 0; i < 7; i++) {
            parent.GetChild(i).GetComponent<SelectWorldButton>().WorldIndex = i;        
        }
    }
}
