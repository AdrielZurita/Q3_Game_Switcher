using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravChanger : MonoBehaviour
{
    public Vector3 gravityDirection = new Vector3(-1, 0, 0);
    public float gravityMagnitude = 9.81f;
    private Rigidbody plyrRb;
    private Transform boxTransform;
    void Start()
    {
        plyrRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        boxTransform = this.transform;
        this.transform.LookAt(this.transform.position + gravityDirection);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            plyrRb.AddForce(gravityDirection.normalized * gravityMagnitude * plyrRb.mass, ForceMode.Acceleration);
        }
    }
}
