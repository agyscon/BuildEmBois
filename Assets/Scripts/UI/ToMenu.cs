﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToMenu : MonoBehaviour {

    [SerializeField] CanvasGroup blackScreen;

    // This method takes whatever was specified fr the 
    // string name to be the next scene to switch to
    public void MenuButtonPressed()
    {
        StartCoroutine(LoadMainMenu());
    }

    private IEnumerator LoadMainMenu() {
        if (blackScreen != null) {
            blackScreen.blocksRaycasts = true;
            for (float i = 0; i < 1f; i += Time.unscaledDeltaTime) {
                blackScreen.alpha = Mathf.SmoothStep(0, 1, i);
                yield return null;
            }
        }
        SceneManager.LoadScene("Main Menu");
    }
}
