using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryButtonScript : MonoBehaviour
{
    public CharacterScript cs;
    public string item_name;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void onClick()
    {
        cs.removeFromInventory(item_name);
    }
}
