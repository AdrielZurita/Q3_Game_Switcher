using UnityEngine;

public class First_Person_Movement : MonoBehaviour
{
    //remove: sneak maybe?
    
    private Vector3 Velocity;
    private Vector3 FallSpeed = new Vector3 (0f, 0f, 0f);
    private Vector3 PlayerMovementInput;
    private Vector2 PlayerMouseInput;
    //private bool Sneaking = false;
    private float xRotation;

    [Header("Components Needed")]
    [SerializeField] private Transform PlayerCamera;
    [SerializeField] private CharacterController Controller;
    [SerializeField] private Transform Player;
    [Space]
    [Header("Movement")]
    [SerializeField] private float Speed;
    [SerializeField] private float JumpForce;
    [SerializeField] private float Sensetivity;
    [SerializeField] private float Gravity = 9.81f;
    /*
    [Space]
    [Header("Sneaking")]
    [SerializeField] private bool Sneak = false;
    [SerializeField] private float SneakSpeed;
    */

    void Start()
    {
        //locks cursor to middle of screen for camera control
        Cursor.lockState = CursorLockMode.Locked; 
    }

    // Update is called once per frame
    void Update()
    {
        //player movement via wasd
        PlayerMovementInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
        //play camera control via mouse
        PlayerMouseInput = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        //rotates the players movement vector to where they're facing
        Velocity = PlayerMovementInput.x * transform.right + PlayerMovementInput.z * transform.forward;

        //if the player is on the ground
        if (Controller.isGrounded)
        {
            FallSpeed.y = -1f;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                FallSpeed.y = JumpForce;
            }
        }
        else
        {   
            //accelerates downward if player isn't grounded
            FallSpeed.y += Gravity * -2f  * Time.deltaTime;
        }

        Controller.Move(Velocity * Speed * Time.deltaTime);
        Controller.Move(FallSpeed * Time.deltaTime);

        //xRotation -= PlayerMouseInput.y * Sensetivity;
        //xRotation = Mathf.Clamp(xRotation, -90f, 90f);

    }

    /*
    private void MovePlayer()
    {   
        //rotates the players movement vector to where they're facing
        Vector3 MoveVector = transform.TransformDirection(PlayerMovementInput);

        //if the player is on the ground
        if (Controller.isGrounded)
        {
            Velocity.y = -1f;

            //only jumps if not sneaking
            if (Input.GetKeyDown(KeyCode.Space) && Sneaking == false)
            {
                Velocity.y = JumpForce;
            }
        }
        else
        {   
            //accelerates downward if player isn't grounded
            Velocity.y += Gravity * -2f * Time.deltaTime;
        }

        //checks if player is sneaking 
        if (Sneaking)
        {
            //modifys speed if sneaking
            Controller.Move(MoveVector * SneakSpeed * Time.deltaTime);
        }
        else
        {
            //sets it to base speed otherwise
            Controller.Move(MoveVector * Speed * Time.deltaTime);
        } 

        Controller.Move(Velocity * Time.deltaTime);

    }

    private void MoveCamera()
    {
        xRotation -= PlayerMouseInput.y * Sensetivity;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        Player.Rotate(0f, PlayerMouseInput.x * Sensetivity, 0f);  // Rotate player body instead
        PlayerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
    }
    */
}
