using SevenWorlds.Shared.Data.Connection;
using SevenWorlds.Shared.UnityLog;
using System.Threading.Tasks;

public class RegistrationButton : GameButton
{
    public override async Task OnClick()
    {
        var response = await RegistrationService.TryToRegister();
        await Task.Delay(1000);
        if(response.response == RegisterAccountResponseType.Success) {
            await ScreenChangerService.ChangeScreen(ScreenId.Login);
        }
    }
}
