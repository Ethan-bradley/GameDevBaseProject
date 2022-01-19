using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public string name;
    public GameObject item_image;
    public GameObject item;
    public CharacterScript player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Adds to the player's inventory when this object is clicked.
    public void OnMouseDown()
    {
        player.addToInventory(this);
    }
}
