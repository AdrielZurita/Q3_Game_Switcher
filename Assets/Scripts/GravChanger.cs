using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravChanger : MonoBehaviour
{
    private Rigidbody plyrRb;
    private Transform boxTransform;
    public Transform playerTransform;
    void Start()
    {
        plyrRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        boxTransform = this.transform;
        this.transform.LookAt(this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerTransform.rotation = this.transform.rotation;
        }
    }
    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerTransform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
