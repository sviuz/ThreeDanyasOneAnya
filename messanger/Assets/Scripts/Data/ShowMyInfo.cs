using UnityEngine;
using UnityEngine.UI;

namespace Data {
    public class ShowMyInfo : MonoBehaviour {
        [SerializeField] private Text myUsername;
        [SerializeField] private Text iconText;
        [SerializeField] private Image myIconColor;

        private void Start() {
            myUsername.text = PlayerPrefs.GetString("username");
            ColorUtility.TryParseHtmlString(PlayerPrefs.GetString("iconColor"), out var hexWithNumberSignFromColor);
            myIconColor.color = hexWithNumberSignFromColor;
            iconText.text = myUsername.text[0].ToString();
        }
    }
}
