using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadContainer : MonoBehaviour {

    public ArrayList botsList = new ArrayList();

    // Use this for initialization
    public void addBots(ArrayList bots)
    {
        botsList.Clear();
        for (int i = 0; i < bots.Count; i++)
        {
            botsList.Add(bots[i]);

        }
        foreach(int bot in botsList)
        {
            print(bot);
        }
    }
    public ArrayList GetBotsList()
    {
        ArrayList holder = new ArrayList();
        foreach(int i in botsList)
        {
            holder.Add(i);
        }
        //print(holder.Count);
        return holder;
    }
}
