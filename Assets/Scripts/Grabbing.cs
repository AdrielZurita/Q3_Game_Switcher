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
    public bool isHolding = false;
    private bool GrabInRange;
    private Vector3 grabDirection;
    private Vector3 pullDirection;
    [SerializeField] private GameObject holdPoint;
    [SerializeField] public GameObject grabbedObject;
    public ObjectPlsHelp objectPlsHelp;
    private Collider[] playerColliders;
    private Collider[] grabbedColliders;
    
    void Start()
    {
        playerColliders = GetComponentsInChildren<Collider>();
    }
        
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
                    objectPlsHelp.canThrow = false;
                    // ignore collisions between player and grabbed object
                    if (grabbedObject != null)
                    {
                        grabbedColliders = grabbedObject.GetComponentsInChildren<Collider>();
                        if (playerColliders != null && grabbedColliders != null)
                        {
                            foreach (var pCol in playerColliders)
                                foreach (var gCol in grabbedColliders)
                                    if (pCol != null && gCol != null)
                                        Physics.IgnoreCollision(pCol, gCol, true);
                        }
                    }
                }
                else
                {   
                    if (grabbedObject != null)
                    {
                        // re-enable collisions
                        grabbedColliders = grabbedObject.GetComponentsInChildren<Collider>();
                        if (playerColliders != null && grabbedColliders != null)
                        {
                            foreach (var pCol in playerColliders)
                                foreach (var gCol in grabbedColliders)
                                    if (pCol != null && gCol != null)
                                        Physics.IgnoreCollision(pCol, gCol, false);
                        }

                        grabbedObject.GetComponent<Rigidbody>().freezeRotation = false;
                        grabbedObject = null;
                    }
                    isHolding = false;
                    objectPlsHelp.canThrow = true;
                }
            }
            else
            {
                if(isHolding)
                {
                    if (grabbedObject != null)
                    {
                        // re-enable collisions
                        grabbedColliders = grabbedObject.GetComponentsInChildren<Collider>();
                        if (playerColliders != null && grabbedColliders != null)
                        {
                            foreach (var pCol in playerColliders)
                                foreach (var gCol in grabbedColliders)
                                    if (pCol != null && gCol != null)
                                        Physics.IgnoreCollision(pCol, gCol, false);
                        }

                        grabbedObject.GetComponent<Rigidbody>().freezeRotation = false;
                        grabbedObject = null;
                    }
                    isHolding = false;
                    objectPlsHelp.canThrow = true;
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
