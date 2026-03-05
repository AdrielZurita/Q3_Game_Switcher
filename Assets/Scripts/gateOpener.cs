using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gateOpener : MonoBehaviour
{
    // right now, the opening animation automatically plays and loops. change it so that collision plays it once and never again, there are two objects that open with seperate animators
    private Animator leftGateAnimator;
    private Animator rightGateAnimator;
    private bool gateOpened = false;
    void Start()
    {
        leftGateAnimator = transform.Find("LeftGate").GetComponent<Animator>();
        rightGateAnimator = transform.Find("RightGate").GetComponent<Animator>();
    }
    
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (!gateOpened)
            {
                leftGateAnimator.SetTrigger("Open");
                rightGateAnimator.SetTrigger("Open");
                gateOpened = true;
            }
        }
    }
}
