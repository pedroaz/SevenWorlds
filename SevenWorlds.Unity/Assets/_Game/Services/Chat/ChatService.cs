using SevenWorlds.Shared.Data.Chat;
using TMPro;
using UnityEngine;

public class ChatService : GameService<ChatService>
{
    public TMP_InputField InputField;
    public GameText ChatArea;

    private void Awake()
    {
        Object = this;
        NetworkEvents.OnChatMessageRecieved += AddMessageToArea;
    }

    private void OnDestroy()
    {
        NetworkEvents.OnChatMessageRecieved -= AddMessageToArea;
    }

    public void AddMessageToArea(object sender, NetworkArgs<ChatMessageData> args)
    {
        if (args.Data == null) return;
        ChatArea.AppendTextToNewLine(args.Data.Message);
    }
}
