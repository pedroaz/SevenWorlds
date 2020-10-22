using SevenWorlds.Shared.UnityLog;
using System.Threading.Tasks;

public class RegistrationButton : GameButton
{
    public override async Task OnClick()
    {
        await RegistrationService.Object.TryToRegister();
    }
}
