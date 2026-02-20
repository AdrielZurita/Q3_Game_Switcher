using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthDeath : MonoBehaviour
{
    [SerializeField] ObjectPlsHelp objectPlsHelp;
    public float maxHealth = 100f;
    public float regenRate = 5f;
    bool isDead = false;
    [Header("Damage/regen")]
    public float damageGracePeriod = 1.0f;
    private float lastDamageTime = -999f;
    private float prevHealth = -1f;
    // Start is called before the first frame update
    void Start()
    {
        if (objectPlsHelp == null)
        {
            var player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                objectPlsHelp = player.GetComponent<ObjectPlsHelp>();
            }
        }
        if (objectPlsHelp != null)
        {
            prevHealth = objectPlsHelp.playerHealth;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (objectPlsHelp == null) return;

        // clamp health
        objectPlsHelp.playerHealth = Mathf.Clamp(objectPlsHelp.playerHealth, 0f, maxHealth);

        // death handling (only once)
        if (!isDead && objectPlsHelp.playerHealth <= 0f)
        {
            isDead = true;
            // put death stuff here
            Debug.Log("Player has died.");
        }

        // detect recent damage (compare previous frame health)
        if (prevHealth < 0f) prevHealth = objectPlsHelp.playerHealth;
        if (objectPlsHelp.playerHealth < prevHealth)
        {
            lastDamageTime = Time.time;
        }

        // passive regen when alive and not recently damaged
        if (!isDead && objectPlsHelp.playerHealth < maxHealth && Time.time - lastDamageTime > damageGracePeriod)
        {
            objectPlsHelp.playerHealth += regenRate * Time.deltaTime;
            objectPlsHelp.playerHealth = Mathf.Min(objectPlsHelp.playerHealth, maxHealth);
        }

        prevHealth = objectPlsHelp.playerHealth;
    }
}
