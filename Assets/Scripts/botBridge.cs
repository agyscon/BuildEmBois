using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class botBridge : MonoBehaviour {

    public GameObject self;
    public GameObject parent;
    PoweredObjectController controller;
    bool inCollider = false;
    InventoryScript player;


    // Use this for initialization
    void Start () {
        self.SetActive(false);
        controller = parent.GetComponent<PoweredObjectController>();
	}

    private void Update() {
        if (inCollider)
        {
            if (Input.GetKeyDown("b") && player != null && player.getBots() > 0)
            {
                self.SetActive(true);
                player.LoseBot();
                controller.powerCheck();

                // TODO: Bot animation here
            }
            if (Input.GetKeyDown("v") && self.activeSelf)
            {
                self.SetActive(false);
                player.AddBot();
                controller.powerCheck();
            }
        }
    }

    // Called when collider is entered and button is pressed
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            player = other.gameObject.GetComponent<InventoryScript>();
            if (player != null)
            {
                inCollider = true;
            }
        }
    }

    // Resets the space to be ready for next enter
    private void OnTriggerExit(Collider other)
    {
        player = null;
        inCollider = false;
    }



}
