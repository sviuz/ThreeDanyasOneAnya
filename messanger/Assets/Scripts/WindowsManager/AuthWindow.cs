using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace WindowsManager {
    public class AuthWindow : MonoBehaviour {
        [Header("Login Section")] 
        [SerializeField] private Text emailLogin;
        [SerializeField] private Text passwordLogin;
        [SerializeField] private Button Login;

        [Header("Registration Section")] 
        [SerializeField] private Text username;
        [SerializeField] private Text emailRegister;
        [SerializeField] private Text passwordRegister;
        [FormerlySerializedAs("Register")] [SerializeField] private Button register;
        [SerializeField] private Color iconColor;
        [Space] [SerializeField] private Text ErrorText;

        public static Action OnErrorsClean;

        void Start() {
            OnErrorsClean += () => { ErrorText.text = ""; };
            Login.onClick.AddListener(OnLoginClick);
            register.onClick.AddListener(OnRegisterClick);
        }

        private void OnRegisterClick() {
            OnErrorsClean?.Invoke();
            if (string.IsNullOrEmpty(username.text) && IsValidEmail(emailRegister.text) && string.IsNullOrEmpty(emailRegister.text) &&
                string.IsNullOrEmpty(passwordRegister.text)) {
                PlayerPrefs.SetString("username", username.text);
                PlayerPrefs.SetString("userEmail", emailRegister.text);
                PlayerPrefs.SetString("userPassword", passwordRegister.text);
                PlayerPrefs.SetString("iconColor", iconColor.ToString());
                //TODO добавить проверку на наличие на сервере данного пользователя
                SceneManager.LoadScene("Chatter");
                //TODO При успешном создании аккаунта, данные пользователя отсылаются на сервер
                //User user = new User(IdGenerator.GenerateId(), username.text, emailRegister.text, passwordRegister.text);
            }
            else {
                ErrorText.text = "You have some errors. Fix 'em.";
            }
        }

        private void OnLoginClick() {
            OnErrorsClean?.Invoke();
            if (string.IsNullOrEmpty(emailLogin.text) && string.IsNullOrEmpty(passwordLogin.text)) {
                PlayerPrefs.SetString("userEmail", emailLogin.text);
                PlayerPrefs.SetString("userPassword", passwordLogin.text);
                //TODO добавить проверку на сервере на наличие данного пользователя
                SceneManager.LoadScene("Chatter");
            }
            else {
                ErrorText.text = "You have some errors. Fix 'em.";
            }
        }
        
        bool IsValidEmail(string email)
        {
            try {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch {
                return false;
            }
        }
    }
}