using UnityEngine.UI;
using UnityEngine;

public class PanelChanger : MonoBehaviour
{
    [SerializeField] private Button LoginTopButton;
    [SerializeField] private Button RegisterTopButton;
    [SerializeField] private GameObject LoginPanel;
    [SerializeField] private GameObject RegisterPanel;
    [SerializeField] private Image LoginColor;
    [SerializeField] private Image RegisterColor;
    private bool _isLogin = true;
    
    void Start(){
        LoginTopButton.onClick.AddListener(ShowLoginPanel);
        RegisterTopButton.onClick.AddListener(ShowRegisterPanel);
    }

    private void ShowLoginPanel() {
        LogRegWindow.OnErrorsClean?.Invoke();
        LoginPanel.SetActive(true);
        RegisterPanel.SetActive(false);
    }

    private void ShowRegisterPanel() {
        LogRegWindow.OnErrorsClean?.Invoke();
        LoginPanel.SetActive(false);
        RegisterPanel.SetActive(true);
    }
}
