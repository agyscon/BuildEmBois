using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildPlatformLogic : MonoBehaviour {
    LayerMask playerMask;
    int layerNum;
    bool inCollider = false;
    public GameObject buildObject;

	// Use this for initialization
	void Start () {
        playerMask = LayerMask.GetMask("Player");
        layerNum = LayerMask.NameToLayer("Player");
        transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
        transform.gameObject.GetComponentInChildren<Light>().enabled = false;
        buildObject.SetActive(false);
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
            transform.gameObject.GetComponent<MeshRenderer>().enabled = true;
            transform.gameObject.GetComponentInChildren<Light>().enabled = true;
            inCollider = true;
        }

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == layerNum)
        {
            transform.gameObject.GetComponent<MeshRenderer>().enabled = false;
            transform.gameObject.GetComponentInChildren<Light>().enabled = false;
            inCollider = false;
        }
    }

}
