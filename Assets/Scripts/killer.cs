using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class killer : MonoBehaviour
{
    public ObjectPlsHelp objectPlsHelp;
    void OnTriggerStay(Collider collision)
    {
        if (collision.gameObject.tag == "Player" && objectPlsHelp != null)
        {
            objectPlsHelp.playerHealth -= 1f * Time.deltaTime;
        }
    }
}
