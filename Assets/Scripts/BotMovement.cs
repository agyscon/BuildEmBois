using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotMovement: MonoBehaviour {

    public enum BotMode {
        Follow, Idle, Build
    }

    [SerializeField] private BotMode botMode;
    [SerializeField] private Transform followTarget;
    private Animator anim;
    private NavMeshAgent navMeshAgent; 
    

    void Start() {
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public void SetState(BotMode mode)
    {
        botMode = mode;
    }

    public BotMode GetState()
    {
        return botMode;
    }
    void Update() {
        if (botMode == BotMode.Follow) {
            transform.gameObject.SetActive(true);
            if (navMeshAgent.destination.x != followTarget.position.x || navMeshAgent.destination.z != followTarget.position.z) {
                print(navMeshAgent.destination);
                print(flattenTransform(followTarget));
                navMeshAgent.SetDestination(flattenTransform(followTarget));
                anim.SetBool("IsBlock", false);
                anim.SetBool("Walking", true);
            } else if (navMeshAgent.remainingDistance <= 2.9f) {
                anim.SetBool("IsBlock", true);
            } else {
                anim.SetBool("Walking", true);
            }
        }
        if (botMode == BotMode.Build)
        {
            transform.gameObject.SetActive(false);
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
                BotCollector bc = c.attachedRigidbody.gameObject.GetComponent<BotCollector>();
                if (bc != null)
                {
                    bc.ReceiveBots(1, transform.gameObject);
                }
                followTarget = c.attachedRigidbody.transform;
                invent.AddBot(gameObject);
            }
        }

    }

}
