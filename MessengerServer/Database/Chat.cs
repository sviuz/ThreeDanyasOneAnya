﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MessengerServer.Database
{
    public class Chat
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ImagePath { get; set; }
        public ICollection<Message> Messages { get; set; } = new List<Message>();
        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
