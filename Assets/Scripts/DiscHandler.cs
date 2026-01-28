using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscHandler : MonoBehaviour
{
    public GameObject gravBoxObj;
    public Transform discTransform;
    // Start is called before the first frame update
    void Start()
    {
        discTransform = this.transform;
        discTransform.Rotate(90f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "CanWall")
            {
                Debug.Log("Hit Wall");
                Destroy(this.gameObject);
                GameObject gravBox = Instantiate(gravBoxObj, transform.position, transform.rotation);
            }
        }
}
