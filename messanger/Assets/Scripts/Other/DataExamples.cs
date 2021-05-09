using System.Collections.Generic;
using Other;
using UserInfo;

public static class DataExamples {
    private static string[] generatedUsernames = {"ball", "book", "pen", "tape", "backpack", "pen"}; //6 items

    private static string[] generatedEmails = {
        "czizi1422@icahu.com", "xahmedhamada1203@tubidu.com", "5eyhsumerandc@test130.com", "7mhmadahdb@gddao.com",
        "tradioacweedr@pickuplanet.com", "ssnaz@v-mail.xyz"
    };

    private static List<User> users = new List<User>();

    public static List<User> GetGeneratedUserList() {
        GenerateUsers();
        return users;
    }

    private static void GenerateUsers() {
        for (int i = 0; i <= 5; i++) {
            users.Add(new User(
                StringGenerator.GenerateString(),
                StringGenerator.GenerateDate(),
                generatedUsernames[i], 
                generatedEmails[i], 
                StringGenerator.GenerateString()));
        }
    }
}