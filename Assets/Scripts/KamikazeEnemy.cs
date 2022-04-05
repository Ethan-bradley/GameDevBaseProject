using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeEnemy : MonoBehaviour
{
    //Current target of this enemy AI
    public GameObject target;
    public GameScript gs;

    //Rigidbody component associated with this AI, helps with physics related things.
    [SerializeField] Rigidbody m_Rigidbody;

    [SerializeField] int health;
    [SerializeField] float m_Speed;

    // This needs to be set before the start of the game or else it will do 0 damage
    // Serialized so it can be adjusted as needed through Unity
    [SerializeField] int damage;


    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(chase());

    }

    // Update is called once per frame
    void Update()
    {
      
    }

    //This IEnumerator runs once every second and causes the enemy ship to look at the player's ship, move towards it, and shoot.
    private IEnumerator chase()
    {
        bool chaseDown = true;
        while (true)
        {

            if (!chaseDown && distanceFromPlayer() > 405)
            {
                explode();
            }

            
            if (chaseDown)
            {
                chaseDown = distanceFromPlayer() > 400;
                transform.LookAt(target.transform);
            }
            m_Rigidbody.velocity = transform.forward * m_Speed;

            yield return new WaitForSeconds(0.005f);
        }
    }

    // Calculates the distance the enemy is from the player
    private float distanceFromPlayer()
    {
        Vector3 targetPos = target.transform.position;
        Vector3 enemyPos = this.gameObject.transform.position;
        return Mathf.Sqrt(Mathf.Pow(enemyPos.x - targetPos.x, 2) + Mathf.Pow(enemyPos.y - targetPos.y, 2) + Mathf.Pow(enemyPos.z - targetPos.z, 2));
    }


    //This adds am to this character's health. If health is less than 0 destroy this object.
    public void changeHealth(int am)
    {
        health += am;
        if (health < 0)
        {
            gs.changeShips(-1);
            explode();
        }
    }

    // Deals damage to the targeted object (aka Player). Object must have a CharacterScript
    private void dealDamage()
    {
        CharacterScript playerScript = target.GetComponent<CharacterScript>();
        playerScript.changeHealth(damage);
    }

    // Run extra animations for a more lively explosion (NOT IMPLEMENTED FULLY)
    private void explode()
    {
        Destroy(this.gameObject);
    }

    // This function runs on colliding with another object with a collider
    private void OnTriggerEnter(Collider other)
    {
        bool isBullet = false;
        // Sets the isBullet bool value to true if the enemy ship collides with a bullet
        // Also checks for collision with the player
        if ((isBullet = other.gameObject.tag == "Bullet") || other.gameObject.tag == "Player")
        {
            // If isBullet is true, then you must destroy the bullet object
            // This is needed so that the player object isn't destroy upon collision with the kamikaze
            if (isBullet)
            {
                Destroy(other.gameObject);
            }
            else
            {
                dealDamage();
            }

            // add the extra -1 so that the health drops below 0
            changeHealth(-health - 1);
        }
    }

}
