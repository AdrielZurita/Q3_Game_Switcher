using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscHandler : MonoBehaviour
{
    public GameObject gravBoxObj;
    private Transform discTransform;
    private Rigidbody discRigidbody;
    public bool returning = false;
    public ObjectPlsHelp objectPlsHelp;
    public float bounceForce = 15f;
    public float bounceDamping = 0.8f;
    public LayerMask groundLayer;
    public float raycastDistance = 4f;
    private Vector3 newGravityDirection;

    // Start is called before the first frame update
    void Start()
    {
        discTransform = this.transform;
        discRigidbody = GetComponent<Rigidbody>();
        discTransform.Rotate(90f, 0f, 0f);
    }

    // Update is called once per frame
    /*void FixedUpdate()
    {
        RaycastHit hit;
        if (Physics.Raycast(discTransform.position, discTransform.forward, out hit, raycastDistance, groundLayer))
        {
            newGravityDirection = -hit.normal;
        }
    }*/

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "CanWall" && objectPlsHelp.returning == false)
        {
            RaycastHit hit;
            if (Physics.Raycast(discTransform.position, discTransform.forward, out hit, raycastDistance, groundLayer))
            {
                newGravityDirection = -hit.normal;
            }
            discTransform.rotation = Quaternion.LookRotation(newGravityDirection, transform.up);
            GameObject gravBox = Instantiate(gravBoxObj, transform.position, transform.rotation);
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "Player" && objectPlsHelp.returning == true)
        {
            objectPlsHelp.havedisc = true;
            objectPlsHelp.returning = false;
            Destroy(this.gameObject);
        }
        if (collision.gameObject.tag == "CantWall" && objectPlsHelp.returning == false)
        {
            Vector3 bounceDirection = collision.relativeVelocity.normalized;
            Vector3 reflectedVelocity = Vector3.Reflect(discRigidbody.velocity, collision.contacts[0].normal);
            reflectedVelocity *= bounceDamping;
            discRigidbody.velocity = reflectedVelocity + (collision.contacts[0].normal * bounceForce);
        }
    }
}
