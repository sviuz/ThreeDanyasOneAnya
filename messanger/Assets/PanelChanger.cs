using WindowsManager;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.Serialization;

public class PanelChanger : MonoBehaviour
{
    [FormerlySerializedAs("LoginTopButton")] [SerializeField] private Button loginTopButton;
    [FormerlySerializedAs("RegisterTopButton")] [SerializeField] private Button registerTopButton;
    [FormerlySerializedAs("LoginPanel")] [SerializeField] private GameObject loginPanel;
    [FormerlySerializedAs("RegisterPanel")] [SerializeField] private GameObject registerPanel;
    [SerializeField] private GameObject colorPalette;
    [SerializeField] private Button colorButton;
    [SerializeField] private Button ColorPicker;

    private bool _isLogin = true;
    
    void Start(){
        loginTopButton.onClick.AddListener(ShowLoginPanel);
        registerTopButton.onClick.AddListener(ShowRegisterPanel);
        colorButton.onClick.AddListener(SelectColor);
        ColorPicker.onClick.AddListener(PickAColor);
    }

    private void ShowLoginPanel() {
        AuthWindow.OnErrorsClean?.Invoke();
        loginPanel.SetActive(true);
        registerPanel.SetActive(false);
    }

    private void PickAColor() {
        colorPalette.SetActive(false);
    }

    private void SelectColor() {
        colorPalette.SetActive(true);
    }

    private void ShowRegisterPanel() {
        AuthWindow.OnErrorsClean?.Invoke();
        loginPanel.SetActive(false);
        registerPanel.SetActive(true);
    }
}
