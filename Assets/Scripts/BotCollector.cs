﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotCollector : MonoBehaviour {

    public int bots;

    private WorldOverlayScript overlayScript;
    public GameObject overlay;
    public List<GameObject> totalBots = new List<GameObject>();

    // Use this for initialization
    void Start () {
        bots = 0;
        overlayScript = overlay.GetComponent<WorldOverlayScript>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ReceiveBots(int num, GameObject bot)
    {
        totalBots.Add(bot);
        bots += num;
        overlayScript.setNumBots(bots);
    }

    public void RegainBots(int num, ArrayList botList)
    {
        //foreach (int bot in botList)
        //{
        //    print(bot);
        //}
        for (int i = 0; i < botList.Count; i++)
        {
            GameObject usedBot = totalBots[(int)botList[i]];
            usedBot.SetActive(true);
            usedBot.GetComponent<BotMovement>().SetState(BotMovement.BotMode.Follow);

        }
        bots += num;
        overlayScript.setNumBots(bots);
        
    }

    public ArrayList LoseBots(int num)
    {
        ArrayList botsUsed = new ArrayList();
        int numToUse = num;
        for (int i = 0; i < totalBots.Count; i++)
        {
            GameObject bot = totalBots[i];
            BotMovement botScript = bot.GetComponent<BotMovement>();
            if (botScript.GetState() == BotMovement.BotMode.Follow)
            {
                botScript.SetState(BotMovement.BotMode.Build);
                numToUse--;
                botsUsed.Add(i);
                if (numToUse == 0)
                {
                    break;
                }
            }
        }
        bots -= num;
        overlayScript.setNumBots(bots);
        return botsUsed;
    }

    public int getBots()
    {
        return bots;
    }
}
