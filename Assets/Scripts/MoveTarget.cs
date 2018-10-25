using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTarget : MonoBehaviour {

    [SerializeField] private Vector3[] waypoints;
    private int currentWaypiont = 0;
    private float moveTimer = 7f;
    private float timerReset = 7f;

    void Start() {
        waypoints = new Vector3[4];
        waypoints[0] = new Vector3(2.49f, 2, -2.56f);
        waypoints[1] = new Vector3(2.49f, 3, 2.56f);
        waypoints[2] = new Vector3(-2.49f, 2, 2.56f);
        waypoints[3] = new Vector3(-2.49f, 2, -2.56f);
    }

    void Update() {
        moveTimer -= Time.deltaTime;
        if (moveTimer <= 0) {
            currentWaypiont++;
            currentWaypiont %= 4;
            StartCoroutine(LerpToPosition(waypoints[currentWaypiont]));
            moveTimer += timerReset;
        }
    }

    IEnumerator LerpToPosition(Vector3 target) {
        while (Vector3.Distance(transform.position, target) > 0.01) {
            transform.position = Vector3.Lerp(transform.position, target, 0.1f);
            yield return null;
        }
        transform.position = target;
    }
}
