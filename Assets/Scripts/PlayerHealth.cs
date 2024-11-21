using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public float health;
    public PlayerController player;

    public void Damage(float damage)
    {
        if (player.isBear)
        {
            damage *= .6f;
        }
        this.health -= damage;
        print("Player health: " + health);
        if (health < 0)
        {
            Destroy(player);
        }
    }
}
