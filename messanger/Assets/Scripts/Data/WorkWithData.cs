﻿using System.Collections.Generic;
using Messages;
using UserInfo;

namespace DefaultNamespace {
    public static class WorkWithData {
        private static List<User> userList;

        private static void LoadBase() {
            userList = GetAllFriendsFromServer();
        }
        public static List<Message> GetAllChat(string ownId, string friendId) {
            
            //TODO реализовать получение всех сообщений по id пользователя и id собеседника
            return null;
        }

        public static List<User> GetAllFriendsFromServer() {
            return null;
        }

        public static User GetUserById(string ID) {
            return userList.Find(user => user.id == ID);
        }
        
    }
}