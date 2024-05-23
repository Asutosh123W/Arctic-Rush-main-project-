using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FadeInScript : MonoBehaviour
{
    public float fadeInDuration = 8f; // Duration for the fade-in effect
    private TextMeshProUGUI textMeshProText;
    private float currentFadeTime = 0f;

    void Start()
    {
        textMeshProText = GetComponent<TextMeshProUGUI>();
        textMeshProText.color = new Color(textMeshProText.color.r, textMeshProText.color.g, textMeshProText.color.b, 0f); // Set initial alpha to 0
    }

    void Update()
    {
        if (currentFadeTime < fadeInDuration)
        {
            currentFadeTime += Time.deltaTime;
            float alpha = Mathf.Clamp01(currentFadeTime / fadeInDuration); // Calculate alpha based on current fade time and total duration
            textMeshProText.color = new Color(textMeshProText.color.r, textMeshProText.color.g, textMeshProText.color.b, alpha); // Set text alpha
        }
    }
}
