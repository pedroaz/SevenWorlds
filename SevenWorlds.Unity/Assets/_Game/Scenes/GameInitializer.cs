using System.Threading.Tasks;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    async void Start()
    {
        ScreenChangerService.Object.ChangeScreen(ScreenId.Login);

        await ConnectToServer();
    }

    private async Task ConnectToServer()
    {
        var connected = await NetworkService.Object.ConnectToServer();
        if (connected) {
            print("Connection to server was ok!");
            UIEvents.ChangeGameText(GameTextId.IsConnectedToServer, "Connected to the server");
        }
        else {
            UIEvents.ChangeGameText(GameTextId.IsConnectedToServer, "Not Connected to the server");
        }
    }
}
