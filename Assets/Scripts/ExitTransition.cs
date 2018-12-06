using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitTransition : MonoBehaviour {

    public int numBots = 7;
    
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
                SceneManager.LoadScene("Congratulations");
            } else {
                TipPanel.GetInstance().SetText("You're missing bots.");
                TipPanel.GetInstance().SetVisible(true);
            }
        }
    }
}
