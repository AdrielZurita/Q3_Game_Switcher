using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public string AcceptedTag = "box";
    public bool Activated;
    public bool Inverted = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision coll)
    {
        if (coll.gameObject.tag == AcceptedTag)
        {
            Activated = !Inverted;
        }
    }

    void OnCollisionExit(Collision coll)
    {
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (coll.gameObject.tag == AcceptedTag)
        {
            Activated = Inverted;
        }
    }
}
