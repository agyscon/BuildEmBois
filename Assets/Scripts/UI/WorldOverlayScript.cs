using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldOverlayScript : MonoBehaviour {
    public Text numBotsText;
    public Text buttonPrompt;
    public Text neededBots;
	// Use this for initialization
	void Start () {
	}

    public void setNumBots(int n)
    {
        numBotsText.text = n + "";
    }

    public void setActiveButtonPrompt(bool isActive, int requiredBots)
    {
        neededBots.enabled = isActive;
        neededBots.text = "x" + requiredBots;
        buttonPrompt.enabled = isActive;
    }


}
