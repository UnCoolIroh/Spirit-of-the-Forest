using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour

{
    public PlayerController player;
    public float attackPower = 5f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "enemy")
        {
            EnemyHealth health = collision.GetComponent<EnemyHealth>();
            if (player.isBear)
            {
                health.Damage(attackPower * 2);
            }
            else
            {
                health.Damage(attackPower);
            }
        }
    }
}
