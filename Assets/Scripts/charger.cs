using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charger : MonoBehaviour
{
    public ObjectPlsHelp objectPlsHelp;
    // Start is called before the first frame update
    void Start()
    {
        objectPlsHelp.chargeAmount = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (objectPlsHelp.chargeAmount > 2.5f)
        {
            objectPlsHelp.chargeAmount = 2.5f;
        }
        if (objectPlsHelp.chargeAmount < 1f)
        {
            objectPlsHelp.chargeAmount = 1f;
        }
    }
    void OnCollisionStay(Collision coll)
    {
        if (coll.gameObject.tag == "Player")
        {
            objectPlsHelp.chargeAmount += 1f * Time.deltaTime;
        }
    }
}
