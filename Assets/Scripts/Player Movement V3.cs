using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementV3 : MonoBehaviour
{
    [Header("movement")]
    public float moveSpeed;
    public float groundDrag;
    public float jumpForce;
    public float jumpCoolDown;
    public float airMult;
    bool readyToJump = true;
    public float jumpGravity;
    public float sprintSpeedMultiplier = 1.2f;
    private float tempSpeed;
    [Header("ground check")]
    public float playerHeight;
    public LayerMask whatIsGround;
    public float heightCheckOffset = 0.2f;
    bool grounded;
    [Header("orientation")]
    public Transform orientation; 
    float horizontalInput;
    float verticalInput; 
    Vector3 moveDirection; 
    Rigidbody rb;
    public ObjectPlsHelp objectPlsHelp;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
        objectPlsHelp.canMove = true;
    }

    void Update()
    {
        grounded = Physics.Raycast(transform.localPosition, -transform.up, playerHeight * 0.5f + heightCheckOffset, whatIsGround);
        
        MyInput();
        SpeedControl();

        if (grounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");

        if(Input.GetKey(KeyCode.Space) && readyToJump && grounded && objectPlsHelp.canMove)
        {
            Jump();
            readyToJump = false;
            Invoke(nameof(ResetJump), jumpCoolDown);
        }

        if(Input.GetKey(KeyCode.LeftShift))
        {
            tempSpeed = moveSpeed * sprintSpeedMultiplier;
        }
        else
        {
            tempSpeed = moveSpeed;
        }
    }

    private void MovePlayer()
    {
        moveDirection = verticalInput * orientation.forward + horizontalInput * orientation.right;
        
        if(grounded && objectPlsHelp.canMove)
        {
            rb.AddForce(moveDirection.normalized * tempSpeed * 10f, ForceMode.Force);
        }
        else if (!grounded && objectPlsHelp.canMove)
        {
            rb.AddForce(transform.up * jumpGravity, ForceMode.Impulse);
            rb.AddForce(moveDirection.normalized * tempSpeed * 10f * airMult, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if(flatVel.magnitude > tempSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * tempSpeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y, limitedVel.z);
        }
    }

    private void Jump()
    {
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void ResetJump()
    {
        readyToJump = true;
    }

}
