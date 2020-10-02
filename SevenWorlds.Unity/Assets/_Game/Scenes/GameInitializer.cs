using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    void Start()
    {
        ScreenChangerService.Object.ChangeScreen(ScreenId.Chat);
    }
}
