using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{

public GameObject button;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (button.GetComponent<Button>().Activated)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z), 2f * Time.deltaTime);
            //openinganimation here when implemented
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y - 5f, transform.position.z), 2f * Time.deltaTime);
            //closing animation here when implemented

        }
    }
}
