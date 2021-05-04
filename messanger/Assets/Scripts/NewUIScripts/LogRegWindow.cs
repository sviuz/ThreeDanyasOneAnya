using System;
using System.Net.Mail;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UserInfo;

public class LogRegWindow : MonoBehaviour {
    [Header("Login Section")] [SerializeField]
    private Button Login;

    [Space] [SerializeField] private Text emailLogin;
    [SerializeField] private Text passwordLogin;

    [Header("Registr ation Section")] [SerializeField]
    private Button Register;

    [Space] [SerializeField] private Text emailRegister;
    [SerializeField] private Text passwordRegister;

    void Start() {
        Login.onClick.AddListener(OnLoginClick);
    }

    private void OnRegisterClick() {
        
    }

    private void OnLoginClick() {
        if (string.IsNullOrEmpty(emailLogin.text) && string.IsNullOrEmpty(passwordLogin.text)) {
            if (IsValid(emailLogin.text)) {
                PlayerPrefs.SetString("user_email", emailLogin.text);
            }
            PlayerPrefs.SetString("user_password", passwordLogin.text);
            
        }
    }

    /*public void OnUserCreate() {
        User user = new User();
        
    }*/
    
    private bool IsValid(string emailaddress) {
        try {
            MailAddress m = new MailAddress(emailaddress);
            return true;
        }
        catch (FormatException) {
            return false;
        }
    }
}