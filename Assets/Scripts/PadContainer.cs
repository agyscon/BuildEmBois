using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadContainer : MonoBehaviour {
    List<GameObject> botsList = new List<GameObject>();

	// Use this for initialization
    public void addBots(GameObject bot)
    {
        botsList.Add(bot);
    }
    public List<GameObject> GetBotsList()
    {
        botsList.Clear();
        return botsList;
    }
}
