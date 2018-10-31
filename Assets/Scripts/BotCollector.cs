using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotCollector : MonoBehaviour {

    public int bots;

    private WorldOverlayScript overlayScript;
    public GameObject overlay;

    // Use this for initialization
    void Start () {
        bots = 0;
        overlayScript = overlay.GetComponent<WorldOverlayScript>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ReceiveBots(int num)
    {
        bots += num;
        overlayScript.setNumBots(bots);
    }

    public void LoseBots(int num)
    {
        bots -= num;
        overlayScript.setNumBots(bots);
    }

    public int getBots()
    {
        return bots;
    }
}
