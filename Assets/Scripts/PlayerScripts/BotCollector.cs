using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotCollector : MonoBehaviour {

    public int bots;

    private WorldOverlayScript overlayScript;
    public GameObject overlay;
    public List<GameObject> totalBots = new List<GameObject>();
    public GameObject objectContainingPads;
    public AudioClip thankYou;

    // Use this for initialization
    void Start () {
        bots = 0;
        overlayScript = overlay.GetComponent<WorldOverlayScript>();
    }
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("r"))
        {
            foreach(Transform child in objectContainingPads.transform)
            {
                Transform actualPadObject = child.GetChild(0);
                BuildPlatformLogic platformScript = actualPadObject.GetComponent<BuildPlatformLogic>();
                platformScript.deactivatePlatform(this);
                platformScript.deactivateSwitch(this);
            }
        }
	}

    public void ReceiveBots(int num, GameObject bot)
    {
        AudioSource.PlayClipAtPoint(thankYou, bot.transform.position);
        totalBots.Add(bot);
        bots += num;
        overlayScript.setNumBots(bots);
    }

    public void RegainBots(int num, ArrayList botList, Vector3 buildPadPosition, bool isSwitch)
    {
        for (int i = 0; i < botList.Count; i++)
        {
            GameObject usedBot = totalBots[(int)botList[i]];
            BotMovement botScript = usedBot.GetComponent<BotMovement>();
            usedBot.SetActive(true);
            if (botScript != null) {
                if (!isSwitch) {
                    botScript.JumpToPad(buildPadPosition);
                } else {
                    botScript.ReactivateNavMeshAgent();
                }
                botScript.SetState(BotMovement.BotMode.Follow);
            }

        }
        bots += num;
        overlayScript.setNumBots(bots);
        
    }

    public ArrayList LoseBots(int num, Transform destination)
    {
        ArrayList botsUsed = new ArrayList();
        int numToUse = num;
        if (num > bots) {
            return null;
        }
        for (int i = 0; i < totalBots.Count; i++)
        {
            GameObject bot = totalBots[i];
            BotMovement botScript = bot.GetComponent<BotMovement>();
            if (botScript.GetState() == BotMovement.BotMode.Follow || botScript.GetState() == BotMovement.BotMode.Stopped)
            {
                botScript.SetDest(destination);
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
