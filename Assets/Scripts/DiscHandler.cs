using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiscHandler : MonoBehaviour
{
    public GameObject gravBoxObj;
    private Transform discTransform;
    public bool returning = false;
    public ObjectPlsHelp objectPlsHelp;
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
                objectPlsHelp.returning = true;
            }
        }
}
