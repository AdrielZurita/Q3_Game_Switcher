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
    public Grabbing grabbingScript;
    public GameObject boxObj;
    public GameObject boxParent;
    public GameObject boxGravObj;

    
    // Start is called before the first frame update
    void Start()
    {
        rb = boxObj.GetComponent<Rigidbody>();
        if (doFunnyStuff == false)
        {
            rb.freezeRotation = true;
        }
        else
        {
            rb.freezeRotation = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics.Raycast(transform.localPosition, -transform.up, boxHeight * 0.5f + heightCheckOffset, whatIsGround);
    }
    void FixedUpdate()
    {
        if(grounded)
        {
            
        }
        else if (!grounded && grabbingScript.grabbedObject != this.gameObject)
        {
            rb.AddForce(transform.up * jumpGravity, ForceMode.Impulse);
        }
    }
}
