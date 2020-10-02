using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameButton : MonoBehaviour
{
    
    private Button button;
    private TextMeshProUGUI textMeshPro;

    private void Awake()
    {
        button = GetComponent<Button>();
        textMeshPro = transform.GetChild(0)?.GetComponent<TextMeshProUGUI>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => {
            OnClick();
        });
        AfterAwake();
    }

    public virtual void AfterAwake()
    {

    }

    public virtual void OnClick()
    {
        
    }

    
}
