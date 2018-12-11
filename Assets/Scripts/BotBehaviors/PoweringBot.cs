using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoweringBot : MonoBehaviour {

    private Animator anim;

    void Awake() {
        anim = GetComponent<Animator>();
    }

    void OnEnable() {
        anim.SetBool("Powering", true);
    }

}
