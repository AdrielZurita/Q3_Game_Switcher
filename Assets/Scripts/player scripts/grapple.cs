using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class grapple : MonoBehaviour
{
    public Vector3 grapplePoint;
    float pullSpeed = 50f;
    public ObjectPlsHelp objectPlsHelp;
    public Vector3 pullDirection;
    GameObject player;
    void Update()
    {
        if (objectPlsHelp.beingPulled)
        {
            player.transform.position += pullDirection * pullSpeed * Time.deltaTime;
        }
    }
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "grapplePart")
        {
            objectPlsHelp.beingPulled = false;
        }
    }
}
