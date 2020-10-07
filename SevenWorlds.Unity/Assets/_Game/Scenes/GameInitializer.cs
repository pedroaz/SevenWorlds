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
            UIFinderService.Object.FindText(GameTextId.IsConnectedToServer).ForEach(x => x.SetText("Connected to the server"));
        }
        else {
            UIFinderService.Object.FindText(GameTextId.IsConnectedToServer).ForEach(x => x.SetText("Not Connected to Server"));
        }
    }
}
