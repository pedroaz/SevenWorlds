using SevenWorlds.Shared.Data.Chat;

public class SendButton : GameButton
{
    public ChatService chatManager;

    public override void AfterAwake()
    {
        chatManager = FindObjectOfType<ChatService>();
    }

    public override void OnClick()
    {
        NetworkService.Object.SendChatMessage(new ChatMessageData() {
            PlayerName = "Unity Client",
            Message = chatManager.InputField.text
        });
    }
}
