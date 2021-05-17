using System;
using System.Net.Mail;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UserInfo;

public class LogRegWindow : MonoBehaviour {
    [Header("Login Section")]
    [SerializeField] private Text emailLogin;
    [SerializeField] private Text passwordLogin;
    [SerializeField] private Button Login;

    [Header("Registration Section")]
    [SerializeField] private Text username;
    [SerializeField] private Text emailRegister;
    [SerializeField] private Text passwordRegister;
    [SerializeField] private Button Register;

    [Space] [SerializeField] private Text ErrorText;

    public static Action OnErrorsClean;
    
    void Start() {
        OnErrorsClean += () => { ErrorText.text = ""; };
        Login.onClick.AddListener(OnLoginClick);
        Register.onClick.AddListener(OnRegisterClick);
    }
    
    

    private void OnRegisterClick() {
        OnErrorsClean?.Invoke();
        if (string.IsNullOrEmpty(username.text) && string.IsNullOrEmpty(emailRegister.text) && string.IsNullOrEmpty(passwordRegister.text)) {
            if (IsEmailValid(emailRegister.text)) {
                PlayerPrefs.SetString("user_email", emailRegister.text);
            }
            PlayerPrefs.SetString("user_password", passwordRegister.text);
            
            //TODO creating user object then sending it to server and saving his parameters to PlayerPrefs
            //User user = new User(IdGenerator.GenerateId(), username.text, emailRegister.text, passwordRegister.text);
        }
    }

    private void OnLoginClick() {
        OnErrorsClean?.Invoke();
        if (string.IsNullOrEmpty(emailLogin.text) && string.IsNullOrEmpty(passwordLogin.text)) {
            if (IsEmailValid(emailLogin.text)) {
                PlayerPrefs.SetString("user_email", emailLogin.text);
            }
            PlayerPrefs.SetString("user_password", passwordLogin.text);
        }else {
            ErrorText.text = "Yiu have some errors. Fix 'em.";
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