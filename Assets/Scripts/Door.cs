using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Connect to button plate")]
    public GameObject button;
    //public AudioClip openingSound;
    //public AudioClip closingSound;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (button.GetComponent<Button>().Activated)
        {
            //AudioSource.PlayClipAtPoint(openingSound, transform.position);
            //trigger opening animation here when implemented
        }
        else
        {
            //AudioSource.PlayClipAtPoint(closingSound, transform.position);
            //trigger closing animation here when implemented

        }
    }
}
