using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPlatformLogic : MonoBehaviour {
    LayerMask playerMask;
    int layerNum;
    bool inCollider = false;
    public GameObject buildObject = null;

	// Use this for initialization
	void Start () {
        playerMask = LayerMask.GetMask("Player");
        layerNum = LayerMask.NameToLayer("Player");
        transform.gameObject.GetComponentInChildren<Light>().enabled = false;
        if (buildObject != null)
        {
            buildObject.SetActive(false);
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        if (inCollider)
        {
            if (Input.GetKeyDown("b"))
            {
                buildObject.SetActive(true);
            }
            if (Input.GetKeyDown("v"))
            {
                buildObject.SetActive(false);
            }
        }
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == layerNum)
        {
            transform.gameObject.GetComponentInChildren<Light>().enabled = true;
            inCollider = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == layerNum)
        {
            transform.gameObject.GetComponentInChildren<Light>().enabled = false;
            inCollider = false;
        }
    }

}
