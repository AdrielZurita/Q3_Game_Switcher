using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class checkpoint : MonoBehaviour
{
    private bool activated = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !activated)
        {
            respawnManager.UpdateCheckpoint(transform.position);
            activated = true;
        }
    }
}
