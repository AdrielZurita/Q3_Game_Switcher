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
    public float spawnOffset = 0.1f;
    private Vector3 newGravityDirection;

    // Start is called before the first frame update
    void Start()
    {
        discTransform = this.transform;
        discRigidbody = GetComponent<Rigidbody>();
        discTransform.Rotate(90f, 0f, 0f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "CanWall" && objectPlsHelp.returning == false)
        {
            ContactPoint contact = collision.contacts[0];
            newGravityDirection = -contact.normal;

            Vector3 forwardOnPlane = Vector3.ProjectOnPlane(transform.forward, contact.normal);
            if (forwardOnPlane.sqrMagnitude < 0.0001f)
            {
                forwardOnPlane = Vector3.Cross(contact.normal, transform.right);
                if (forwardOnPlane.sqrMagnitude < 0.0001f)
                {
                    forwardOnPlane = Vector3.Cross(contact.normal, Vector3.up);
                }
            }
            forwardOnPlane.Normalize();

            Quaternion spawnRotation = Quaternion.LookRotation(forwardOnPlane, contact.normal);
            Vector3 spawnPosition = contact.point + contact.normal * spawnOffset;

            if (GameObject.FindWithTag("GravBox") == null)
            {
                GameObject gravBox = Instantiate(gravBoxObj, spawnPosition, spawnRotation);
            }
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
