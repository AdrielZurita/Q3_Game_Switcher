using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gateOpener : MonoBehaviour
{
    [SerializeField] private Animator leftGateAnimator;
    [SerializeField] private Animator rightGateAnimator;
    public bool gateOpened = false;
    void Start()
    {
        leftGateAnimator = this.transform.Find("LeftGate").GetComponent<Animator>();
        rightGateAnimator = this.transform.Find("RightGate").GetComponent<Animator>();
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
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (gateOpened)
            {
                leftGateAnimator.SetTrigger("Close");
                rightGateAnimator.SetTrigger("Close");
                gateOpened = false;
            }
        }
    }
}
