using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
