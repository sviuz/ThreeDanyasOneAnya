using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MessengerServer.Database;

namespace MessengerServer
{
    enum Actions
    {
        SignUp,
        SignIn,
        SignOut,
        GetPersonalInfo,
        SetPersonalInfo,
        GetUsersList,
        AddFriend,
        DelFriend,
        GetFriendsList,
        SendReport,
        GetReportsList,
        SetModerator,
        Ban,
        Unban,
        GetChatsList,
        AddChat,
        EditChat,
        DelChat,
        GetMessages,
        AddMessage,
        EditMessage,
        DelMessage,
        AddFormat,
        CreateAdmin,
        GetLogs
    }
    enum FileTypeId
    {
        Image = 1,
        Audio,
        Video,
        File
    }
    public class ServerMessage
    {
        public bool isOk { get; set; }
        public int ActionId { get; set; }
        public IList<User> Users { get; set; } = new List<User>();
        public IList<Report> Reports { get; set; } = new List<Report>();
        public IList<Chat> Chats { get; set; } = new List<Chat>();
        public IList<Message> Messages { get; set; } = new List<Message>();
        public Format Format { get; set; }
        public IList<Log> Logs { get; set; }
        public int Online { get; set; }
        public byte[] File { get; set; }
        public string Filename { get; set; }
        public int TypeId { get; set; }
        public string Password { get; set; }
    }
}
