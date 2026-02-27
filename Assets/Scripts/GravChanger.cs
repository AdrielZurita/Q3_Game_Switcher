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
    public float rotationSpeed = 150f;
    public float repulsionForce = 800f;
    public LayerMask physObjectLayer;
    private List<Transform> trackedControllers = new List<Transform>();
    private bool isExitingGravBox = false;
    private Quaternion targetExitRotation = Quaternion.Euler(0, 0, 0);

    void Start()
    {
        if (objectPlsHelp != null)
        {
            repulsionForce = objectPlsHelp.bounciness * objectPlsHelp.chargeAmount;
        }
        if (objectPlsHelp != null && !objectPlsHelp.isPositive)
        {
            objectPlsHelp.chargeAmount = 1f;
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
        // Handle smooth rotation when exiting gravity box
        if (isExitingGravBox && playerTransform != null)
        {
            playerTransform.rotation = Quaternion.RotateTowards(playerTransform.rotation, targetExitRotation, rotationSpeed * Time.deltaTime);
            
            // Check if rotation is complete
            if (Quaternion.Angle(playerTransform.rotation, targetExitRotation) < 0.1f)
            {
                playerTransform.rotation = targetExitRotation;
                isExitingGravBox = false;
            }
        }
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
            plyrRb.AddForce(repulsionDirection * repulsionForce);
        }
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
                objectInBox = collision.gameObject;
            }
            else
            {
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
            isExitingGravBox = true;
            targetExitRotation = Quaternion.Euler(0, 0, 0);
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
            plyrRb.AddForce(repulsionDirection * repulsionForce);
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
        Transform ctrl = t.Find("Box Grav controller");
        if (ctrl != null) return ctrl;
        ctrl = t.Find("Grav controller");
        if (ctrl != null) return ctrl;
        if (t.parent != null)
        {
            ctrl = t.parent.Find("Box Grav controller");
            if (ctrl != null) return ctrl;
            ctrl = t.parent.Find("Grav controller");
            if (ctrl != null) return ctrl;
        }
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
