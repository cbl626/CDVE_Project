using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float playerHeight;
    [SerializeField] Transform orientation;

    [Header("Movement")]
    [SerializeField] float moveSpeed;

    [SerializeField] float groundDrag;
    [Header("Jump")]
    [SerializeField] KeyCode jumpKey = KeyCode.Space;
    [SerializeField] float jumpForce;
    [SerializeField] float jumpCooldown;
    [SerializeField] float airMultiplier;

    [Header("Ground Check")]
    [SerializeField] LayerMask whatIsGround;
    [SerializeField] bool isGrounded;
    

    bool readyToJump;
    float horizontalInput;
    float verticalInput;
    Vector3 moveDirection;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        readyToJump = true;
    }

    void Update()
    {
        // Ground check
        isGrounded = Physics.Raycast(transform.position, Vector3.down, playerHeight * .5f + .2f, whatIsGround);
        
        PlayerInput();

        SpeedControl();

        // Handle drag
        rb.drag = isGrounded ? groundDrag : 0;
    }

    void FixedUpdate()
    {
        MovePlayer();
    }


    void PlayerInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // When to jump
        if (Input.GetKey(jumpKey) && readyToJump && isGrounded)
        {
            readyToJump = false;
            Jump();
            Invoke(nameof(ResetJump), jumpCooldown);
        }


    }
    void MovePlayer()
    {
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        float multiplier = isGrounded ? 1f : airMultiplier;

        rb.AddForce(moveDirection.normalized * moveSpeed * 10f * multiplier, ForceMode.Force);

    }

    void SpeedControl()
    {
        Vector3 flatVelocity = new(rb.velocity.x, 0f, rb.velocity.z);

        //Limit velocity if needed
        if(flatVelocity.magnitude > moveSpeed)
        {
            Vector3 limitedVelocity = flatVelocity.normalized * moveSpeed;
            rb.velocity = new(limitedVelocity.x, rb.velocity.y, limitedVelocity.z);
        }
    } 

    void Jump()
    {
        // Reset y velocity
        rb.velocity = new(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce,ForceMode.Impulse);
    }

    void ResetJump()
    {
        readyToJump = true;
    }

}
