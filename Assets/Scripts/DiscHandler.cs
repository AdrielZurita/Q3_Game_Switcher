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

            // Use the contact normal as the 'up' for the spawned box to remove unpredictable rolls.
            Vector3 up = contact.normal.normalized;

            // Choose a stable forward vector by projecting a preferred reference onto the plane.
            Vector3 forwardOnPlane = Vector3.ProjectOnPlane(transform.forward, up);
            if (forwardOnPlane.sqrMagnitude < 0.0001f)
            {
                // fallback to transform.right if forward is nearly parallel to the normal
                forwardOnPlane = Vector3.ProjectOnPlane(transform.right, up);
                if (forwardOnPlane.sqrMagnitude < 0.0001f)
                {
                    // final fallback to world forward
                    forwardOnPlane = Vector3.ProjectOnPlane(Vector3.forward, up);
                }
            }
            forwardOnPlane.Normalize();

            // LookRotation(forward, up) produces a deterministic rotation with 'up' aligned to surface normal
            Quaternion spawnRotation = Quaternion.LookRotation(forwardOnPlane, up);
            Vector3 spawnPosition = contact.point + up * spawnOffset;

            if (GameObject.FindWithTag("GravBox") == null)
            {
                // Compute a rotation that aligns the prefab's local axes to the desired surface axes.
                // This compensates for any rotation baked into the prefab so the spawned box
                // will have its local up aligned to the surface normal and its local forward
                // aligned to the chosen forward direction on the plane.
                Vector3 upWards = contact.normal.normalized;
                Quaternion targetRotation = spawnRotation; // LookRotation(forwardOnPlane, up)

                // Prefab's authored world rotation (may include offsets). Use inverse to cancel it.
                Quaternion prefabRot = gravBoxObj.transform.rotation;
                Quaternion finalRotation = targetRotation * Quaternion.Inverse(prefabRot);

                GameObject gravBox = Instantiate(gravBoxObj, spawnPosition, finalRotation);

                Collider boxCol = gravBox.GetComponent<Collider>();
                if (boxCol != null)
                {
                    const int maxAttempts = 12;
                    const float step = 0.05f;
                    Physics.SyncTransforms();
                    for (int i = 0; i < maxAttempts; i++)
                    {
                        Vector3 center = boxCol.bounds.center;
                        Vector3 halfExtents = boxCol.bounds.extents * 0.98f;
                        Collider[] hits = Physics.OverlapBox(center, halfExtents, gravBox.transform.rotation, ~0, QueryTriggerInteraction.Ignore);
                        bool overlapping = false;
                        foreach (var hit in hits)
                        {
                            if (hit != boxCol && !hit.transform.IsChildOf(gravBox.transform))
                            {
                                overlapping = true;
                                break;
                            }
                        }

                        if (!overlapping) break;
                        gravBox.transform.position += contact.normal * step;
                        Physics.SyncTransforms();
                    }
                }
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
