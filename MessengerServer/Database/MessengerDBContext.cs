using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace MessengerServer.Database
{
    public class MessengerDBContext : DbContext
    {
        public MessengerDBContext() : base("defaultConnection") { }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<EventType> EventTypes { get; set; }
        public DbSet<Format> Formats { get; set; }
        public DbSet<Log> Logs { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Type> Types { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
