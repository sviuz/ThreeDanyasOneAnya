using UnityEngine;

namespace UserInfo {
    public class User {
<<<<<<< HEAD
        public string id;
        public string username;
        public string email;//regex in loginWindow
        public string password;//********** in password in loginWindow
        
=======
        private string id;
        private string username;
        private string email;//regex in loginWindow
        private string password;//********** in password in loginWindow
        /*private bool isOnline;
        private bool isShowLastOnline;
        private string lastOnline;*/
>>>>>>> parent of 855cadb (Added some view in main scene)

        public User(string id, string username, string email, string password) {
            this.id = id;
            this.username = username;
            this.email = email;
            this.password = password;
        }

        public void Create() {
            //TODO Send User's data to server.
        }
    }
}