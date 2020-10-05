using SevenWorlds.Shared.Data.Chat;

public class SendButton : GameButton
{
    public ChatManager chatManager;

    public override void AfterAwake()
    {
        chatManager = FindObjectOfType<ChatManager>();
    }

    public override void OnClick()
    {
        NetworkService.Object.SendChatMessage(new ChatMessageData() {
            PlayerName = "Unity Client",
            Message = chatManager.InputField.text
        });
    }
}
