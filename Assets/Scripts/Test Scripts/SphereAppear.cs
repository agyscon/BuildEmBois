using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereAppear : MonoBehaviour {
    public GameObject boom;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void LateUpdate () {
		if (Input.GetKeyDown("p"))
        {
            Debug.Log("Yes");
            boom.SetActive(true);
        } else
        {
            boom.SetActive(false);
        }
	}
}
