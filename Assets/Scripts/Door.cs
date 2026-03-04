using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [Header("Connect to button plate")]
    public GameObject button;
    [Header("preset don't touch")]
    public Transform door;
    public Transform closedPosition;
    public Transform openPosition;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (button.GetComponent<Button>().Activated)
        {
            door.transform.position = Vector3.MoveTowards(door.transform.position, openPosition.position, 5f * Time.deltaTime);
            //opening animation here when implemented
        }
        else
        {
            door.transform.position = Vector3.MoveTowards(door.transform.position, closedPosition.position, 5f * Time.deltaTime);
            //closing animation here when implemented

        }
    }
}
