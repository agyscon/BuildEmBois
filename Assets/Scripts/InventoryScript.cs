using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryScript : MonoBehaviour {

    public int numBots;

	// Use this for initialization
	void Start () {
        numBots = 0;
	}

    public void AddBot()
    {
        numBots++;
    }

    public void LoseBot()
    {
        numBots--;
    }

    public int getBots()
    {
        return numBots;
    }


}
