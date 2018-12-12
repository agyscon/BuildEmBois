using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitionButton : MonoBehaviour {

    public string nextSceneName; 

    [SerializeField] CanvasGroup blackScreen;
    
    // This method takes whatever was specified fr the 
    // string name to be the next scene to switch to
	public void ButtonPressed()
    {
        StartCoroutine(TransitionCoroutine());
    }

    private IEnumerator TransitionCoroutine() {
        if (blackScreen != null) {
            blackScreen.blocksRaycasts = true;
            Time.timeScale = 1;
            for (float i = 0; i < 1f; i += Time.unscaledDeltaTime) {
                blackScreen.alpha = Mathf.SmoothStep(0f, 1f, i);
                yield return null;
            }
        }
        SceneManager.LoadScene(nextSceneName);
    }

}
