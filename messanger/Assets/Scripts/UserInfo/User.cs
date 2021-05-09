namespace UserInfo {
    public class User {
        public string id;
        public string date;
        public string username;
        public string email;//regex in loginWindow
        public string password;//********** in password in loginWindow
        

        public User(string id,string date, string username, string email, string password) {
            this.id = id;
            this.date = date;
            this.username = username;
            this.email = email;
            this.password = password;
        }

        public void Create() {
            //TODO Send User's data to server.
        }
    }
}