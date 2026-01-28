using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowDisc : MonoBehaviour
{
    public GameObject discObj;
    public float throwVelocity = 700f;
    public bool havedisc = true;
    public Transform Transform;

    void Update()
    {
        if (Input.GetButtonDown("Fire1") && havedisc == true)
        {
            GameObject disc = Instantiate(discObj, transform.position, transform.rotation);
            disc.GetComponent<Rigidbody>().AddRelativeForce(new Vector3(0, throwVelocity, 0));
            havedisc = false;
        }
        else if ((Input.GetButtonDown("Fire1") && havedisc == false) || (GameObject.FindWithTag("Disc") != null && (Mathf.Abs(GameObject.FindWithTag("Disc").transform.position.x - transform.position.x) > 50f || Mathf.Abs(GameObject.FindWithTag("Disc").transform.position.z - transform.position.z) > 50f)))
        {
            havedisc = true;
            GameObject disc = GameObject.FindWithTag("Disc");
            if (disc != null)
                Destroy(disc);
            GameObject gravBox = GameObject.FindWithTag("GravBox");
            if (gravBox != null)
                Destroy(gravBox);
        }
    }
}
