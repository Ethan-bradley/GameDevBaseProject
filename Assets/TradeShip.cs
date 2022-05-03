using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TradeShip : MonoBehaviour
{
    //Current target of this enemy AI
    public GameObject target;
    public GameObject source;
    [SerializeField] int health;
    [SerializeField] float m_Speed;
    //Rigidbody component associated with this AI, helps with physics related things.
    [SerializeField] Rigidbody m_Rigidbody;
    [SerializeField] GameObject hitParticleEffect;
    public GameScript gs;
    public GameObject player;
    public Item plasma;
    private bool dead = false;
    // Start is called before the first frame update
    void Start()
    {
        gs = FindObjectOfType<GameScript>();
        player = FindObjectOfType<CharacterScript>().gameObject;
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
            if (Vector3.Distance(this.transform.position, target.transform.position) > 100)
            {
                transform.LookAt(target.transform);
                m_Rigidbody.velocity = transform.forward * m_Speed;
                yield return new WaitForSeconds(5.0f);
            } else
            {
                yield return new WaitForSeconds(5.0f);
                GameObject temptarget = target;
                target = source;
                source = temptarget;
            }
        }
    }

    //This adds am to this character's health. If health is less than 0 destroy this object.
    public void changeHealth(int am)
    {
        health += am;
        if (health < 0)
        {
            if (!dead)
            {
                dead = true;
                CharacterScript playerScript = player.GetComponent<CharacterScript>();
                playerScript.addToInventory(plasma);
            }
            Destroy(this.gameObject);
        }
    }

    //This function runs on colliding with another object with a collider
    private void OnCollisionEnter(Collision collision)
    {
        //If that object has the bullet tag then remove 1 health from this ship.
        if (collision.gameObject.tag == "PlayerBullet")
        {
            Instantiate(hitParticleEffect, collision.transform.position, Quaternion.identity);
            changeHealth(-collision.gameObject.GetComponent<BulletScript>().GetDamage());
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Bullet")
        {
            Destroy(collision.gameObject);
        }
    }
}
