using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoost : MonoBehaviour
{
    public float boostAmount = 2f; // Speed boost multiplier
    public float duration = 5f; // Duration of the speed boost

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            ApplySpeedBoost(other.gameObject.GetComponent<PlayerMovement>());
            gameObject.SetActive(false); // Disable the speed boost object
        }
    }

    private void ApplySpeedBoost(PlayerMovement player)
    {
        // Apply speed boost effect
        player.ApplySpeedBoost(boostAmount);

        // Start a coroutine to revert the speed boost after the duration
        StartCoroutine(RemoveSpeedBoostAfterDelay(player));
    }

    private IEnumerator RemoveSpeedBoostAfterDelay(PlayerMovement player)
    {
        // Wait for the duration of the speed boost
        yield return new WaitForSeconds(duration);

        // Revert the speed back to normal after the duration
        player.RemoveSpeedBoost(boostAmount);
    }
}
