using System.Collections.Generic;
using DefaultNamespace;
using Messages;

public static class MessageHandler{
    private static List<Message> _messages;

    public static void LoadBase(string myId, string friendId) {
        _messages = WordWithData.GetAllChat(myId,friendId);
    }

}
