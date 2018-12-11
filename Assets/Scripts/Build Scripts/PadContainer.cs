using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PadContainer : MonoBehaviour {

    public ArrayList botsList = new ArrayList();

    private bool isBuilt;
    private bool isBuilding;

    // Use this for initialization
    public void addBots(ArrayList bots)
    {
        botsList.Clear();
        for (int i = 0; i < bots.Count; i++)
        {
            botsList.Add(bots[i]);

        }
    }
    public ArrayList GetBotsList()
    {
        ArrayList holder = new ArrayList();
        foreach(int i in botsList)
        {
            holder.Add(i);
        }
        botsList.Clear();
        return holder;
    }

    public void setIsBuilt(bool built)
    {
        isBuilt = built;
    }

    public bool getIsBuilt()
    {
        return isBuilt;
    }

    public void setIsBuilding(bool building)
    {
        isBuilding = building;
    }

    public bool getIsBuilding()
    {
        return isBuilding;
    }
}
