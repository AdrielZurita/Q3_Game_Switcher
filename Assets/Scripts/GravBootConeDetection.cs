using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravBootConeDetection : MonoBehaviour
{
    private bool isAvailable = false;
    public bool GravBootsUnlocked = false;
    private Vector3 newGravityDirection;
    private Quaternion DecetorFinalRotation;
    public bool bootsActive = false;
    public ObjectPlsHelp objectPlsHelp;
    public float rotationSpeed = 400f;
    public Transform PlayerParent;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   
        if(objectPlsHelp.inGravBox)
        {
            isAvailable = true;
        }
        
        if(Input.GetKeyDown(KeyCode.LeftControl) && GravBootsUnlocked)
        {
            bootsActive = !bootsActive;
            //print("Grav boots active: " + bootsActive);
            //print("Grav boots available: " + isAvailable);
            //Debug.Log("Final Rotation: " + DecetorFinalRotation);
        }

        if(isAvailable)
        {
            if(bootsActive)
            {
                PlayerParent.transform.rotation = Quaternion.RotateTowards(PlayerParent.transform.rotation, DecetorFinalRotation, rotationSpeed * Time.deltaTime);
            }
            else
            {
                if(objectPlsHelp.inGravBox)
                {
                    // do nothing
                }
                else
                {
                    PlayerParent.transform.rotation = Quaternion.RotateTowards(PlayerParent.transform.rotation, Quaternion.Euler(0, 0, 0), rotationSpeed * Time.deltaTime); // might rotate player backwards??
                }
            }
        }       
    }

    void OnCollisionStay(Collision other)
    {
        if (other.gameObject.GetComponent<ObjectDataHolding>() == null || !other.gameObject.GetComponent<ObjectDataHolding>().gravBootCompatible)
        {
            isAvailable = false;
        }
        else if (other.gameObject.GetComponent<ObjectDataHolding>().gravBootCompatible)
        {
            isAvailable = true;
        }   
        else if (other.gameObject.tag == "canWall" && isAvailable)
        {
            isAvailable = true;
        }

        //Debug.Log("Collision");
        if (isAvailable)
        {
            ContactPoint contact = other.contacts[0];
            newGravityDirection = -contact.normal;
            Vector3 up = contact.normal.normalized;
            Vector3 forwardOnPlane = Vector3.ProjectOnPlane(transform.forward, up);
            if (forwardOnPlane.sqrMagnitude < 0.0001f)
            {
                forwardOnPlane = Vector3.ProjectOnPlane(transform.right, up);
                if (forwardOnPlane.sqrMagnitude < 0.0001f)
                    {
                        forwardOnPlane = Vector3.ProjectOnPlane(Vector3.forward, up);
                    }
            }
            forwardOnPlane.Normalize();
            DecetorFinalRotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
            //print("Detector Final Rotation: " + DecetorFinalRotation);
        }
    }

    void OnCollisionExit(Collision other)
    {
        isAvailable = false;
    }
}
