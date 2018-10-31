using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotCollector : MonoBehaviour {

    public int bots;

	// Use this for initialization
	void Start () {
        bots = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ReceiveBots(int num)
    {
        bots += num;
    }

    public void LoseBots(int num)
    {
        bots -= num;
    }

    public int getBots()
    {
        return bots;
    }
}
