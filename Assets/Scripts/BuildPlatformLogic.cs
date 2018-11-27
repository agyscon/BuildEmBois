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

    // Use this for initialization
    void Start()
    {
        playerMask = LayerMask.GetMask("Player");
        layerNum = LayerMask.NameToLayer("Player");
        transform.gameObject.GetComponentInChildren<Light>().enabled = false;
        buildObject.SetActive(false);
        overlayScript = overlay.GetComponent<WorldOverlayScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inCollider)
        {
            if (Input.GetKeyDown("b") && player != null && player.getBots() >= botsNeeded)
            {
                buildObject.SetActive(true);
                ArrayList botsUsed = player.LoseBots(botsNeeded);
                transform.parent.gameObject.GetComponent<PadContainer>().addBots(botsUsed);
            }
            if (Input.GetKeyDown("v") && buildObject.activeSelf)
            {
                buildObject.SetActive(false);
                //player.ReceiveBots(botsNeeded);
                print("reached");
                ArrayList botIndices = transform.parent.GetComponent<PadContainer>().GetBotsList();
                foreach (int bot in botIndices)
                {
                    print(bot);
                }
                player.RegainBots(botsNeeded, botIndices);
            }
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
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject.GetComponent<BotCollector>();
            if (player != null)
            {
                transform.gameObject.GetComponentInChildren<Light>().enabled = true;
                inCollider = true;
                overlayScript.setActiveButtonPrompt(true, botsNeeded);
                
            }
            
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == layerNum)
        {
            player = null;
            transform.gameObject.GetComponentInChildren<Light>().enabled = false;
            inCollider = false;
            overlayScript.setActiveButtonPrompt(false, botsNeeded);
        }
    }

}
