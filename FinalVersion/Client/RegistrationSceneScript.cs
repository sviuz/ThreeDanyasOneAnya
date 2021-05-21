using Assets;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class RegistrationSceneScript : MonoBehaviour
{
    private InputField username;
    private InputField password;
    private InputField email;
    private Button btnLoadImage;
    private Button btnSignUp;
    private string filepath;
    // Start is called before the first frame update
    void Start()
    {
        username = GameObject.FindWithTag("username").GetComponent<InputField>() as InputField;
        password = GameObject.FindWithTag("password").GetComponent<InputField>() as InputField;
        email = GameObject.FindWithTag("email").GetComponent<InputField>() as InputField;
        btnLoadImage = GameObject.FindWithTag("btnLoadImage").GetComponent<Button>() as Button;
        btnSignUp = GameObject.FindWithTag("btnSignUp").GetComponent<Button>() as Button;
        btnLoadImage.onClick.AddListener(LoadImage);
        btnSignUp.onClick.AddListener(SignUp);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void LoadImage()
    {
        filepath = EditorUtility.OpenFilePanel("Select Image (max size - 5 MB)", "", "jpg");
    }

    void SignUp()
    {
        NetworkData data = new NetworkData();
        ServerMessage request = new ServerMessage();
        request.File = File.ReadAllBytes(filepath);
        request.Users.Add(new Assets.Database.User()
        {
            Username = username.text,
            Email = email.text
        });
        request.ActionId = (int)Actions.SignUp;
        request.Password = password.text;
        ServerMessage response = data.Query(request);
        if(response.isOk)
        {
            var settings = LocalSettings.GetSettings();
            settings.User.Username = username.text;
            settings.User.Email = email.text;
            SceneManager.LoadScene("MenuScene");
        }
    }
}
