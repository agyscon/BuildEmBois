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
    private bool jumping;
    public AudioClip[] stepSounds;


    void Start() {
        anim = GetComponent<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        jumping = false;
    }

    void OnEnable() {
        if (botMode != BotMode.Idle) {
            anim.SetBool("BootUp", true);
        }
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
        if (navMeshAgent.isOnOffMeshLink && !jumping) {
            StartCoroutine(JumpOffMesh());
        }
        if (botMode == BotMode.Follow) {
            if (Vector3.Distance(FlattenTransform(followTarget.position), FlattenTransform(navMeshAgent.destination)) > 0.5f) {
                if (!navMeshAgent.pathPending) {
                    navMeshAgent.SetDestination((followTarget.position));
                }
                anim.SetBool("IsBlock", false);
                anim.SetBool("Walking", true);
                if (navMeshAgent.isStopped) {
                    anim.SetBool("Walking", false);
                    botMode = BotMode.Stopped;
                }
            } else if (navMeshAgent.remainingDistance <= 3f) {
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
        if (gameObject.activeSelf) {
            anim.SetFloat("Y", navMeshAgent.velocity.magnitude / navMeshAgent.speed);
        }
    }

    // Takes a transform and turns it into a vector3 with y of 0
    private Vector3 FlattenTransform(Vector3 transform) {
        return new Vector3(transform.x, 0, transform.z);
    }

    void OnTriggerEnter(Collider c)
    {
        if (c.gameObject.CompareTag("Player") && c.attachedRigidbody != null)
        {
            //NOTE: inventory script was deleted if more problems arise. This is what we changed
            //InventoryScript invent = c.attachedRigidbody.gameObject.GetComponent<InventoryScript>();
            if (/*invent != null &&*/ botMode == BotMode.Idle)
            {
                botMode = BotMode.Follow;
                anim.SetBool("BootUp", true);
                BotCollector bc = c.attachedRigidbody.gameObject.GetComponent<BotCollector>();
                if (bc != null)
                {
                    bc.ReceiveBots(1, transform.gameObject);
                }
                followTarget = c.attachedRigidbody.transform;
                //invent.AddBot();
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

    private IEnumerator JumpOffMesh() {
        jumping = true;
        OffMeshLinkData offMeshLinkData = navMeshAgent.currentOffMeshLinkData;
        // Find source and destination and adjust for bot height
        Vector3 startPos = offMeshLinkData.startPos;
        startPos.y += 0.5f;
        Vector3 endPos = offMeshLinkData.endPos;
        endPos.y += 0.5f;
        // Calculate highest point bot should jump to
        float maxHeight = Mathf.Max(startPos.y, endPos.y) + 1 + 0.05f * Mathf.Abs(startPos.y - endPos.y);
        // Lerp bot to start pos as necessary
        while (Vector3.Distance(transform.position, startPos) > 0.2f) {
            Vector3 lerpPos = transform.position;
            lerpPos.x = Mathf.Lerp(transform.position.x, startPos.x, 0.1f);
            lerpPos.y = Mathf.Lerp(transform.position.y, startPos.y, 0.1f);
            lerpPos.z = Mathf.Lerp(transform.position.z, startPos.z, 0.1f);
            transform.position = lerpPos;
            yield return null;
        }
        transform.position = startPos;
        float timeElapsed = 0f;
        anim.SetTrigger("JumpUp");
        // Wait for jump to start
        while (timeElapsed < 0.5f) {
            yield return null;
            timeElapsed += Time.deltaTime;
            anim.SetBool("Airborne", true);
        }
        Vector3 pos = transform.position;
        // Jump across gap
        while (timeElapsed < 1.5f) {
            pos.x = Mathf.SmoothStep(startPos.x, endPos.x, (timeElapsed - 0.5f));
            pos.z = Mathf.SmoothStep(startPos.z, endPos.z, (timeElapsed - 0.5f));
            if (timeElapsed < 1f) {
                float amplitude = maxHeight - startPos.y;
                pos.y = startPos.y + amplitude * Mathf.Sin(Mathf.PI * (timeElapsed - 0.5f));
            } else {
                float amplitude = maxHeight - endPos.y;
                pos.y = endPos.y + amplitude * Mathf.Sin(Mathf.PI * (timeElapsed - 0.5f));
            }
            transform.position = pos;
            yield return null;
            timeElapsed += Time.deltaTime;
        }
        anim.SetBool("Airborne", false);
        // Land
        while (timeElapsed < 2f) {
            yield return null;
            timeElapsed += Time.deltaTime;
        }
        // Lerp bot to start pos as necessary
        while (Vector3.Distance(transform.position, endPos) > 0.2f) {
            Vector3 lerpPos = transform.position;
            lerpPos.x = Mathf.Lerp(transform.position.x, endPos.x, 0.1f);
            lerpPos.y = Mathf.Lerp(transform.position.y, endPos.y, 0.1f);
            lerpPos.z = Mathf.Lerp(transform.position.z, endPos.z, 0.1f);
            transform.position = lerpPos;
            yield return null;
        }
        transform.position = endPos;
        navMeshAgent.CompleteOffMeshLink();
        jumping = false;
    }

    private IEnumerator JumpToArea(Vector3 destination) {
        jumping = true;
        OffMeshLinkData offMeshLinkData = navMeshAgent.currentOffMeshLinkData;
        // Find source and destination and adjust for bot height
        Vector3 startPos = offMeshLinkData.startPos;
        startPos.y += 0.5f;
        Vector3 endPos = offMeshLinkData.endPos;
        endPos.y += 0.5f;
        // Calculate highest point bot should jump to
        float maxHeight = Mathf.Max(startPos.y, endPos.y) + 1 + 0.05f * Mathf.Abs(startPos.y - endPos.y);
        // Lerp bot to start pos as necessary
        while (Vector3.Distance(transform.position, startPos) > 0.2f) {
            Vector3 lerpPos = transform.position;
            lerpPos.x = Mathf.Lerp(transform.position.x, startPos.x, 0.1f);
            lerpPos.y = Mathf.Lerp(transform.position.y, startPos.y, 0.1f);
            lerpPos.z = Mathf.Lerp(transform.position.z, startPos.z, 0.1f);
            transform.position = lerpPos;
            yield return null;
        }
        transform.position = startPos;
        float timeElapsed = 0f;
        anim.SetTrigger("JumpUp");
        // Wait for jump to start
        while (timeElapsed < 0.5f) {
            yield return null;
            timeElapsed += Time.deltaTime;
            anim.SetBool("Airborne", true);
        }
        Vector3 pos = transform.position;
        // Jump across gap
        while (timeElapsed < 1.5f) {
            pos.x = Mathf.SmoothStep(startPos.x, endPos.x, (timeElapsed - 0.5f));
            pos.z = Mathf.SmoothStep(startPos.z, endPos.z, (timeElapsed - 0.5f));
            if (timeElapsed < 1f) {
                float amplitude = maxHeight - startPos.y;
                pos.y = startPos.y + amplitude * Mathf.Sin(Mathf.PI * (timeElapsed - 0.5f));
            } else {
                float amplitude = maxHeight - endPos.y;
                pos.y = endPos.y + amplitude * Mathf.Sin(Mathf.PI * (timeElapsed - 0.5f));
            }
            transform.position = pos;
            yield return null;
            timeElapsed += Time.deltaTime;
        }
        anim.SetBool("Airborne", false);
        // Land
        while (timeElapsed < 2f) {
            yield return null;
            timeElapsed += Time.deltaTime;
        }
        // Lerp bot to start pos as necessary
        while (Vector3.Distance(transform.position, endPos) > 0.2f) {
            Vector3 lerpPos = transform.position;
            lerpPos.x = Mathf.Lerp(transform.position.x, endPos.x, 0.1f);
            lerpPos.y = Mathf.Lerp(transform.position.y, endPos.y, 0.1f);
            lerpPos.z = Mathf.Lerp(transform.position.z, endPos.z, 0.1f);
            transform.position = lerpPos;
            yield return null;
        }
        transform.position = endPos;
        navMeshAgent.CompleteOffMeshLink();
        jumping = false;
    }

    public void Step()
    {
        Vector3 stepPos = transform.position;

        AudioSource.PlayClipAtPoint(this.stepSounds[Random.Range(0, stepSounds.Length)], stepPos, .1f);
    }
}
