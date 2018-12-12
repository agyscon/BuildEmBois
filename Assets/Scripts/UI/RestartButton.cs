using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour {

    [SerializeField] CanvasGroup blackScreen;

	public void RestartButtonPressed()
    {
        StartCoroutine(RestartCoroutine());
    }

    private IEnumerator RestartCoroutine() {
        if (blackScreen != null) {
            blackScreen.blocksRaycasts = true;
            for (float i = 0; i < 1f; i += Time.unscaledDeltaTime) {
                blackScreen.alpha = Mathf.SmoothStep(0, 1, i);
                yield return null;
            }
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
