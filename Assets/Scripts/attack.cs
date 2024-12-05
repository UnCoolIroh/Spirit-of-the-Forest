using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour

{
    public PlayerController player;
    public float attackPower = 10f;
    
    public void Start() {
        player = GetComponentInParent<PlayerController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "fly")
        {
            FlyingEnemy health = collision.GetComponent<FlyingEnemy>();
            if (player.isBear)
            {
                health.TakeDamage(attackPower * 2);
            }
            else
            {
                health.TakeDamage(attackPower);
            }
        }
        if (collision.tag == "hive")
        {
            HiveSpawner health = collision.GetComponent<HiveSpawner>();
            if (player.isBear)
            {
                health.TakeDamage(attackPower * 2);
            }
            else
            {
                health.TakeDamage(attackPower);
            }
        }
        if (collision.tag == "spider")
        {
            SpiderEnemy health = collision.GetComponent<SpiderEnemy>();
            if (player.isBear)
            {
                health.TakeDamage(attackPower * 2);
            }
            else
            {
                health.TakeDamage(attackPower);
            }
        }
    }
}
