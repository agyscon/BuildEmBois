using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour {
    public int numBots;
    public GameObject[] bots;
    public int current;


	// Use this for initialization
	void Start () {
        current = 0;
        bots = new GameObject[7];
	}

    public void AddBot(GameObject newBot)
    {
        bots[current] = newBot;
        current++;
    }


}
