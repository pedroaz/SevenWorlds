using SevenWorlds.Shared.UnityLog;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    public bool connected = false;

    async void Start()
    {

        ScreenChangerService.Object.HideAll();
        await ScreenChangerService.Object.ChangeScreen(ScreenId.Login);


        LOG.Log("Setting up all objects");
        foreach (var item in Resources.FindObjectsOfTypeAll<SetupMonoBehaviour>()) {
            item.Setup();
        }
        
        
        UserInputService.Object.ShouldHandleUserInputs = true;
        SoundService.Object.PlaySong(SongId.Opening);

        try {
            await ConnectToServer();
        }
        catch (Exception e) {
            LOG.Log(e);
        }
    }

    private async Task ConnectToServer()
    {
        UIEvents.ChangeGameText(GameTextId.IsConnectedToServer, "Not Connected to the server");

        while (!connected && Application.isPlaying) {
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
