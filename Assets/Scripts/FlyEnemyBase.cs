using UnityEngine;

public class FlyingEnemy : MonoBehaviour
{
    // Movement
    public float moveSpeed = 2f;  // Speed of the enemy
    public float damageInterval = 1.5f;  // Time between damage ticks

    // Health
    public float maxHealth = 3; // Maximum health of the fly
    private float currentHealth;

    // Components
    private Transform player;
    private float damageTimer;
    private bool isInContactWithPlayer = false;
    private GameObject actualplayer;
    public GameObject deathDrop;

    void Start()
    {
        actualplayer = GameObject.FindWithTag("Player");
        player = GameObject.FindWithTag("Player").transform; // Assumes the player is tagged "Player"
        currentHealth = maxHealth; // Initialize health
    }

    void Update()
    {
        if (player != null)
        {
            // Move towards the player
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)direction * moveSpeed * Time.deltaTime;
        }

        // Handle damage-over-time (DoT) when in contact with the player
        if (isInContactWithPlayer)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer >= damageInterval)
            {
                DamagePlayer();
                damageTimer = 0f;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInContactWithPlayer = true;
            damageTimer = 0f;  // Reset the damage timer
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isInContactWithPlayer = false;
        }
    }

    private void DamagePlayer()
    {
        actualplayer.GetComponent<PlayerHealth>().Damage(3);
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Optional: Play a death animation or spawn particle effects here if we have it
        Destroy(gameObject);
        Instantiate(deathDrop, transform.position, Quaternion.identity);

    }

    private void OnBecameInvisible()
    {
        // Destroy the enemy if it goes off-screen for some reason
        Destroy(gameObject);
    }
}
