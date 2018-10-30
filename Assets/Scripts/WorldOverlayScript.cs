using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WorldOverlayScript : MonoBehaviour {
    private Text numBotsText;
	// Use this for initialization
	void Start () {
        numBotsText = GetComponentInChildren<Text>();
	}

    void setNumBots(int n)
    {
        numBotsText.text = "x" + n;
    }


}
