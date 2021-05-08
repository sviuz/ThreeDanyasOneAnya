namespace UserInfo {
    public class User {
        public string id;
        public string username;
        public string email;//regex in loginWindow
        public string password;//********** in password in loginWindow
        

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