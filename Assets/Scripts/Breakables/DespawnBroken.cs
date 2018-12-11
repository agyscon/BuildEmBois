using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnBroken : MonoBehaviour {
    public AudioClip objectNoise;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        StartCoroutine(waitForSeconds());
		
	}

    IEnumerator waitForSeconds()
    {
        yield return new WaitForSeconds(10);
        Destroy(transform.gameObject);
    }

    
}
