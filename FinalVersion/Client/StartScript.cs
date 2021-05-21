using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets;
using UnityEngine.SceneManagement;

public class StartScript : MonoBehaviour
{
    private Button btnSignIn;
    private Button btnSignUp;
    private InputField username;
    private InputField password;
    // Start is called before the first frame update
    void Start()
    {
        btnSignIn = GameObject.FindWithTag("btnSignIn").GetComponent<Button>() as Button;
        btnSignUp = GameObject.FindWithTag("btnSignUp").GetComponent<Button>() as Button;
        username = GameObject.FindWithTag("username").GetComponent<InputField>() as InputField;
        password = GameObject.FindWithTag("password").GetComponent<InputField>() as InputField;
        btnSignIn.onClick.AddListener(SignIn);
        btnSignUp.onClick.AddListener(SignUp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SignIn()
    {
        NetworkData data = new NetworkData();
        ServerMessage request = new ServerMessage();
        request.ActionId = (int)Actions.SignIn;
        request.Users.Add(new Assets.Database.User()
        {
            Username = username.text
        });
        request.Password = password.text;
        ServerMessage response = data.Query(request);
        if(response.isOk)
        {
            var settings = LocalSettings.GetSettings();
            settings.User.Username = username.text;
            SceneManager.LoadScene("MenuScene");
        }
    }
    
    void SignUp()
    {
        SceneManager.LoadScene("RegistrationScene");
    }
}
