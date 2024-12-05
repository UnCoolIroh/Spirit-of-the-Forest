using UnityEngine;
using TMPro;

public class Merchant : MonoBehaviour
{
    public int healthUpgradeCost = 3; // Starting cost for health upgrades
    public int damageUpgradeCost = 3; // Starting cost for damage upgrades

    public GameObject merchantPanel; // Merchant UI Panel
    public TextMeshProUGUI gemCountText;
    public TextMeshProUGUI healthCostText;
    public TextMeshProUGUI damageCostText;

    private bool isPlayerNearby = false;
    private PlayerController playerController;
    private PlayerHealth playerHealth;
    private attack playerAttack;

    void Start()
    {
        merchantPanel.SetActive(false); // Hide UI initially
    }

    void Update()
    {
        if (isPlayerNearby && UserInput.instance.controls.Gameplay.TraversalAbility.WasPressedThisFrame()) // Interact with E key
        {
            ToggleMerchantPanel();
        }
    }

    public void ToggleMerchantPanel()
    {
        bool isActive = merchantPanel.activeSelf;
        merchantPanel.SetActive(!isActive);

        if (!isActive)
        {
            UpdateUI();
        }
    }

    public void UpgradeHealth()
    {
        if (playerController.gems >= healthUpgradeCost)
        {
            playerController.gems -= healthUpgradeCost;
            playerHealth.maxHealth += 10; // Add 10 to max health
            playerHealth.currentHealth = playerHealth.maxHealth; // Heal to full on upgrade ofc
            playerHealth.healthbar.SetMaxHealth(playerHealth.maxHealth); // Update health bar
            healthUpgradeCost++; // Increase the upgrade cost
            UpdateUI();
        }
        else
        {
            Debug.Log("Not enough gems!");
        }
    }

    public void UpgradeDamage()
    {
        if (playerController.gems >= damageUpgradeCost)
        {
            playerController.gems -= damageUpgradeCost;
            playerAttack.attackPower += 2; // Increase attack power by 2
            damageUpgradeCost++; // Increase the upgrade cost
            UpdateUI();
        }
        else
        {
            Debug.Log("Not enough gems!");
        }
    }

    private void UpdateUI()
    {
        gemCountText.text = $"Gems: {playerController.gems}";
        healthCostText.text = $"Health Upgrade: {healthUpgradeCost} Gems";
        damageCostText.text = $"Damage Upgrade: {damageUpgradeCost} Gems";
        playerController.gemText.text = playerController.gems.ToString();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            playerController = other.GetComponent<PlayerController>();
            playerHealth = other.GetComponent<PlayerHealth>();

            // Find the attack script on the attack entity
            Transform attackEntity = other.transform.Find("attack");
            if (attackEntity != null)
            {
                playerAttack = attackEntity.GetComponent<attack>();
            }

            if (playerAttack == null)
            {
                Debug.LogError("PlayerAttack component not found!");
            }
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            merchantPanel.SetActive(false);
        }
    }
}
