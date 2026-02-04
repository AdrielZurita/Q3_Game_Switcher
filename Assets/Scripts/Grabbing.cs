using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabbing : MonoBehaviour
{
    public Vector3 offset = new Vector3(0, 0, 0);
    public Transform cameraPosition;
    public LayerMask grabLayer;
    public float pullForce = 10f;
    public float grabRange = 5f;
    private bool isHolding = false;
    private bool GrabInRange;
    private Vector3 grabDirection;
    private Vector3 pullDirection;
    [SerializeField] private GameObject holdPoint;
    [SerializeField] private GameObject grabbedObject;
        
    void Update()
    {
        grabDirection = cameraPosition.forward;
        //if (Input.GetMouseButtonDown(0)) this was for using left click to grab
        if (Input.GetKeyDown(KeyCode.E)) // this is for using E to grab
        {
            RaycastHit hit;
            if(Physics.Raycast(transform.position, grabDirection,out hit, grabRange, grabLayer))
            {
                if(!isHolding)
                {
                    grabbedObject = hit.transform.gameObject;
                    isHolding = true;
                }
                else
                {   
                    grabbedObject.GetComponent<Rigidbody>().freezeRotation = false;
                    grabbedObject = null;
                    isHolding = false;
                }
            }
            else
            {
                if(isHolding)
                {
                    grabbedObject.GetComponent<Rigidbody>().freezeRotation = false;
                    grabbedObject = null;
                    isHolding = false;
                }
            }

            
        }

        if (isHolding)
        { 
            //grabbedObject.transform.position = holdPoint.transform.position;
            grabbedObject.GetComponent<Rigidbody>().freezeRotation = true;
            pullDirection = (holdPoint.transform.position - grabbedObject.transform.position);
            grabbedObject.GetComponent<Rigidbody>().velocity = pullDirection * pullForce;
        }
    }
}
