using System;
using System.Net.Mail;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UserInfo;

public class LogRegWindow : MonoBehaviour {
    [Header("Login Section")] [SerializeField]
    private Button LoginButton;

    [Space] [SerializeField] private Text emailLogin;
    [SerializeField] private Text passwordLogin;

    [Header("Registr ation Section")] [SerializeField]
    private Button RegisterButton;

    [Space] [SerializeField] private Text username;
    [SerializeField] private Text emailRegister;
    [SerializeField] private Text passwordRegister;

    void Start() {
        LoginButton.onClick.AddListener(OnLoginClick);
        RegisterButton.onClick.AddListener(OnRegisterClick);
    }

    private void OnRegisterClick() {
        if (string.IsNullOrEmpty(username.text) && string.IsNullOrEmpty(emailRegister.text) && string.IsNullOrEmpty(passwordRegister.text)) {
            if (IsEmailValid(emailRegister.text)) {
                PlayerPrefs.SetString("user_email", emailRegister.text);
            }
            PlayerPrefs.SetString("user_password", passwordRegister.text);
            
            //TODO creating user object then sending it to server and saving his parameters to PlayerPrefs
            User user = new User(IdGenerator.GenerateId(), username.text, emailRegister.text, passwordRegister.text);
            
        }
    }
    
    

    private void OnLoginClick() {
        if (string.IsNullOrEmpty(emailLogin.text) && string.IsNullOrEmpty(passwordLogin.text)) {
            if (IsEmailValid(emailLogin.text)) {
                PlayerPrefs.SetString("user_email", emailLogin.text);
            }
            PlayerPrefs.SetString("user_password", passwordLogin.text);
        }
    }
    
    private bool IsEmailValid(string emailaddress) {
        try {
            MailAddress m = new MailAddress(emailaddress);
            return true;
        }
        catch (FormatException) {
            return false;
        }
    }
}