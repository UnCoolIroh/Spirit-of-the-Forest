using System.Collections;
using UnityEngine;

public class BurrowingWorm : MonoBehaviour
{
    // Worm Behavior
    public float aboveGroundDuration = 4f; // Time above ground before burrowing
    public float belowGroundDuration = 2f; // Time below ground before unburrowing
    public float attackDelay = 2f; // Time after unburrowing before attacking
    public GameObject bulletPrefab; // Bullet prefab for shooting
    public float bulletSpeed = 5f; // Speed of the bullets
    public int bulletCount = 3; // Number of bullets per burst
    public Transform[] validPositions; // Possible unburrowing positions

    // HP and Damage
    public int maxHealth = 5; // Max HP
    private int currentHealth;

    // Components
    private Animator animator; // Animator for burrowing/unburrowing animations
    private bool isBurrowed = false; // Whether the worm is burrowed
    private Transform player; // Reference to the player
    public GameObject deathDrop;

    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindWithTag("Player").transform; // Assumes the player is tagged "Player"
        currentHealth = maxHealth; // Initialize health
        StartCoroutine(WormLifecycle());
    }

    private IEnumerator WormLifecycle()
    {
        while (true)
        {
            // Above ground phase
            isBurrowed = false;
            animator.SetTrigger("Unburrow");
            yield return new WaitForSeconds(aboveGroundDuration);

            // Burrow phase
            animator.SetTrigger("Burrow");
            yield return new WaitForSeconds(1f); // Give time for the burrowing animation
            isBurrowed = true;

            // Choose a new random position to unburrow
            Transform targetPosition = validPositions[Random.Range(0, validPositions.Length)];
            transform.position = targetPosition.position;

            // Wait below ground before unburrowing
            yield return new WaitForSeconds(belowGroundDuration);

            // Unburrow
            animator.SetTrigger("Unburrow");
            isBurrowed = false;
            yield return new WaitForSeconds(1f); // Time for unburrowing animation

            // Wait and attack
            yield return new WaitForSeconds(attackDelay);
            ShootBurst();

            // Repeat cycle
        }
    }

    private void ShootBurst()
    {
        // Fire a burst of bullets in the player's direction
        Vector2 directionToPlayer = (player.position - transform.position).normalized;

        for (int i = 0; i < bulletCount; i++)
        {
            // Spread bullets slightly by modifying their angle
            float angleOffset = -10f + i * 10f; // Spread bullets across a small arc
            Vector2 bulletDirection = Quaternion.Euler(0, 0, angleOffset) * directionToPlayer;

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.velocity = bulletDirection * bulletSpeed;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isBurrowed) return; // Worm can't take damage while burrowed

        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        animator.SetTrigger("Die"); // Trigger death animation
        Destroy(gameObject, 1f); // Wait for animation to finish before destroying
        Instantiate(deathDrop, transform.position, Quaternion.identity);   
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Damage the player (Assumes the player has a PlayerHealth script)
            collision.gameObject.GetComponent<PlayerHealth>()?.Damage(1);
        }
    }
}