using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gravboots : MonoBehaviour
{
    // activate key is left control
    private bool bootsActive = false;
    //public ObjectPlsHelp objectPlsHelp;
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

        /*if(objectPlsHelp.inGravBox)
        {
            canChange = false;
        }
        else
        {
            canChange = true;
        }*/
    }

    void OnTriggerEnter(Collider other)
    {   
        if (other.gameObject.GetComponent<ObjectDataHolding>().gravBootCompatible)
        {
            isAvailable = true;
        }
        else
        {
            isAvailable = false;
        }   
    }

    void OnTriggerExit(Collider other)
    {
        isAvailable = false;
    }
}
