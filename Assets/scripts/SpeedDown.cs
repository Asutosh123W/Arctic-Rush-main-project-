using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedDown : MonoBehaviour
{
    public float speedReductionAmount = 0.5f; // Speed reduction multiplier
    public float duration = 5f; // Duration of the speed reduction

    public int maxHealth = 3;
    public int currentHealth;
    public HealthBar healthBar;

    private void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ApplySpeedReduction(other.gameObject.GetComponent<PlayerMovement>());
            gameObject.SetActive(false); // Disable the speed down object
            TakeDamage(1);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        healthBar.SetHealth(currentHealth);

        // Ensure health doesn't go below 0
        currentHealth = Mathf.Max(currentHealth, 0);
    }

    private void ApplySpeedReduction(PlayerMovement player)
    {
        // Apply speed reduction effect
        player.ApplySpeedReduction(speedReductionAmount);

        // Start a coroutine to revert the speed reduction after the duration
        StartCoroutine(RemoveSpeedReductionAfterDelay(player));
    }

    private IEnumerator RemoveSpeedReductionAfterDelay(PlayerMovement player)
    {
        // Wait for the duration of the speed reduction
        yield return new WaitForSeconds(duration);

        // Revert the speed back to normal after the duration
        player.RemoveSpeedReduction(speedReductionAmount);
    }
}
