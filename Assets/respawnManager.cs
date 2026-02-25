using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawnManager : MonoBehaviour
{
    public static Vector3 lastCheckpointPos;
    private GameObject player;
    public ObjectPlsHelp objectPlsHelp;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (lastCheckpointPos == Vector3.zero)
        {
            lastCheckpointPos = player.transform.position;
        }
    }

    public void RespawnPlayer()
    {
        if (player != null)
        {
            player.transform.position = lastCheckpointPos;
            objectPlsHelp.playerHealth = objectPlsHelp.maxHealth;
            objectPlsHelp.canMove = true;
            objectPlsHelp.havedisc = true;
            objectPlsHelp.returning = false;
            objectPlsHelp.inGravBox = false;
            objectPlsHelp.isPositive = true;
            objectPlsHelp.canThrow = true;
            // if disc or Gravbox destroy them
            GameObject disc = GameObject.FindWithTag("Disc");
            if (disc != null)
            {
                Destroy(disc);
            }
            GameObject gravBox = GameObject.FindWithTag("GravBox");
            if (gravBox != null)
            {
                Destroy(gravBox);
            }
        }
    }

    public static void UpdateCheckpoint(Vector3 newPos)
    {
        lastCheckpointPos = newPos;
    }
}
