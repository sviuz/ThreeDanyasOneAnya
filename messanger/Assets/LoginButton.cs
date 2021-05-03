using UnityEngine;
using UnityEngine.UI;

public class LoginPanel : MonoBehaviour {
    [SerializeField] private Text email;
    [SerializeField] private Text password;
    [SerializeField] private Button LoginButton;

    void Start() {
        LoginButton.onClick.AddListener(OnLoginEnter);
    }

    private void OnLoginEnter() {
        PlayerPrefs.SetString("user_email", email.text);
        PlayerPrefs.SetString("user_password", password.text);
        //SceneManager.LoadScene("Menu");
    }
}