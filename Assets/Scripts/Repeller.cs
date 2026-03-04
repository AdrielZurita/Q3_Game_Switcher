using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Repeller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Rigidbody playerRb = collision.gameObject.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                Vector3 repulsionDirection = collision.transform.position - transform.position;
                repulsionDirection.Normalize();
                float repulsionForce = 500f;
                playerRb.AddForce(repulsionDirection * repulsionForce);
            }
        }
    }
}
