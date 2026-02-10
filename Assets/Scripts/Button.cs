using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : MonoBehaviour
{
    public string AcceptedTag = "box";

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
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (coll.gameObject.tag == AcceptedTag)
        {
            //If the GameObject has the same tag as specified, output this message in the console
            Debug.Log("Do something here");
        }
    }

    void OnCollisionExit(Collision coll)
    {
        //Check for a match with the specific tag on any GameObject that collides with your GameObject
        if (coll.gameObject.tag == AcceptedTag)
        {
            //If the GameObject has the same tag as specified, output this message in the console
            Debug.Log("Do something else here");
        }
    }
}
