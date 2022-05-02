using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class BuildingScript : MonoBehaviour
{
    public int health;
    public int cost;
    public GameObject trade_display;
    public GameObject main_trade_display;
    public Button trade;
    [SerializeField] Item[] offers;
    [SerializeField] int[] prices;
    public CharacterScript cs;
    private bool tradeDisplayEnabled = false;

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
        Debug.Log("Clicked");

        if (tradeDisplayEnabled)
        {
            removeDisplay();
        }
        else
        {
            createDisplay();
        }
        //health -= 1;
    }

    private void createDisplay()
    {
        tradeDisplayEnabled = true;
        main_trade_display.SetActive(true);
        trade_display.SetActive(true);
        Button new_button = Instantiate(trade);
        new_button.transform.SetParent(trade_display.transform, false);
        new_button.GetComponentInChildren<Text>().text = "Exit Trade Station";
        new_button.GetComponent<Button>().onClick.AddListener(delegate { removeDisplay(); });

        int i = 0;
        foreach(Item item in offers) {
            //Create sell button for item
            int price = prices[i];
            new_button = Instantiate(trade);
            new_button.transform.SetParent(trade_display.transform, false);
            new_button.GetComponentInChildren<Text>().text  = "Sell " + item.name + " $"+prices[i];
            new_button.GetComponent<Button>().onClick.AddListener(delegate { sell(item, price); });
            //Create Buy Button for item
            new_button = Instantiate(trade);
            new_button.transform.SetParent(trade_display.transform, false);
            new_button.GetComponentInChildren<Text>().text = "Buy " + item.name + " $" + prices[i];
            new_button.GetComponent<Button>().onClick.AddListener(delegate { buy(item, price); });
            i++;
        }
   
    }

    private void removeDisplay()
    {
        tradeDisplayEnabled = false;
        foreach (Transform g in trade_display.GetComponentInChildren<Transform>())
        {
            Destroy(g.gameObject);
        }
        main_trade_display.SetActive(false);
        trade_display.SetActive(false);
    }

    //Sells a particular item to the character at a certain price if the character has that item.
    void sell(Item item, int price)
    {
        if (cs.containsItem(item))
        {
            cs.changeMoney(price);
            cs.removeFromInventory(item);
        }
    }
    //Buys a particular item at a certain price if they have enough money for it.
    void buy(Item item, int price)
    {
        if ((cs.getMoney() - price) >= 0)
        {
            cs.addToInventory(item);
            cs.changeMoney(-price);
        }
    }
}
