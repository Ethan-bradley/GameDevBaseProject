using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEditor.Events;
using System;

public class CharacterScript : MonoBehaviour
{ 
    [SerializeField] Rigidbody m_Rigidbody;
    //Controls character speed.
    [SerializeField] float m_Speed;
    [SerializeField] int health;
    [SerializeField] int money;
    [SerializeField] int plasma;
    [SerializeField] float rotationRate;
    public Text healthText;
    [SerializeField] List<Item> inventory;
    [SerializeField] GameObject hitParticleEffect;
    public GameObject inventory_display;
    //Currently equipped items.
    [SerializeField] List<Item> equiped;
    private string temp_string;
    private int range;
    private int damage;
    public Button yourButton;
    public GameObject bullet;
    public GameObject gun;

    private void Awake()
    {
        if (FindObjectsOfType<CharacterScript>().Length > 1)
        {
            Destroy(this.gameObject);
        } 
        else
        {
            DontDestroyOnLoad(this.gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        healthText.text = "Health: " + health;
    }

    // Update is called once per frame
    void Update()
    {
        //These functions add velocity to the character using arrow keys.
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            m_Rigidbody.velocity = transform.forward * m_Speed;

        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            m_Rigidbody.velocity = -transform.forward * m_Speed;

        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            m_Rigidbody.velocity = transform.right * m_Speed;

        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            m_Rigidbody.velocity = -transform.right * m_Speed;

        }

        //Functions below move character with WASD movement
        if (Input.GetKey(KeyCode.W))
        {
            //Another method of moving forward
            //this.transform.localPosition = (new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, this.transform.localPosition.z + m_Speed);
            //Current method of moving forward
            transform.Translate(0, 0, 1.0f);

        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.Translate(0, 0, -1.0f);

        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.Translate(-1.0f, 0, 0);

        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.Translate(1.0f, 0, 0);

        }
        //Move character up and down with E and Q.
        if (Input.GetKey(KeyCode.Q))
        {
            transform.Translate(0, -1.0f, 0);

        }
        if (Input.GetKey(KeyCode.E))
        {
            transform.Translate(0, 1.0f, 0);

        }

        transform.Rotate(new Vector3(0, 1, 0) * Input.GetAxis("Mouse X")
            * Time.deltaTime * rotationRate, Space.World);

        //Increases character speed on mouse button down.
        if (Input.GetMouseButtonDown(0))
        {
            m_Speed += 1.0f;

        }
        //Decreases character speed on second mouse button press.
        if (Input.GetMouseButtonDown(1))
        {
            m_Speed -= 1.0f;

        }
        //Calls the shoot function when the space bar is pressed.
        if (Input.GetKeyDown(KeyCode.Space))
        {
            shoot();
        }

    }

    public float getSpeed()
    {
        return m_Speed;
    }

    public void setSpeed(float newSpeed)
    {
        m_Speed = newSpeed;
    }

    //This adds am to this character's health.
    public void changeHealth(int am)
    {
        health += am;
        healthText.text = "Health: " + health + " Money: " + money + "Plasma: "+plasma;
    }

    //This adds am to this character's money.
    public void changeMoney(int am)
    {
        money += am;
        healthText.text = "Health: " + health + "Money: " + money + "Plasma: " + plasma;
    }

    //This function gets a character's money
    public int getMoney()
    {
        return money;
    }

    //This function adds an item to inventory and adds its image to the inventory screen.
    public void addToInventory(Item item)
    {
        GameObject new_item = Instantiate(item.item_image) as GameObject;
        Item new_item2 = Instantiate(item) as Item;
        new_item2.item_image = new_item;
        inventory.Add(new_item2);
        new_item.GetComponent<InventoryButtonScript>().cs = this;
        new_item.transform.SetParent(inventory_display.transform, false);
        item.player = this;
    }

    //This function returns whether the inventory contains this item.
    public bool containsItem(Item item)
    {
        foreach (Item inventory_item in inventory)
        {
            //Debug.Log(item.name);
            //Debug.Log(inventory_item.name);
            if (inventory_item.name == item.name)
            {
                return true;
            }
        }
        return false;
        //return inventory.Contains(item);
    }

    public bool containsItem(string item_name)
    {
        foreach (Item inventory_item in inventory)
        {
            //Debug.Log(item.name);
            //Debug.Log(inventory_item.name);
            if (inventory_item.name == item_name)
            {
                return true;
            }
        }
        return false;
        //return inventory.Contains(item);
    }

    //This function removes the item from inventory and removes its image from the inventory screen 
    public void removeFromInventory(Item item)
    {
        //inventory.Remove(item);
        foreach (Item inventory_item in inventory)
        {
            //Debug.Log(item.name);
            //Debug.Log(inventory_item.name);
            if (inventory_item.name == item.name)
            {
                //GameObject old_item = GameObject.Find(inventory_item.item_image.name);
                Destroy(inventory_item.item_image);
                inventory.Remove(inventory_item);
                return;

            }
        }
    }

    //This function removes the item from inventory and removes its image from the inventory screen 
    public void removeFromInventory(string item_name)
    {
        foreach (Item inventory_item in inventory)
        {
            if (inventory_item.name == item_name)
            {
                //GameObject old_item = GameObject.Find(inventory_item.item_image.name);
                Destroy(inventory_item.item_image);
                inventory.Remove(inventory_item);
                return;

            }
        }
    }

    private bool nameEquals(Item item)
    {
        return item.name == temp_string;
    }


    //Adds item to equipped list.
    public void equip(Item item)
    {
        equiped.Add(item);
    }
    //This function shoots a bullet from the character's gun gameobject (an empty gameobject placed in front of the ship).
    private void shoot()
    {
        if (plasma > 0)
        {
            GameObject temp_bullet = Instantiate(bullet) as GameObject;
            //Sets the bullet to gun's position.
            temp_bullet.transform.position = gun.transform.position;
            //Sets the rotation of the bullet to that of the ship
            temp_bullet.transform.rotation = this.transform.rotation;
            plasma -= 1;
            changeMoney(0);
        } else
        {
            if (containsItem("Plasma"))
            {
                plasma += 50;
                changeMoney(0);
                removeFromInventory("Plasma");
            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "EnemyBullet")
        {
            changeHealth(-1);
            Instantiate(hitParticleEffect, collision.transform.position, Quaternion.identity);
            Destroy(collision.gameObject);
            if (health < 0)
            {
                FindObjectOfType<LevelManager>().LoadGameOverScreen();
            }
        }

        if (collision.gameObject.tag == "PlayerBullet")
        {
            Destroy(collision.gameObject);
        }
    }


}
