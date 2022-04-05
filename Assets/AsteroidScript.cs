using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidScript : MonoBehaviour
{
    [SerializeField] int health = 3;
    [SerializeField] CharacterScript player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //This adds am to this character's health. If health is less than 0 destroy this object.
    public void changeHealth(int am)
    {
        health += am;
        if (health < 0)
        {
            player.addToInventory(gameObject.GetComponent<Item>());
            Destroy(this.gameObject);
        }
    }

    //This function runs on colliding with another object with a collider
    private void OnCollisionEnter(Collision collision)
    {
        //If that object has the bullet tag then remove 1 health from this ship.
        if (collision.gameObject.tag == "PlayerBullet")
        {
            changeHealth(-1);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
        }
    }
}
