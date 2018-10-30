using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    public Vector3 jump;
    public float jumpForce = 5.0f;

    public bool isGrounded;
    Rigidbody rb;
    Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        jump = Vector3.up;
    }

    void Update()
    {
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);

        if (Input.GetKeyDown ("space") && isGrounded)
        {
            // add impulse to move the character up and fall again
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }


    }

    //This is a physics callback
    void OnCollisionStay(Collision collision)
    {
        isGrounded = true;
    }
}
