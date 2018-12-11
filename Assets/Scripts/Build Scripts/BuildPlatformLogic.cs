using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPlatformLogic : MonoBehaviour
{
    LayerMask playerMask;
    int layerNum;
    bool inCollider = false;
    public GameObject buildObject;
    public int botsNeeded = 1;
    BotCollector player;
    public GameObject overlay;
    private WorldOverlayScript overlayScript;
    private PadContainer container;
    private Light padLight;
    public bool isSwitch = false;
    public GameObject bot;
    public GameObject[] wireWithotWithoutCurrent;
    public AudioClip switchSound;
    public AudioClip buildSound;

    // Use this for initialization
    void Start()
    {
        playerMask = LayerMask.GetMask("Player");
        layerNum = LayerMask.NameToLayer("Player");
        buildObject.SetActive(false);
        overlayScript = overlay.GetComponent<WorldOverlayScript>();
        container = transform.parent.gameObject.GetComponent<PadContainer>();
        padLight = transform.GetComponentInChildren<Light>();
        if (isSwitch)
        {
            bot.SetActive(false);
        }
        if (padLight != null)
        {
            padLight.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (inCollider)
        {
            if (Input.GetKeyDown("b") && player != null && player.getBots() >= botsNeeded && !container.getIsBuilt())
            {
                buildObject.SetActive(true);
                ArrayList botsUsed = player.LoseBots(botsNeeded);
                container.addBots(botsUsed);
                container.setIsBuilt(true);
                if (isSwitch)
                {
                    Material current = Resources.Load("Current") as Material;
                    foreach(GameObject wire in wireWithotWithoutCurrent)
                    {
                        wire.GetComponent<MeshRenderer>().material = current;
                    }
                    bot.SetActive(true);
                    Vector3 buildPos = transform.position;
                    AudioSource.PlayClipAtPoint(switchSound, buildPos);
                } else
                {
                    Vector3 buildPos = transform.position;
                    AudioSource.PlayClipAtPoint(buildSound, buildPos);
                }

            }
            if (Input.GetKeyDown("v") && buildObject.activeSelf)
            {
                deactivatePlatform(player);
                deactivateSwitch(player);
            }
        }

    }

    public void deactivateSwitch(BotCollector player)
    {
        if (isSwitch)
        {
            Material noCurrent = Resources.Load("NoCurrent") as Material;
            foreach (GameObject wire in wireWithotWithoutCurrent)
            {
                wire.GetComponent<MeshRenderer>().material = noCurrent;
            }
            bot.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other) {
        Activate(other);
    }

    private void OnTriggerStay(Collider other) {
        Activate(other);
    }

    private void Activate(Collider other)
    {
        //print(other.gameObject.name + ": " + other.gameObject.tag);
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject.GetComponent<BotCollector>();
            if (player != null)
            {
                if (padLight != null)
                {
                    padLight.enabled = true;
                }
                inCollider = true;
                overlayScript.setActiveButtonPrompt(true, botsNeeded);
                
            }
            
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = null;
            if (padLight != null)
            {
                padLight.enabled = false;
            }
            inCollider = false;
            overlayScript.setActiveButtonPrompt(false, botsNeeded);
        }
    }

    public void deactivatePlatform(BotCollector playerBotCollector)
    {
        container.setIsBuilt(false);
        buildObject.SetActive(false);
        //player.ReceiveBots(botsNeeded);
        print("reached");
        ArrayList botIndices = container.GetBotsList();
        foreach (int bot in botIndices)
        {
            print(bot);
        }
        playerBotCollector.RegainBots(botIndices.Count, botIndices, transform.position);
    }

}
