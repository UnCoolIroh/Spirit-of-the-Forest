using System.Collections;
using UnityEngine;

public class SpiderEnemy : MonoBehaviour
{
    // Movement
    public float patrolSpeed = 2f;
    public float chaseSpeed = 5f;
    public float chargeSpeed = 10f;

    // Detection
    public float detectionRadius = 5f;
    public float chargeCooldown = 2f;

    // Attack
    public float chargeUpTime = 1f;

    // Health
    public int maxHealth = 15;
    private int currentHealth;

    // Components
    private Transform player;
    private Rigidbody2D rb;
    private Animator animator;
    private bool isCharging = false;
    private bool isOnCooldown = false;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform; // Assumes the player has the tag "Player"
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>(); // Assumes an Animator is attached to the same GameObject
        currentHealth = maxHealth; // Initialize health
    }

    void Update()
    {
        if (isCharging || isOnCooldown) return;

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= detectionRadius)
        {
            StartCoroutine(ChargeAttack());
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        // Play Idle animation during patrol
        animator.SetTrigger("Idle");

        // Simple back-and-forth movement logic
        rb.velocity = new Vector2(Mathf.PingPong(Time.time * patrolSpeed, 1) * 2 - 1, rb.velocity.y);
    }

    private IEnumerator ChargeAttack()
    {
        isCharging = true;

        // Trigger Charge-Up animation
        animator.SetTrigger("ChargeUp");

        // Face the player
        Vector2 directionToPlayer = (player.position - transform.position).normalized;

        // Wait for the charge-up animation to finish
        yield return new WaitForSeconds(chargeUpTime);

        // Trigger Charge animation and launch toward the player
        animator.SetTrigger("Charge");
        rb.velocity = directionToPlayer * chargeSpeed;

        // Wait for the cooldown after charging
        isOnCooldown = true;
        yield return new WaitForSeconds(chargeCooldown);

        // Reset to Idle state
        isCharging = false;
        isOnCooldown = false;
        animator.SetTrigger("Idle");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // Damage the player
            collision.gameObject.GetComponent<PlayerHealth>()?.Damage(1);

            // Trigger optional Impact animation
            animator.SetTrigger("Impact");

            // Stop movement after hitting the player
            rb.velocity = Vector2.zero;
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Trigger death animation (if we have one available)
        animator.SetTrigger("Die");

        // Destroy the spider after death animation (adjust delay for animation length)
        Destroy(gameObject, 0.5f);
    }
}