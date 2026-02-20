using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravboots : MonoBehaviour
{
    // activate key is left control
    private bool bootsActive = false;
    public ObjectPlsHelp objectPlsHelp;
    private bool isAvailable = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.LeftControl))
        {
            bootsActive = !bootsActive;
            print("Grav boots active: " + bootsActive);
            print("Grav boots available: " + isAvailable);
        }

        if(isAvailable)
        {
            if(bootsActive)
            {
                //transform.rotation = Quaternion.RotateTowards(transform.rotation, this.boxTransform.rotation, rotationSpeed * Time.deltaTime);
            }
            else
            {
                if(objectPlsHelp.inGravBox)
                {
                    print("deactivated in box");
                    // do nothing
                }
                else
                {
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, 0), rotationSpeed * Time.deltaTime); // might rotate player backwards??
                    print("deactivated out of box");
                }
            }
        }


        /*if(objectPlsHelp.inGravBox)
        {
            canChange = false;
        }
        else
        {
            canChange = true;
        }*/
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
    }

    void OnCollisionExit(Collision other)
    {
        isAvailable = false;
    }
}




