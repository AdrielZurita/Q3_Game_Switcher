using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowDisc : MonoBehaviour
{
    public GameObject discObj;
    public float throwVelocity = 700f;
    public float returnVelocity = 500f;
    public ObjectPlsHelp objectPlsHelp;
    private bool discRotatedForReturn = false;
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
        if (Input.GetButtonDown("Fire1") && objectPlsHelp.havedisc == true)
        {
            GameObject disc = Instantiate(discObj, transform.position, transform.rotation);
            disc.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, throwVelocity, 0));
            objectPlsHelp.havedisc = false;
        }
        else if ((Input.GetButtonDown("Fire1") && objectPlsHelp.havedisc == false) || (GameObject.FindWithTag("Disc") != null && (Mathf.Abs(GameObject.FindWithTag("Disc").transform.position.x - transform.position.x) > 50f || Mathf.Abs(GameObject.FindWithTag("Disc").transform.position.z - transform.position.z) > 50f)) || objectPlsHelp.returning == true)
        {
            if (GameObject.FindWithTag("Disc") != null)
            {
                GameObject discToRotate = GameObject.FindWithTag("Disc");
                discToRotate.transform.Rotate(90f, 0f, 0f);
            }
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
            disc.transform.position = Vector3.MoveTowards(disc.transform.position, this.transform.position, returnVelocity * Time.deltaTime);
            objectPlsHelp.returning = true;
            discRotatedForReturn = false;
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
                if (!discRotatedForReturn)
                {
                    disc.transform.Rotate(90f, 0f, 0f);
                    discRotatedForReturn = true;
                }
                disc.transform.position = Vector3.MoveTowards(disc.transform.position, this.transform.position, returnVelocity * Time.deltaTime);
                
                if (Vector3.Distance(disc.transform.position, this.transform.position) < 1f)
                {
                    Destroy(disc);
                    objectPlsHelp.havedisc = true;
                    objectPlsHelp.returning = false;
                    discRotatedForReturn = false;
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && objectPlsHelp.havedisc == true)
        {
            objectPlsHelp.isPositive = !objectPlsHelp.isPositive;
        }
    }
}
