using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCharController : MonoBehaviour {
    private CharacterController controller;
    public float speed = 1;
    public float gravity = 9.8f;
    public float ySpeed = 0f;
    public float xSpeed = 0f;
    public float zSpeed = 0f;
    public float jumpSpeed = 5f;

	// Use this for initialization
	void Start () {
        controller = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
        float xInput = Input.GetAxis("Horizontal");
        float zInput = Input.GetAxis("Vertical");
        
        float t = Time.deltaTime;
        Vector3 movementVector = new Vector3(xInput, ySpeed, zInput);
        if (controller.isGrounded)
        {
            ySpeed = 0;
            if (Input.GetKeyDown("space"))
            {
                ySpeed = jumpSpeed;
            }
        }

        ySpeed -= gravity * t;
        movementVector.y = ySpeed;

        controller.Move(speed * movementVector * t);


	}
}
