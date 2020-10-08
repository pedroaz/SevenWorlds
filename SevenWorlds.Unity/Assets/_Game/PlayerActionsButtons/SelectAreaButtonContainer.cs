using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectAreaButtonContainer : MonoBehaviour
{
    private void Awake()
    {
        var button = Resources.FindObjectsOfTypeAll<SelectAreaButton>();
        var parent = button[0].transform.parent;


        for (int i = 0; i < 10; i++) {
            for (int j = 0; j < 10; j++) {
                parent.GetChild(i + j * 10).GetComponent<SelectAreaButton>().SetupPosition(i, j);
            }
        }
    }
}
