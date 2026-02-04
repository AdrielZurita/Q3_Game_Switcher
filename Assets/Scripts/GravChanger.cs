using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravChanger : MonoBehaviour
{
    private Rigidbody plyrRb;
    private Transform boxTransform;
    private Transform playerTransform;
    void Start()
    {
        plyrRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        boxTransform = this.transform;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(discTransform.position, discTransform.forward, out hit, raycastDistance, groundLayer))
        {
            FaceTowardsWall(hit);
        }
    }
    void OnTriggerEnter(Collider collision)
    {
        Debug.Log("test");
        if (collision.gameObject.tag == "Player")
        {
            Debug.Log("testPlayer");
            playerTransform.rotation = this.transform.rotation;
        }
    }
    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerTransform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }

    public void FaceTowardsWall(RaycastHit hit)
    {
        Vector3 targetDirection = Vector3.zero;
        targetDirection = hit.point - transform.position;
        transform.rotation = Quaternion.LookRotation(-hit.normal, transform.up);
    }
}
