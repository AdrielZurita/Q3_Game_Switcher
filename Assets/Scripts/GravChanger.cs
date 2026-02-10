using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravChanger : MonoBehaviour
{
    private Rigidbody plyrRb;
    private Transform boxTransform;
    private Transform playerTransform;
    public ObjectPlsHelp objectPlsHelp;

    void Start()
    {
        plyrRb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        boxTransform = this.transform;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && objectPlsHelp.isPositive == true)
        {
            playerTransform.rotation = this.transform.rotation;
            objectPlsHelp.inGravBox = true;
        }
        else if (collision.gameObject.tag == "Player" && objectPlsHelp.isPositive == false)
        {
            // repel player
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                Vector3 repulsionDirection = collision.transform.position - transform.position;
                repulsionDirection.Normalize();
                float repulsionForce = 800f;
                playerRb.AddForce(repulsionDirection * repulsionForce);
            }
        }
    }
    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && objectPlsHelp.isPositive == true)
        {
            objectPlsHelp.inGravBox = false;
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
