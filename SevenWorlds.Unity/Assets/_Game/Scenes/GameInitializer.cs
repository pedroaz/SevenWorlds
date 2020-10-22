using System.Threading.Tasks;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    public bool connected = false;

    async void Start()
    {
        await ScreenChangerService.Object.ChangeScreen(ScreenId.Login);

        await ConnectToServer();
    }

    private async Task ConnectToServer()
    {
        UIEvents.ChangeGameText(GameTextId.IsConnectedToServer, "Not Connected to the server");

        while (!connected) {
            connected = await NetworkService.Object.ConnectToServer();
            if (connected) {
                UIEvents.ChangeGameText(GameTextId.IsConnectedToServer, "Connected to the server");
            }
            else {
                UIEvents.ChangeGameText(GameTextId.IsConnectedToServer, "Not Connected to the server");
            }
        } 

        
    }
}
