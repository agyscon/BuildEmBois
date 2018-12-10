using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour {

    public int speed = 10;
    public AudioClip bounceSound;

    void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Rigidbody>().velocity = (Vector3.up * speed);
            Vector3 bouncePos = transform.forward;
            AudioSource.PlayClipAtPoint(bounceSound, bouncePos);

        }
    }
}
