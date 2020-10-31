using SevenWorlds.Shared.UnityLog;
using System;
using System.Threading.Tasks;
using UnityEngine;

public class GameInitializer : MonoBehaviour
{
    public bool connected = false;

    async void Start()
    {
        // MUST BE THE FIRST THING
        LOG.Log("Setting up all objects");
        foreach (var item in Resources.FindObjectsOfTypeAll<SetupMonoBehaviour>()) {
            item.Setup();
        }

        ScreenChangerService.HideAll();
        await ScreenChangerService.ChangeScreen(ScreenId.Login);
        
        UserInputService.ShouldHandleUserInputs = true;
        SoundService.PlaySong(SongId.Opening);

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
            connected = await NetworkService.ConnectToServer();
            if (connected) {
                UIEvents.ChangeGameText(GameTextId.IsConnectedToServer, "Connected to the server");
            }
            else {
                UIEvents.ChangeGameText(GameTextId.IsConnectedToServer, "Not Connected to the server");
                LOG.Log("Failed to connect to server. Waiting 3 seconds and trying again");
                await Task.Delay(3000);
            }
        } 
    }
}
