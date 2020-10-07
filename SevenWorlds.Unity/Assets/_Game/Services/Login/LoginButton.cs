using System.Threading.Tasks;

public class LoginButton : GameButton
{
    public override async Task OnClick()
    {
        var response = await LoginService.Object.Login();
        if (response.Success) {
            GameState.Object.PlayerData = response.PlayerData;
        }
        else {

        }
    }
}
