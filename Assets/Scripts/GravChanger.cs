using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravChanger : MonoBehaviour
{
    private Rigidbody plyrRb;
    private Transform boxTransform;
    private Transform playerTransform;
    public ObjectPlsHelp objectPlsHelp;
    public GameObject objectInBox;

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
            Vector3 repulsionDirection = collision.transform.position - transform.position;
            repulsionDirection.Normalize();
            float repulsionForce = 800f;
            plyrRb.AddForce(repulsionDirection * repulsionForce);
        }

        if (collision.gameObject.tag == "box" && objectPlsHelp.isPositive == true)
        {
            objectInBox = collision.gameObject;
            Transform boxParent = collision.gameObject.transform.parent;
            Transform boxCtrl = boxParent.Find("Box Grav controller");
            if (boxCtrl != null)
            {
                boxCtrl.rotation = this.transform.rotation;
            }
            else
            {
                Debug.LogWarning("Child 'Box Grav controller' not found on " + collision.gameObject.name, collision.gameObject);
                collision.gameObject.transform.rotation = this.transform.rotation;
            }
        }
        else if (collision.gameObject.tag == "box" && objectPlsHelp.isPositive == false)
        {
            Vector3 repulsionDirection = collision.transform.position - transform.position;
            repulsionDirection.Normalize();
            float repulsionForce = 800f;
            Rigidbody boxRb = collision.gameObject.GetComponent<Rigidbody>();
            if (boxRb != null)
            {
                boxRb.AddForce(repulsionDirection * repulsionForce);
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

        if (collision.gameObject.tag == "box" && objectPlsHelp.isPositive == true)
        {
            Transform boxParent = collision.gameObject.transform.parent;
            Transform boxCtrl = boxParent.Find("Box Grav controller");
            if (boxCtrl != null)
            {
                boxCtrl.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                Debug.LogWarning("Child 'Box Grav controller' not found on " + collision.gameObject.name, collision.gameObject);
                collision.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
            }
        }
    }

    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && objectPlsHelp.isPositive == false)
        {
            Vector3 repulsionDirection = collision.transform.position - transform.position;
            repulsionDirection.Normalize();
            float repulsionForce = 800f;
            plyrRb.AddForce(repulsionDirection * repulsionForce);
        }
        if (collision.gameObject.tag == "box" && objectPlsHelp.isPositive == false)
        {
            Vector3 repulsionDirection = collision.transform.position - transform.position;
            repulsionDirection.Normalize();
            float repulsionForce = 800f;
            Rigidbody boxRb = collision.gameObject.GetComponent<Rigidbody>();
            if (boxRb != null)
            {
                boxRb.AddForce(repulsionDirection * repulsionForce);
            }
        }
    }

    public void FaceTowardsWall(RaycastHit hit)
    {
        Vector3 targetDirection = Vector3.zero;
        targetDirection = hit.point - transform.position;
        transform.rotation = Quaternion.LookRotation(-hit.normal, transform.up);
    }
}
