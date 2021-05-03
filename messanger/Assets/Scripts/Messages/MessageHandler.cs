using System.Collections.Generic;
using Messages;

public static class MessageHandler{
    private static List<Message> _messages;

    public static void LoadBase() {
        _messages = new List<Message>();
    }

}
