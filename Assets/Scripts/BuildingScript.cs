using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingScript : MonoBehaviour
{
    public int health;
    public int cost;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //This function changes the health of the building
    public void changeHealth(int am)
    {
        health += am;
    }

    //Interaction Script, when a mouse clicks on this building subtracts 1 health (runs this function).
    public void OnMouseDown()
    {
        health -= 1;  
    }
}
