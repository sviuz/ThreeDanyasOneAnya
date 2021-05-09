using UnityEngine;
using UnityEngine.SceneManagement;

namespace WindowsManager {
    public class Preloader : MonoBehaviour
    {
        private void Awake() {
            if (PlayerPrefs.HasKey("UserIsInSystem")) {
                SceneManager.LoadScene("Chatter");
            }
            else {
                SceneManager.LoadScene("Scenes/TestNewUI");
            }
        }
    }
}
