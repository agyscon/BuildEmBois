using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PunchPrompt : MonoBehaviour {

    public bool inCollider = false;
    public WorldOverlayScript overlayScript;

	private void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            BotCollector player = c.gameObject.GetComponent<BotCollector>();
            if (player != null)
            {
                
                inCollider = true;
                overlayScript.setActivePunchPrompt(true);

            }

        }
    }

    private void OnTriggerExit(Collider c)
    {
        if (c.gameObject.CompareTag("Player"))
        {
            inCollider = false;
            overlayScript.setActivePunchPrompt(false);
        }
    }
}
