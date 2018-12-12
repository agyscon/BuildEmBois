using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartTrigger : MonoBehaviour {

    [SerializeField] CanvasGroup blackScreen;

	void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            StartCoroutine(RestartCoroutine());
        }
    }
    
    private IEnumerator RestartCoroutine() {
        if (blackScreen != null) {
            blackScreen.blocksRaycasts = true;
            for (float i = 0; i < 0.25f; i += Time.unscaledDeltaTime) {
                blackScreen.alpha = Mathf.SmoothStep(0, 1, i / 0.25f);
                yield return null;
            }
        }
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

}
