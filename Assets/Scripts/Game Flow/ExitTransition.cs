using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTransition : MonoBehaviour {

    public int numBots = 7;
    public string nextSceneName;

    [SerializeField] private CanvasGroup blackScreen;

    void OnTriggerEnter(Collider c) {
        ExitConditions(c);
    }
    
    void OnTriggerStay(Collider c) {
        ExitConditions(c);
    }

    void OnTriggerExit(Collider c) {
        if (c.tag.Equals("Player")) {
            TipPanel.GetInstance().SetVisible(false);
        }
    }

    void ExitConditions(Collider c) {
        if (c.tag.Equals("Player")) {
            BotCollector playerInventory = c.GetComponent<BotCollector>();
            if (playerInventory.getBots() == numBots) {
            } else {
                TipPanel.GetInstance().SetText("You're missing bots.");
                TipPanel.GetInstance().SetVisible(true);
            }
        }
    }

    IEnumerator ExitScene() {
        if (blackScreen != null) {
            blackScreen.blocksRaycasts = true;
            for (float i = 0; i < 1f; i += Time.unscaledDeltaTime) {
                blackScreen.alpha = Mathf.SmoothStep(0, 1, i);
                yield return null;
            }
        }
        SceneManager.LoadScene(nextSceneName);
    }
}
