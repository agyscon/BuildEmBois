using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweredObjectController : MonoBehaviour {

    public List<GameObject> lineBreaks = new List<GameObject>();
    public GameObject self;

	// Use this for initialization
	void Start () {
        self.SetActive(false);
	}

    // Called when a bot is placed or removed in order to check for line completion
    public void powerCheck() {

        bool allTrue = true;
        foreach (GameObject breaks in lineBreaks) {
            if (!breaks.GetComponent<botBridge>().self.activeSelf)
            {
                allTrue = false;
            }
        }

        if (allTrue)
        {
            self.SetActive(true);
        } else
        {
            self.SetActive(false);
        }
    }
}
