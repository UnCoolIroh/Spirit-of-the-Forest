using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attack : MonoBehaviour

{
    public PlayerController player;
    public float attackPower = 10f;
    public AudioManager audioManager;
    
    public void Start() {
        audioManager = GameObject.FindGameObjectWithTag("audio").GetComponent<AudioManager>();
        player = GetComponentInParent<PlayerController>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        audioManager.playSFX(audioManager.hit);
        if (collision.tag == "fly")
        {
            FlyingEnemy health = collision.GetComponent<FlyingEnemy>();
            if (player.isBear)
            {
                health.TakeDamage(attackPower * 2.5f);
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
