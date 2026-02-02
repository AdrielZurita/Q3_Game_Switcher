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

    // Start is called before the first frame update
    void Start()
    {
        discTransform = this.transform;
        discRigidbody = GetComponent<Rigidbody>();
        discTransform.Rotate(90f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "CanWall" && objectPlsHelp.returning == false)
        {
            Debug.Log("Hit Wall");
            Destroy(this.gameObject);
            GameObject gravBox = Instantiate(gravBoxObj, transform.position, transform.rotation);
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
