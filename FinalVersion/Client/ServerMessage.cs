using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assets.Database;

namespace Assets
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
        GetLogs,
        GetFile
    }
    enum FileTypeId
    {
        Image = 1,
        Audio,
        Video,
        File
    }
    [Serializable]
    public class ServerMessage
    {
        public bool isOk { get; set; }
        public int ActionId { get; set; }
        public IList<User> Users { get; set; } = new List<User>();
        public IList<Report> Reports { get; set; } = new List<Report>();
        public IList<Chat> Chats { get; set; } = new List<Chat>();
        public IList<Message> Messages { get; set; } = new List<Message>();
        public int Online { get; set; }
        public byte[] File { get; set; }
        public string Filename { get; set; }
        public int TypeId { get; set; }
        public string Password { get; set; }
    }
}
