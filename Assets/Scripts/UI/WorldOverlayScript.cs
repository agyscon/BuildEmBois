using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldOverlayScript : MonoBehaviour {
    public Text numBotsText;
    public Text buttonPrompt;
    public Text neededBots;
    public Text punchPrompt;
    // Use this for initialization
    public int requiredNumber;


	void Start () {
	}

    public void setNumBots(int n)
    {
        numBotsText.text = n + "/" + requiredNumber;
    }

    public void setActiveButtonPrompt(bool isActive, int requiredBots)
    {
        neededBots.enabled = isActive;
        neededBots.text = "x" + requiredBots;
        buttonPrompt.enabled = isActive;
    }

    public void setActivePunchPrompt(bool isActive)
    {
        punchPrompt.enabled = isActive;
    }


}
