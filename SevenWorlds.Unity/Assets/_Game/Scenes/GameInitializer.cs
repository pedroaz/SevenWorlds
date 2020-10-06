using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    async void Start()
    {
        ScreenChangerService.Object.ChangeScreen(ScreenId.Login);
        var connected = await NetworkService.Object.ConnectToServer();
    }
}
