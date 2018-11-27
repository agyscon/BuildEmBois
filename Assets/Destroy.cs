using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour {

    ParticleSystem hitParticles;
    bool destroyed;

    // Use this for initialization
    void Start () {
        destroyed = false;
        hitParticles = GetComponentInChildren<ParticleSystem>();
    }
	
	// Update is called once per frame
	void Update () {
        if (destroyed)
        {
            if (!hitParticles.isPlaying)
            {
                hitParticles.Play();
            }
            Destroy(gameObject, 2f);
        }
        
	}

    public void Break()
    {
        destroyed = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Break();
        }
    }
}
