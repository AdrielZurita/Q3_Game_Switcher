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
    public float rotationSpeed = 200f;
    public float repulsionForce = 800f;
    public LayerMask physObjectLayer;
    // track affected controllers so we can reset them if this GravChanger is destroyed
    private List<Transform> trackedControllers = new List<Transform>();

    void Start()
    {
        if (objectPlsHelp != null)
        {
            repulsionForce = objectPlsHelp.bounciness;
        }
        var playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            plyrRb = playerObj.GetComponent<Rigidbody>();
            playerTransform = playerObj.transform;
        }
        boxTransform = this.transform;
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && objectPlsHelp.isPositive == true)
        {
            objectPlsHelp.inGravBox = true;
        }
        else if (collision.gameObject.tag == "Player" && objectPlsHelp.isPositive == false)
        {
            Vector3 repulsionDirection = collision.transform.position - transform.position;
            repulsionDirection.Normalize();
            plyrRb.AddForce(repulsionDirection * repulsionForce * objectPlsHelp.chargeAmount);
        }
        // Handle physical objects (boxes, turrets, etc.) by layer mask
        if ((physObjectLayer.value & (1 << collision.gameObject.layer)) != 0)
        {
            if (objectPlsHelp != null && objectPlsHelp.isPositive)
            {
                Transform ctrl = FindControllerTransform(collision.transform);
                if (ctrl != null)
                {
                    ctrl.rotation = this.transform.rotation;
                    if (!trackedControllers.Contains(ctrl)) trackedControllers.Add(ctrl);
                }
                else
                {
                    collision.gameObject.transform.rotation = this.transform.rotation;
                    if (!trackedControllers.Contains(collision.transform)) trackedControllers.Add(collision.transform);
                }
                // keep compatibility: store one object reference
                objectInBox = collision.gameObject;
            }
            else
            {
                // negative: push physical objects away
                Vector3 repulsionDirection = collision.transform.position - transform.position;
                repulsionDirection.Normalize();
                Rigidbody boxRb = collision.gameObject.GetComponent<Rigidbody>();
                if (boxRb != null)
                {
                    boxRb.AddForce(repulsionDirection * repulsionForce);
                }
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
        if ((physObjectLayer.value & (1 << collision.gameObject.layer)) != 0 && objectPlsHelp != null && objectPlsHelp.isPositive)
        {
            Transform ctrl = FindControllerTransform(collision.transform);
            if (ctrl != null)
            {
                ctrl.rotation = Quaternion.Euler(0, 0, 0);
                trackedControllers.Remove(ctrl);
            }
            else
            {
                collision.gameObject.transform.rotation = Quaternion.Euler(0, 0, 0);
                trackedControllers.Remove(collision.transform);
            }
            // attempt to re-enable physics on the object
            Rigidbody rb = collision.gameObject.GetComponentInParent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
                rb.freezeRotation = false;
            }
        }
    }

    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && objectPlsHelp.isPositive == false)
        {
            Vector3 repulsionDirection = collision.transform.position - transform.position;
            repulsionDirection.Normalize();
            plyrRb.AddForce(repulsionDirection * repulsionForce * objectPlsHelp.chargeAmount);
            objectPlsHelp.chargeAmount -= 10f * Time.deltaTime;
        }
        if ((physObjectLayer.value & (1 << collision.gameObject.layer)) != 0 && objectPlsHelp != null && objectPlsHelp.isPositive == false)
        {
            Vector3 repulsionDirection = collision.transform.position - transform.position;
            repulsionDirection.Normalize();
            Rigidbody boxRb = collision.gameObject.GetComponent<Rigidbody>();
            if (boxRb != null)
            {
                boxRb.AddForce(repulsionDirection * repulsionForce);
            }
        }
        if (collision.gameObject.tag == "Player" && objectPlsHelp.inGravBox)
        {
            if (playerTransform.rotation != this.boxTransform.rotation)
            {
                playerTransform.rotation = Quaternion.RotateTowards(playerTransform.rotation, this.boxTransform.rotation, rotationSpeed * Time.deltaTime);
            }
        }
    }

    private Transform FindControllerTransform(Transform t)
    {
        if (t == null) return null;
        // try immediate children
        Transform ctrl = t.Find("Box Grav controller");
        if (ctrl != null) return ctrl;
        ctrl = t.Find("Grav controller");
        if (ctrl != null) return ctrl;
        // try parent (some prefabs put the controller on the parent)
        if (t.parent != null)
        {
            ctrl = t.parent.Find("Box Grav controller");
            if (ctrl != null) return ctrl;
            ctrl = t.parent.Find("Grav controller");
            if (ctrl != null) return ctrl;
        }
        // nothing found
        return null;
    }

    void OnDisable()
    {
        ResetTrackedControllers();
    }

    void OnDestroy()
    {
        ResetTrackedControllers();
    }

    private void ResetTrackedControllers()
    {
        foreach (var ctrl in trackedControllers)
        {
            if (ctrl == null) continue;
            // try to reset controller rotation
            ctrl.rotation = Quaternion.Euler(0, 0, 0);
            // try to re-enable physics on parent object
            Rigidbody rb = ctrl.GetComponentInParent<Rigidbody>();
            if (rb != null)
            {
                rb.isKinematic = false;
                rb.useGravity = true;
                rb.freezeRotation = false;
            }
        }
        trackedControllers.Clear();
    }

    public void FaceTowardsWall(RaycastHit hit)
    {
        Vector3 targetDirection = Vector3.zero;
        targetDirection = hit.point - transform.position;
        transform.rotation = Quaternion.LookRotation(-hit.normal, transform.up);
    }
}
