using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowDisc : MonoBehaviour
{
    public GameObject discObj;
    public float throwVelocity = 50f;
    public float returnVelocity = 30f;
    public ObjectPlsHelp objectPlsHelp;
    public Transform playerTransform;
    void Start()
    {
        objectPlsHelp.havedisc = true;
        objectPlsHelp.returning = false;
        objectPlsHelp.inGravBox = false;
        objectPlsHelp.isPositive = true;
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && objectPlsHelp.havedisc == true)
        {
            GameObject disc = Instantiate(discObj, transform.position, transform.rotation);
            disc.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, throwVelocity, 0));
            objectPlsHelp.havedisc = false;
        }
        else if ((Input.GetMouseButtonDown(0) && objectPlsHelp.havedisc == false) || (GameObject.FindWithTag("Disc") != null && (Mathf.Abs(GameObject.FindWithTag("Disc").transform.position.x - transform.position.x) > 50f || Mathf.Abs(GameObject.FindWithTag("Disc").transform.position.z - transform.position.z) > 50f)) || objectPlsHelp.returning == true)
        {
            GameObject gravBox = GameObject.FindWithTag("GravBox");
            if (gravBox != null)
            {
                GravChanger gravChanger = gravBox.GetComponent<GravChanger>();
                if (gravChanger.objectInBox != null)
                {
                    Transform boxParent = gravChanger.objectInBox.transform.parent;
                    Transform boxCtrl = boxParent.Find("Box Grav controller");
                    if (boxCtrl != null)
                    {
                        boxCtrl.rotation = Quaternion.Euler(0, 0, 0);
                    }
                }
                Transform boxTrn = gravBox.transform;
                if (objectPlsHelp.inGravBox)
                {
                    playerTransform.rotation = Quaternion.Euler(0, 0, 0);
                }
                objectPlsHelp.inGravBox = false;
                GameObject discA = Instantiate(discObj, boxTrn.position, boxTrn.rotation);
                Destroy(gravBox);
            }
            GameObject disc = GameObject.FindWithTag("Disc");
            Rigidbody discRb = disc.GetComponent<Rigidbody>();
            if (discRb != null)
            {
                discRb.isKinematic = true;
            }
            objectPlsHelp.returning = true;
            if (objectPlsHelp.inGravBox)
            {
                playerTransform.rotation = Quaternion.Euler(0, 0, 0);
                objectPlsHelp.inGravBox = false;
            }
        }
        
        if (objectPlsHelp.returning)
        {
            GameObject disc = GameObject.FindWithTag("Disc");
            if (disc != null)
            {
                // Face the player
                disc.transform.LookAt(playerTransform.position);
                
                // Move kinematic body toward player (physics will detect collisions)
                Vector3 direction = (this.transform.position - disc.transform.position).normalized;
                disc.transform.position += direction * returnVelocity * Time.deltaTime;
                
                if (Vector3.Distance(disc.transform.position, this.transform.position) < 1f)
                {
                    Destroy(disc);
                    objectPlsHelp.havedisc = true;
                    objectPlsHelp.returning = false;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && objectPlsHelp.havedisc == true)
        {
            objectPlsHelp.isPositive = !objectPlsHelp.isPositive;
        }


        if (!objectPlsHelp.inGravBox && playerTransform.rotation != Quaternion.Euler(0, 0, 0))
        {
            playerTransform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
