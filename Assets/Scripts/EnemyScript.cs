using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyScript : MonoBehaviour
{
    //Current target of this enemy AI
    public GameObject target;
    public GameObject bullet;
    [SerializeField] int health;
    [SerializeField] float m_Speed;
    //Rigidbody component associated with this AI, helps with physics related things.
    [SerializeField] Rigidbody m_Rigidbody;
    public GameObject gun;
    public GameScript gs;
    public Item plasma;
    // Start is called before the first frame update
    void Start()
    {
        gs = FindObjectOfType<GameScript>();
        target = FindObjectOfType<CharacterScript>().gameObject;
        StartCoroutine(run());   
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    //This IEnumerator runs once every second and causes the enemy ship to look at the player's ship, move towards it, and shoot.
    private IEnumerator run()
    {
        while (true)
        {
            transform.LookAt(target.transform);
            m_Rigidbody.velocity = transform.forward * m_Speed;
            shoot();
            yield return new WaitForSeconds(1.0f);
        }
    }

    //Shoot function for enemy ship.
    //This function shoots a bullet from the enemy's gun gameobject (an empty gameobject placed in front of the ship).
    private void shoot()
    {
        GameObject temp_bullet = Instantiate(bullet) as GameObject;
        //Sets the bullet to gun's position.
        temp_bullet.transform.position = gun.transform.position;
        //bullet.transform.position = new Vector3(this.transform.localPosition.x, this.transform.localPosition.y, this.transform.localPosition.z - 80);
        temp_bullet.GetComponent<BulletScript>().target = target;
    }

    //This adds am to this character's health. If health is less than 0 destroy this object.
    public void changeHealth(int am)
    {
        health += am;
        if (health < 0)
        {
            gs.changeShips(-1);
            CharacterScript playerScript = target.GetComponent<CharacterScript>();
            playerScript.addToInventory(plasma);
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
