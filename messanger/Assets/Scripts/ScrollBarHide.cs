using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ScrollBarHide : MonoBehaviour {
    [FormerlySerializedAs("SCrollLine")] public GameObject ScrollLine;
    public ScrollRect scroll;

    private void LateUpdate() {
        ScrollLine.SetActive(Mathf.Abs(scroll.velocity.y) > 10 &&
                             scroll.content.sizeDelta.y > scroll.viewport.sizeDelta.y);
    }
}