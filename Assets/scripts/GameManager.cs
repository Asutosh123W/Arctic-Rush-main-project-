using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    int score;
    public static GameManager inst;

    public Text scoreText;
    public PlayerMovement playerMovement;

    public void IncrementScore()
    {
        score++;
        scoreText.text = "SCORE: " + score;
        //increase player's speed
        playerMovement.speed += playerMovement.speedIncreasePerPoint;
    }

    private void Awake()
    {
        inst = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
