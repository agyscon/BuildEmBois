using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ExplosionForce : MonoBehaviour {

    public float radius = 2.0f;
    public float power = 10.0f;
    public float distanceInFront = 1.0f;

    private int punchableLayer = 10;

	// Use this for initialization
	void Start () {


    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("mouse 0"))
        {
            hitForce();
        }
		
	}

    public void hitForce()
    {
        Vector3 explosionPos = transform.forward * distanceInFront;
        Debug.DrawRay(transform.forward, explosionPos);
        Debug.Log(transform.TransformPoint(transform.forward));
        explosionPos = (transform.TransformPoint(explosionPos));
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();
            GameObject pieces = null;

            if (hit.transform.tag == "Breakable")
            {
                InitBreak breakScript = hit.gameObject.GetComponent<InitBreak>();
                pieces = breakScript.breakObject();

            }
            //print(rb.);
            if (rb != null && rb.gameObject.layer == punchableLayer)
            {
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
                //StartCoroutine(waitBeforeForce(rb, explosionPos));
            }

            if (pieces != null)
            {
                int counter = 0;
                foreach (Transform child in pieces.transform)
                {
                    counter++;
                    Rigidbody childRb = child.GetComponent<Rigidbody>();
                    print(punchableLayer);
                    if (childRb != null && childRb.gameObject.layer == punchableLayer)
                    {
                        childRb.isKinematic = false;
                        childRb.AddExplosionForce(power, explosionPos, radius, 3.0F);

                    }

                }
                print(counter);
            }

            else
            {
                Debug.Log("Well this sucks");
            }
        }

    }
    IEnumerator waitBeforeForce(Rigidbody body, Vector3 position)
    {
        yield return new WaitForSeconds(2);
        body.AddExplosionForce(power, position, radius, 3.0F);
    }

}
