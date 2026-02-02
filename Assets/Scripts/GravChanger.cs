using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravChanger : MonoBehaviour
{
    public Vector3 gravityDirection = new Vector3(-1, 0, 0);
    public float gravityMagnitude = 9.81f;
    private Rigidbody plyrRb;
    private Transform boxTransform;
    private bool playerInBox = false;
    void Start()
    {
        plyrRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        boxTransform = this.transform;
        this.transform.LookAt(this.transform.position + gravityDirection);
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInBox)
        {
            plyrRb.AddForce(gravityDirection.normalized * gravityMagnitude * plyrRb.mass, ForceMode.Acceleration);
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInBox = true;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInBox = false;
        }
    }
}
