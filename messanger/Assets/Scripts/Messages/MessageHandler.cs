using System.Collections.Generic;
using Data;

namespace Messages {
    public static class MessageHandler{
        private static List<Message> _messages;

        public static void LoadBase(string myId, string friendId) {
            _messages = WorkWithData.GetAllChat(myId,friendId);
        }

    }
}
