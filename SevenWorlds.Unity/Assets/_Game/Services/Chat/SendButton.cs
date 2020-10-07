using SevenWorlds.Shared.Data.Chat;
using System.Threading.Tasks;

public class SendButton : GameButton
{
    public ChatService chatManager;

    public override void AfterAwake()
    {
        chatManager = FindObjectOfType<ChatService>();
    }

    public override async Task OnClick()
    {
        await NetworkService.Object.SendChatMessage(new ChatMessageData() {
            PlayerName = "Unity Client",
            Message = chatManager.InputField.text
        });
    }
}
