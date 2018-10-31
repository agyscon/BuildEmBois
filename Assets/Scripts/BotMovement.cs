using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotMovement: MonoBehaviour {

    private enum BotMode {
        Follow, Idle
    }

    [SerializeField] private BotMode botMode;
    [SerializeField] private Transform followTarget;
    private Animator anim;
    private NavMeshAgent navMeshAgent;
    

    void Start() {
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update() {
        if (botMode == BotMode.Follow) {
            if (navMeshAgent.destination.x != followTarget.position.x || navMeshAgent.destination.z != followTarget.position.z) {
                print(navMeshAgent.destination);
                print(flattenTransform(followTarget));
                navMeshAgent.SetDestination(flattenTransform(followTarget));
                anim.SetBool("IsBlock", false);
                anim.SetBool("Walking", true);
            } else if (navMeshAgent.remainingDistance <= 0.9f) {
                anim.SetBool("IsBlock", true);
            } else {
                anim.SetBool("Walking", true);
            }
        }
        anim.SetFloat("Y", navMeshAgent.velocity.magnitude / navMeshAgent.speed);
    }

    // Takes a transform and turns it into a vector3 with y of 0
    private Vector3 flattenTransform(Transform transform) {
        return new Vector3(transform.position.x, 0, transform.position.z);
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.attachedRigidbody != null)
        {
            InventoryScript invent = c.attachedRigidbody.gameObject.GetComponent<InventoryScript>();
            if (invent != null && botMode != BotMode.Follow)
            {
                botMode = BotMode.Follow;
                invent.AddBot(gameObject);
            }
        }

    }

}
