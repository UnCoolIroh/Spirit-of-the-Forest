using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;
    public PlayerController player;
    public Healthbar healthbar;
    private void Start()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }
    public void Damage(float damage)
    {
        if (player.isAlive)
        {
            if (player.isBear)
            {
                damage *= .6f;
            }
            this.currentHealth -= damage;
            healthbar.SetHealth(currentHealth);
            if (currentHealth < 0)
            {
                player.isAlive = false;
            }
        }
    }

    public void healthReset()
    {
        currentHealth = maxHealth;
        healthbar.SetMaxHealth(maxHealth);
    }
}
