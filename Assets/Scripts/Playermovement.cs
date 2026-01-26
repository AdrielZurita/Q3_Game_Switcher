using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playermovement : MonoBehaviour
{
    public Rigidbody rb;
    public Vector3 movement = new Vector3(0, 0, 0);
    public float speed = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        print("start");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("a"))
        {
            movement.x += speed;
        }
        else
        {
            if (movement.x >= 0.2)
            {
                movement.x *= 0.1f;
            }
            else
            {
                movement.x = 0f;
            }
        }

        rb.velocity = movement;

    }
}
