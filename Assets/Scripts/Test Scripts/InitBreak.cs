using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitBreak : MonoBehaviour {

    public GameObject remains;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {		
	}

    public GameObject breakObject()
    {
        GameObject newObject = Instantiate(remains, transform.position, transform.rotation);
        Destroy(transform.gameObject);
        return newObject;
    }
}
