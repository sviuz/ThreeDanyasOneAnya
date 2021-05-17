using System;
using System.Net.Mail;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UserInfo;

public class LogRegWindow : MonoBehaviour {
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
    [Header("Login Section")]
    [SerializeField] private Text emailLogin;
=======
    [Header("Login Section")] [SerializeField]
    private Button LoginButton;

    [Space] [SerializeField] private Text emailLogin;
>>>>>>> parent of 7526548 (Added Error text. Fixed moving from panel to panel.)
    [SerializeField] private Text passwordLogin;

<<<<<<< HEAD
    [Header("Registration Section")]
    [SerializeField] private Text username;
=======
=======
>>>>>>> parent of f5365c4 (Added new plugin)
=======
>>>>>>> parent of f5365c4 (Added new plugin)
    [Header("Login Section")] [SerializeField]
    private Text emailLogin;

    [SerializeField] private Text passwordLogin;
    [SerializeField] private Button Login;

    [Header("Registration Section")] [SerializeField]
    private Text username;

<<<<<<< HEAD
<<<<<<< HEAD
>>>>>>> parent of f5365c4 (Added new plugin)
=======
    [Header("Registr ation Section")] [SerializeField]
    private Button RegisterButton;

    [Space] [SerializeField] private Text username;
>>>>>>> parent of 7526548 (Added Error text. Fixed moving from panel to panel.)
=======
    [Header("Login Section")]
    [SerializeField] private Text emailLogin;
    [SerializeField] private Text passwordLogin;
    [SerializeField] private Button Login;

    [Header("Registration Section")]
    [SerializeField] private Text username;
>>>>>>> parent of 855cadb (Added some view in main scene)
    [SerializeField] private Text emailRegister;
    [SerializeField] private Text passwordRegister;

<<<<<<< HEAD
    public static Action OnErrorsClean;
<<<<<<< HEAD
<<<<<<< HEAD
    
=======

>>>>>>> parent of f5365c4 (Added new plugin)
=======
>>>>>>> parent of 7526548 (Added Error text. Fixed moving from panel to panel.)
=======
    
>>>>>>> parent of 855cadb (Added some view in main scene)
    void Start() {
        LoginButton.onClick.AddListener(OnLoginClick);
        RegisterButton.onClick.AddListener(OnRegisterClick);
    }
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
    
    
=======
>>>>>>> parent of 7526548 (Added Error text. Fixed moving from panel to panel.)

    private void OnRegisterClick() {
        if (string.IsNullOrEmpty(username.text) && string.IsNullOrEmpty(emailRegister.text) && string.IsNullOrEmpty(passwordRegister.text)) {
            if (IsEmailValid(emailRegister.text)) {
                PlayerPrefs.SetString("user_email", emailRegister.text);
            }
            PlayerPrefs.SetString("user_password", passwordRegister.text);
            
            //TODO creating user object then sending it to server and saving his parameters to PlayerPrefs
            User user = new User(IdGenerator.GenerateId(), username.text, emailRegister.text, passwordRegister.text);
            
        }
=======

=======
    
    
>>>>>>> parent of 855cadb (Added some view in main scene)

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
<<<<<<< HEAD
        else {
            ErrorText.text = "Yiu have some errors. Fix 'em.";
        }
>>>>>>> parent of f5365c4 (Added new plugin)
=======
>>>>>>> parent of 855cadb (Added some view in main scene)
    }
    
    

    private void OnLoginClick() {
        if (string.IsNullOrEmpty(emailLogin.text) && string.IsNullOrEmpty(passwordLogin.text)) {
<<<<<<< HEAD
<<<<<<< HEAD
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
=======
            PlayerPrefs.SetString("user_email", emailLogin.text);
=======
            if (IsEmailValid(emailLogin.text)) {
                PlayerPrefs.SetString("user_email", emailLogin.text);
            }
>>>>>>> parent of 855cadb (Added some view in main scene)
            PlayerPrefs.SetString("user_password", passwordLogin.text);
        }else {
            ErrorText.text = "Yiu have some errors. Fix 'em.";
>>>>>>> parent of f5365c4 (Added new plugin)
        }
    }
    
    private bool IsEmailValid(string emailaddress) {
        try {
            MailAddress m = new MailAddress(emailaddress);
            return true;
        }
        catch (FormatException) {
            return false;
=======
=======
>>>>>>> parent of f5365c4 (Added new plugin)
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
        if (string.IsNullOrEmpty(username.text) && string.IsNullOrEmpty(emailRegister.text) &&
            string.IsNullOrEmpty(passwordRegister.text)) {
            PlayerPrefs.SetString("user_email", emailRegister.text);
            PlayerPrefs.SetString("user_password", passwordRegister.text);

            SceneManager.LoadScene("Chatter");
            //TODO creating user object then sending it to server and saving his parameters to PlayerPrefs
            //User user = new User(IdGenerator.GenerateId(), username.text, emailRegister.text, passwordRegister.text);
        }
        else {
            ErrorText.text = "Yiu have some errors. Fix 'em.";
        }
    }

    private void OnLoginClick() {
        OnErrorsClean?.Invoke();
        if (string.IsNullOrEmpty(emailLogin.text) && string.IsNullOrEmpty(passwordLogin.text)) {
            PlayerPrefs.SetString("user_email", emailLogin.text);
            PlayerPrefs.SetString("user_password", passwordLogin.text);
            SceneManager.LoadScene("Chatter");
        }
        else {
            ErrorText.text = "Yiu have some errors. Fix 'em.";
<<<<<<< HEAD
>>>>>>> parent of f5365c4 (Added new plugin)
=======
>>>>>>> parent of f5365c4 (Added new plugin)
        }
    }
}