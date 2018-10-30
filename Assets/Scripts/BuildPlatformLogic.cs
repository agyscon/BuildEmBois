﻿using System.Collections;
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

    // Use this for initialization
    void Start()
    {
        playerMask = LayerMask.GetMask("Player");
        layerNum = LayerMask.NameToLayer("Player");
        transform.gameObject.GetComponentInChildren<Light>().enabled = false;
        buildObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (inCollider)
        {
            if (Input.GetKeyDown("b") && player.bots >= botsNeeded)
            {
                buildObject.SetActive(true);
                player.LoseBots(botsNeeded);
            }
            if (Input.GetKeyDown("v") && buildObject.activeSelf)
            {
                buildObject.SetActive(false);
                player.ReceiveBots(botsNeeded);
            }
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == layerNum)
        {
            player = other.gameObject.GetComponent<BotCollector>();
            transform.gameObject.GetComponentInChildren<Light>().enabled = true;
            inCollider = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == layerNum)
        {
            player = null;
            transform.gameObject.GetComponentInChildren<Light>().enabled = false;
            inCollider = false;
        }
    }

}
