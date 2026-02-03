using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbing : MonoBehaviour
{
    public Vector3 offset = new Vector3(0, 0, 0);
    public Transform cameraPosition;
    public LayerMask box;
    public float pullForce = 10f;
    public float grabRange = 5f;
    [SerializeField] private bool isHolding = false;
    [SerializeField] private bool GrabInRange;
    [SerializeField] private Vector3 grabDirection;
    [SerializeField] private GameObject holdPoint;
    [SerializeField] private GameObject grabbedObject;
    [SerializeField] private Vector3 pullDirection;
    
    void Update()
    {
        grabDirection = cameraPosition.forward;
        //if (Input.GetMouseButtonDown(0)) this was for using left click to grab
        if (Input.GetKeyDown(KeyCode.E)) // this is for using E to grab
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, grabDirection,out hit, grabRange, box))
            {
                if(!isHolding)
                {
                    grabbedObject = hit.transform.gameObject;
                    isHolding = true;
                }
                else
                {   

                    grabbedObject = null;
                    isHolding = false;
                }
            }
            else
            {
                if(isHolding)
                {
                    isHolding = false;
                }
            }

            
        }

        if (isHolding)
        { 
            //grabbedObject.transform.position = holdPoint.transform.position;
            pullDirection = (holdPoint.transform.position - grabbedObject.transform.position);
            grabbedObject.GetComponent<Rigidbody>().velocity = pullDirection * pullForce;
        }
    }
}
