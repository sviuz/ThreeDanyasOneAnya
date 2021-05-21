using Assets.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets
{
    class LocalSettings
    {
        public User User;
        public List<Chat> Chats;
        private LocalSettings() 
        {
            User = new User();
            Chats = new List<Chat>();
        }
        private static LocalSettings settings;
        public static LocalSettings GetSettings()
        {
            if(settings == null)
            {
                settings = new LocalSettings();
            }
            return settings;
        }
    }
}
