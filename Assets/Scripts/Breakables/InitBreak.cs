using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitBreak : MonoBehaviour {

    public GameObject remains;
    GameObject newObject;

	// Use this for initialization
	void Start () {
        newObject = Instantiate(remains, transform.position, transform.rotation);
        newObject.active = false;
		
	}
	
	// Update is called once per frame
	void Update () {		
	}

    public GameObject breakObject()
    {
        newObject.active = true;
        Destroy(transform.gameObject);
        return newObject;
    }
}
