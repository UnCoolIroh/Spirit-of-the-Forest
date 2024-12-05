using UnityEngine;

public class HiveSpawner : MonoBehaviour
{
    // Spawning
    public GameObject flyingEnemyPrefab; // Prefab for the flying enemy
    public float spawnInterval = 3f; // Time between spawns
    public int maxSpawnedEnemies = 15; // Maximum enemies a hive can spawn
    private int currentSpawnedEnemies = 0;

    // Health
    public int maxHealth = 20; // Max HP for the hive
    private int currentHealth;

    private Animator animator;
    private float spawnTimer;

    void Start()
    {
        currentHealth = maxHealth; // Initialize hive health
        animator = GetComponent<Animator>(); // Assumes an Animator is attached to the hive

    }

    void Update()
    {
        if (currentSpawnedEnemies < maxSpawnedEnemies)
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer >= spawnInterval)
            {
                SpawnEnemy();
                spawnTimer = 0f;
            }
        }
    }

    private void SpawnEnemy()
    {
		animator.SetTrigger("Spawn");
        // Spawn a flying enemy at hive's position
        Instantiate(flyingEnemyPrefab, transform.position, Quaternion.identity);
        currentSpawnedEnemies++;
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
        Destroy(gameObject); // Destroy the hive
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerAttack"))
        {
            TakeDamage(1); // Adjust damage value as needed
        }
    }
}