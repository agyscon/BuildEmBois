using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TipPanel : MonoBehaviour {

    private static TipPanel instance;
    private CanvasGroup canvasGroup;
    private Text text;

    public static TipPanel GetInstance() {
        return instance;
    }

    void Awake() {
        instance = this;
        canvasGroup = GetComponent<CanvasGroup>();
        text = GetComponentInChildren<Text>();
    }

    public void SetText(string s) {
        text.text = s;
    }

    public void SetVisible(bool visible) {
        if (visible) {
            canvasGroup.alpha = 1;
        } else {
            canvasGroup.alpha = 0;
        }
    }

}
