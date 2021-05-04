using UnityEngine;

namespace UserInfo {
    public class User {
        private string id;
        private string username;
        private string email;//regex in loginWindow
        private string password;//********** in password in loginWindow
        /*private bool isOnline;
        private bool isShowLastOnline;
        private string lastOnline;*/

        public User(string id, string username, string email, string password) {
            this.id = id;
            this.username = username;
            this.email = email;
            this.password = password;
        }

    }
}