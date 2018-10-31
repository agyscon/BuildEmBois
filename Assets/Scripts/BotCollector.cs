using System.Collections;
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

    public void RegainBots(int num, List<GameObject> botList)
    {

        for (int i = 0; i < botList.Count; i++)
        {
            for (int j = 0; j < totalBots.Count; j++)
            {
                if (botList[i].Equals(totalBots[j]))
                {
                    totalBots[i].GetComponent<BotMovement>().SetState(BotMovement.BotMode.Follow);
                }

            }
        }
        bots += num;
        overlayScript.setNumBots(bots);
    }

    public void LoseBots(int num)
    {
        int numToUse = num;
        for (int i = 0; i < totalBots.Count; i++)
        {
            GameObject bot = totalBots[i];
            BotMovement botScript = bot.GetComponent<BotMovement>();
            if (botScript.GetState() == BotMovement.BotMode.Follow)
            {
                botScript.SetState(BotMovement.BotMode.Build);
                numToUse--;
                if (numToUse == 0)
                {
                    break;
                }
            }
        }
        bots -= num;
        overlayScript.setNumBots(bots);
    }

    public int getBots()
    {
        return bots;
    }
}
