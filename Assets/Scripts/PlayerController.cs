using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class PlayerController : MonoBehaviour
{
    public Vector3 jump;
    public float jumpForce = 5.0f;
    private bool canJump;
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
        if (rb.velocity.y == 0)
        {
            isGrounded = true;
        }
        var x = Input.GetAxis("Horizontal");
        var z = Input.GetAxis("Vertical");

        //anim.SetFloat("velx", x);
        anim.SetFloat("Speed", z);
        
        x = x * Time.deltaTime * 150.0f;
        z = z * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        //transform.Translate(0, 0, z);

        if (Input.GetKeyDown ("space") && isGrounded)
        {
            // add impulse to move the character up and fall again
            rb.AddForce(jump * jumpForce, ForceMode.Impulse);
            isGrounded = false;
            anim.SetTrigger("Jump");
        }

    }
}
