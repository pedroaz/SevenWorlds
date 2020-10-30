using SevenWorlds.Shared.UnityLog;
using System.Threading.Tasks;

public class LoginButton : GameButton
{
    public override async Task OnClick()
    {
        try {
            var response = await LoginService.TryToLogin();
            await LoginService.ProcessLoginResponse(response);
        }
        catch (System.Exception e) {
            LOG.Log(e);
        }
    }
}
