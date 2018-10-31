using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableBot : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {
        if (other.attachedRigidbody != null)
        {
            BotCollector bc = other.attachedRigidbody.gameObject.GetComponent<BotCollector>();
            if (bc != null)
            {
                //Destroy(this.gameObject);
                bc.ReceiveBots(1, new GameObject());
            }
        }
    }
}
