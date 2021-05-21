﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseSetup.Database
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public string Email { get; set; }
        public Role Role { get; set; }
        public int? RoleId { get; set; }
        public string ImagePath { get; set; }
        public bool IsOnline { get; set; }
        public bool IsBanned { get; set; }
        public virtual ICollection<User> Friends { get; set; } = new List<User>();
        public virtual ICollection<Chat> Chats { get; set; } = new List<Chat>();
    }
}
