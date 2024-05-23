using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Coin : MonoBehaviour
{
    public float turnSpeed = 90f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Obstacle>() != null)
        {
            Destroy(gameObject);
            return;
        }
        //check if the coin collided with the player
        if (other.gameObject.name != "Player")
        {
            return;
        }
        //add to the player's score
        GameManager.inst.IncrementScore();

        //Destroy this coin object
        FindObjectOfType<AudioManager>().PlaySound("PickupCoin");
        Destroy(gameObject);
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, turnSpeed * Time.deltaTime);
    }
}
