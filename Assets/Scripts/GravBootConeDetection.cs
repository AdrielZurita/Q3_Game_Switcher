using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravBootConeDetection : MonoBehaviour
{
    private Vector3 newGravityDirection;
    GravBootPlayerAffector handlingScript;
    public Quaternion FinalRotation;

    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (handlingScript.isAvailable)
        {
            ContactPoint contact = collision.contacts[0];
            newGravityDirection = -contact.normal;
            Vector3 up = contact.normal.normalized;
            Vector3 forwardOnPlane = Vector3.ProjectOnPlane(transform.forward, up);
            if (forwardOnPlane.sqrMagnitude < 0.0001f)
            {
                forwardOnPlane = Vector3.ProjectOnPlane(transform.right, up);
                if (forwardOnPlane.sqrMagnitude < 0.0001f)
                    {
                        forwardOnPlane = Vector3.ProjectOnPlane(Vector3.forward, up);
                    }
            }
            forwardOnPlane.Normalize();
            Quaternion spawnRotation = Quaternion.LookRotation(forwardOnPlane, up);
            //Vector3 spawnPosition = contact.point + up * spawnOffset;
            Quaternion finalRotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
        }
        
    }
}
