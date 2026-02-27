using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turret : MonoBehaviour
{
    public bool playerInRange = false;
    public bool shooting = false;
    public ObjectPlsHelp objectPlsHelp;

    [Header("Shooting")]
    public float damageAmount = 10f;
    public float prepTime = 1.5f;
    public float fireInterval = 0.5f;
    
    [Header("Laser Setup")]
    public LineRenderer lineRenderer;
    public float laserDistance = 100f;
    public float startOffset = 0.5f;


    private GameObject player;
    Coroutine prepCoroutine;
    Coroutine shootCoroutine;

    void Start()
    {
        playerInRange = false;
        shooting = false;
        player = GameObject.FindGameObjectWithTag("Player");
        if (lineRenderer != null) lineRenderer.enabled = false;
    }

    void Update()
{
    if (playerInRange && lineRenderer != null)
    {
        Vector3 rayOrigin = transform.position + (transform.forward * startOffset);
        Vector3 directionToPlayer = (player.transform.position - rayOrigin).normalized;

        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, transform.position);

        if (Physics.Raycast(rayOrigin, directionToPlayer, out RaycastHit hit, laserDistance))
        {
            lineRenderer.SetPosition(1, hit.point);
        }
        else
        {
            lineRenderer.SetPosition(1, rayOrigin + (directionToPlayer * laserDistance));
        }
    }
}

    void UpdateLaser()
    {
        lineRenderer.enabled = true; 
        lineRenderer.SetPosition(0, transform.position); 

        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
        Ray ray = new Ray(transform.position, directionToPlayer);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, laserDistance))
        {
            lineRenderer.SetPosition(1, hit.point); // End at whatever we hit
        }
        else
        {
            lineRenderer.SetPosition(1, transform.position + (directionToPlayer * laserDistance));
        }
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
        // Optional: Change laser color to yellow/red here to signal "Prepping"
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
            Vector3 rayOrigin = transform.position + (transform.forward * startOffset);
            Vector3 directionToPlayer = (player.transform.position - rayOrigin).normalized;

            if (Physics.Raycast(rayOrigin, directionToPlayer, out RaycastHit hit, laserDistance))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    objectPlsHelp.playerHealth -= damageAmount;
                    Debug.Log("Direct hit on Player!");
                }
                else
                {
                    Debug.Log("Laser blocked by: " + hit.collider.name);
                }
            }
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
            if (lineRenderer != null) lineRenderer.enabled = false; // Hide laser

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
