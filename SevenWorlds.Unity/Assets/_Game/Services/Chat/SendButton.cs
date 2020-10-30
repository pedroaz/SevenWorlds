using SevenWorlds.Shared.Data.Chat;
using System.Threading.Tasks;

public class SendButton : GameButton
{
    public ChatService chatManager;

    public override void Setup()
    {
        chatManager = FindObjectOfType<ChatService>();
    }

    public override async Task OnClick()
    {
        await NetworkService.SendChatMessage(new ChatMessageData("Unity Client", chatManager.InputField.text));
    }
}
