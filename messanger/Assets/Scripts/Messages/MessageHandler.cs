using System.Collections.Generic;
using Data;

namespace Messages {
    public static class MessageHandler{
        private static List<Message> _messages;

<<<<<<< HEAD
        public static void LoadBase(string myId, string friendId) {
            _messages = WorkWithData.GetAllChat(myId,friendId);
        }
=======
    public static void LoadBase(string myId, string friendId) {
        _messages = WordWithData.GetAllChat(myId,friendId);
    }
>>>>>>> parent of 855cadb (Added some view in main scene)

    }
}
