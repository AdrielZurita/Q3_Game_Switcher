using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret : MonoBehaviour
{
    public bool playerInRange = false;
    public bool shooting = false;
    [SerializeField] ObjectPlsHelp objectPlsHelp;
    [Header("Shooting")]
    public float damageAmount = 10f;
    public float prepTime = 1.5f;
    public float fireInterval = 0.5f;

    Coroutine prepCoroutine;
    Coroutine shootCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        playerInRange = false;
        shooting = false;
        if (objectPlsHelp == null)
        {
            var player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                objectPlsHelp = player.GetComponent<ObjectPlsHelp>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = true;
            if (prepCoroutine == null)
                prepCoroutine = StartCoroutine(PrepToShootAtPlayer());
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (shooting && shootCoroutine == null)
            {
                shootCoroutine = StartCoroutine(ShootAtPlayer());
            }
        }
    }

    public IEnumerator PrepToShootAtPlayer()
    {
        // put alert noise here
        yield return new WaitForSeconds(prepTime);
        prepCoroutine = null;
        if (playerInRange)
        {
            shooting = true;
        }
    }
    public IEnumerator ShootAtPlayer()
    {
        while (playerInRange)
        {
            if (objectPlsHelp != null)
            {
                // put shoot noise here
                objectPlsHelp.playerHealth -= damageAmount;
            }
            yield return new WaitForSeconds(fireInterval);
        }
        shooting = false;
        shootCoroutine = null;
    }

    void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            playerInRange = false;
            if (prepCoroutine != null)
            {
                StopCoroutine(prepCoroutine);
                prepCoroutine = null;
            }
            if (shootCoroutine != null)
            {
                StopCoroutine(shootCoroutine);
                shootCoroutine = null;
            }
            shooting = false;
        }
    }

    void OnDisable()
    {
        if (prepCoroutine != null) StopCoroutine(prepCoroutine);
        if (shootCoroutine != null) StopCoroutine(shootCoroutine);
    }
}
