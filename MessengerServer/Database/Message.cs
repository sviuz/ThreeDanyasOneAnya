using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerServer.Database
{
    public class Message
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public Media Media { get; set; }
        public int? MediaId { get; set; }
        public Chat Chat { get; set; }
        public int? ChatId { get; set; }
        public User User { get; set; }
        public int? UserId { get; set; }
        public DateTime Time { get; set; }
    }
}
