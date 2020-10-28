using SevenWorlds.Shared.UnityLog;
using System.Threading.Tasks;

public class LoginButton : GameButton
{
    public override async Task OnClick()
    {
        try {
            var response = await LoginService.Object.TryToLogin();
            await LoginService.Object.ProcessLoginResponse(response);
        }
        catch (System.Exception e) {
            LOG.Log(e);
        }
    }
}
