using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

    private bool paused;
    private CanvasGroup canvasGroup;

    void Awake() {
        Time.timeScale = 1f;
        paused = false;
        Cursor.visible = false;
        canvasGroup = GetComponent<CanvasGroup>();
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!paused) {
                PauseMethod();
            } else {
                UnpauseMethod();
            }
        }
    }

    public void PauseMethod() {
        Time.timeScale = 0f;
        canvasGroup.alpha = 1;
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        paused = true;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void UnpauseMethod() {
        Time.timeScale = 1f;
        canvasGroup.alpha = 0;
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        paused = false;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;
    }

}
