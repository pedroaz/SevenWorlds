using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameButton : MonoBehaviour
{
    
    private Button button;
    private TextMeshProUGUI textMeshPro;

    private void Awake()
    {
        button = GetComponentInChildren<Button>();
        textMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => {
            _ = OnClick();
        });
        AfterAwake();
    }

    public virtual void AfterAwake()
    {

    }

    public virtual async Task OnClick()
    {
        await new Task(() => { });
    }
}
