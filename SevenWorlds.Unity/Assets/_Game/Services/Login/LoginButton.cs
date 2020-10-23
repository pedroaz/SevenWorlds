using System.Threading.Tasks;

public class LoginButton : GameButton
{
    public override async Task OnClick()
    {
        var response = await LoginService.Object.TryToLogin();
        await LoginService.Object.ProcessLoginResponse(response);
    }

   
}
