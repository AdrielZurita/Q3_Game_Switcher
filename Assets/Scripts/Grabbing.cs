using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbing : MonoBehaviour
{
    public Vector3 offset = new Vector3(0, 0, 3);
    public Transform cameraPosition;
    public bool toggle;
    public bool GrabInRange;
    
    void Update()
    {
        //grabDirection = offset * cameraPosition.forward;
        //if (Input.GetMouseButtonDown(0)) this was for using left click to grab
        if (Input.GetKey(KeyCode.E)) // this is for using E to grab
        {
            if (toggle)
            {
                toggle = false;
            }
            else
            {
                toggle = true;
            }
        }

        if (toggle)
        {
            GrabInRange = true; //Physics.Raycast(transform.position, Vector3.down, playerHeight * 0.5f + 0.2f, whatIsGround);
        }
        else
        {
            
        }
    }
}
