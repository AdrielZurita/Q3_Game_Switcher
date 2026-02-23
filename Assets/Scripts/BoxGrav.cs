using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxGrav : MonoBehaviour
{
    public LayerMask whatIsGround;
    private bool grounded;
    private float boxHeight = 1.3f;
    private float heightCheckOffset = 0.1f;
    public float jumpGravity;
    Rigidbody rb;
    public bool doFunnyStuff = false;
    GameObject playerParent;
    Grabbing grabbingScript;
    public GameObject boxObj;
    public GameObject boxParent;
    public GameObject boxGravObj;

    
    // Start is called before the first frame update
    void Start()
    {
        playerParent = GameObject.FindWithTag("Player");
        if (boxObj == null)
        {
            boxObj = this.gameObject;
        }
        Collider col = null;
        if (boxObj != null)
        {
            col = boxObj.GetComponent<Collider>();
            if (col == null)
            {
                col = boxObj.GetComponentInParent<Collider>();
            }
            if (col == null)
            {
                col = boxObj.GetComponentInChildren<Collider>();
            }
            if (col != null)
            {
                boxHeight = col.bounds.size.y;
            }
        }
        rb = boxObj.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = boxObj.GetComponentInParent<Rigidbody>();
        }
        if (rb == null)
        {
            rb = boxObj.GetComponentInChildren<Rigidbody>();
        }
        if (rb == null)
        {
            Debug.LogWarning("BoxGrav: no Rigidbody found on boxObj or its relatives: " + boxObj.name, boxObj);
        }
        grabbingScript = playerParent != null ? playerParent.GetComponent<Grabbing>() : null;
        if (rb != null)
        {
            rb.freezeRotation = !doFunnyStuff;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 origin = boxObj != null ? boxObj.transform.position : transform.position;
        grounded = Physics.Raycast(origin, -transform.up, boxHeight * 0.5f + heightCheckOffset, whatIsGround);
    }
    void FixedUpdate()
    {
        if (!grounded)
        {
            bool isGrabbed = false;
            if (grabbingScript != null && grabbingScript.grabbedObject != null)
            {
                isGrabbed = grabbingScript.grabbedObject == boxObj;
            }
            if (!isGrabbed && rb != null)
            {
                rb.AddForce(-transform.up * jumpGravity, ForceMode.Impulse);
            }
        }
    }
}
