using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameScript : MonoBehaviour
{
    public Text shipsLeftText;

    private int shipsLeft;
    private LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Updates enemy ships left display on the shipsLeftText
    public void changeShips(int am)
    {
        shipsLeft += am;
        shipsLeftText.text = "Enemies Left: " + shipsLeft;
        if (shipsLeft <= 0)
        {
            LoadNextLevel();
        }
    }

    //Exits and returns to menu scene.
    public void LoadNextLevel()
    {
        levelManager.LoadNextLevel();
    }
}
