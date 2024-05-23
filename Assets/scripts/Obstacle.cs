using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacle : MonoBehaviour
{
    PlayerMovement playerMovement;

    void Start()
    {
     playerMovement = GameObject.FindObjectOfType<PlayerMovement>();   
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Player")
        {
            FindObjectOfType<AudioManager>().PlaySound("GameOver");
            SceneManager.LoadScene("EndScreen");
        }
    }
    void Update()
    {
        
    }
}
