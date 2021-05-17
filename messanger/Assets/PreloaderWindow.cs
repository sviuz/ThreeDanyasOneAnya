using UnityEngine.UI;
using UnityEngine;

public class PreloaderWindow : MonoBehaviour
{
    [SerializeField] private Button LoginButton;
    [SerializeField] private Button RegisterButton;
    [SerializeField] private GameObject LoginPanel;
    [SerializeField] private GameObject RegisterPanel;
    [SerializeField] private Image LoginImage;
    [SerializeField] private Image RegisterImage;
    private bool _isLogin = true;
    
    void Start(){
        LoginButton.onClick.AddListener(ActivateWindow);
        RegisterButton.onClick.AddListener(ActivateWindow);
    }

    private void ActivateWindow(){
        if (_isLogin){
            var img = LoginImage.color;
            RegisterImage.color = LoginImage.color;
            img.a = 1f;
            LoginImage.color = img;

            RegisterPanel.SetActive(false);
            LoginPanel.SetActive(true);
            _isLogin = false;
        }
        else{
            var img = RegisterImage.color;
            LoginImage.color = RegisterImage.color;
            img.a = 1f;
            RegisterImage.color = img;

            RegisterPanel.SetActive(true);
            LoginPanel.SetActive(false);
            _isLogin = true;
        }
    }
}
