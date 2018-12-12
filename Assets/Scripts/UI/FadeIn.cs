using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour {

    [SerializeField] CanvasGroup blackScreen;

    void Start() {
        StartCoroutine(FadeInCoroutine());
    }

    private IEnumerator FadeInCoroutine() {
        if (blackScreen != null) {
            blackScreen.blocksRaycasts = false;
            blackScreen.interactable = false;
            for (float i = 0f; i < 1f; i += Time.unscaledDeltaTime) {
                blackScreen.alpha = Mathf.SmoothStep(1f, 0f, i);
                yield return null;
            }
            blackScreen.alpha = 0;
        }
    }
}
