using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BotMovement: MonoBehaviour {

    public enum BotMode {
        Follow, Stopped, Idle, Build
    }

    [SerializeField] private BotMode botMode;
    [SerializeField] private Transform followTarget;
    private Animator anim;
    private NavMeshAgent navMeshAgent; 

    // The bot another bot bumped into. Let's the bot know when to move to avoid crowding.
    private BotMovement bumpedBot;


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
            if (Vector3.Distance(FlattenTransform(followTarget.position), FlattenTransform(navMeshAgent.destination)) > 0.5f) {
                if (!navMeshAgent.pathPending) {
                    navMeshAgent.SetDestination(FlattenTransform(followTarget.position));
                }
                anim.SetBool("IsBlock", false);
                anim.SetBool("Walking", true);
                if (navMeshAgent.isStopped) {
                    anim.SetBool("Walking", false);
                    botMode = BotMode.Stopped;
                }
            } else if (navMeshAgent.remainingDistance <= 2f) {
                anim.SetBool("Walking", false);
                navMeshAgent.isStopped = true;
                botMode = BotMode.Stopped;
            }
        } else if (botMode == BotMode.Stopped) {
            if (bumpedBot != null) {
                if (bumpedBot.GetState() != BotMode.Stopped) {
                    bumpedBot = null;
                    botMode = BotMode.Follow;
                }
            } else {
                if (Vector3.Distance(FlattenTransform(followTarget.position), FlattenTransform(navMeshAgent.destination)) > 0.5f) {
                    navMeshAgent.isStopped = false;
                    anim.SetBool("Walking", true);
                    botMode = BotMode.Follow;
                }
            }
        }
        if (botMode == BotMode.Build)
        {
            transform.gameObject.SetActive(false);
        }
        anim.SetFloat("Y", navMeshAgent.velocity.magnitude / navMeshAgent.speed);
    }

    // Takes a transform and turns it into a vector3 with y of 0
    private Vector3 FlattenTransform(Vector3 transform) {
        return new Vector3(transform.x, 0, transform.z);
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("Player") && c.attachedRigidbody != null)
        {
            InventoryScript invent = c.attachedRigidbody.gameObject.GetComponent<InventoryScript>();
            if (invent != null && botMode == BotMode.Idle)
            {
                botMode = BotMode.Follow;
                BotCollector bc = c.attachedRigidbody.gameObject.GetComponent<BotCollector>();
                if (bc != null)
                {
                    bc.ReceiveBots(1, transform.gameObject);
                }
                followTarget = c.attachedRigidbody.transform;
                invent.AddBot();
            }
        }
        else if (c.gameObject.CompareTag("BotPart") && botMode == BotMode.Follow)
        {
            BotPart botPart = c.GetComponent<BotPart>();
            if (botPart != null) {
                GameObject bot = botPart.GetParentBot();
                if (bot != null) {
                    BotMovement botScript = bot.GetComponent<BotMovement>();
                    if (botScript != null) {
                        BumpBot(botScript);
                    }
                }
            }
        }
    }
    
    void OnTriggerStay(Collider c) {
        if (c.gameObject.CompareTag("Bot") && botMode == BotMode.Follow) {
            BotPart botPart = c.GetComponent<BotPart>();
            if (botPart != null) {
                GameObject bot = botPart.GetParentBot();
                if (bot != null) {
                    BotMovement botScript = bot.GetComponent<BotMovement>();
                    if (botScript != null) {
                        BumpBot(botScript);
                    }
                }
            }
        }
    }

    // Stop when colliding with another bot that's stopped
    private void BumpBot(BotMovement botScript) {
        if (botScript != null)
        {
            if (botScript.GetState() == BotMode.Stopped)
            {
                anim.SetBool("Walking", false);
                navMeshAgent.isStopped = true;
                botMode = BotMode.Stopped;
                bumpedBot = botScript;
            }
        }
    }

}
