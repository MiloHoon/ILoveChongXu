using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector] 
    public int currentHealth, currentCoins;

    [HideInInspector]
    public bool healthMax;

    public Text healthText;
    public Text coinsText;
    //    public GameObject winHUD;
    //    public GameObject loseHUD;

//    [HideInInspector]
//    public GameObject[] coinsOnScene;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        // Hide The Win/Lose HUD On Start
//        winHUD.SetActive(false);
//        loseHUD.SetActive(false);

        // Get The Variable From PlayerPref To Display On HealthText
        currentHealth = PlayerPrefs.GetInt("PlayerCurrentLives");

        // Get The Variable From PlayerPref To Display On NailText
        currentCoins = PlayerPrefs.GetInt("PlayerCurrentCoins");
    }

    // Update is called once per frame
    void Update()
    {
        Health();
//        Nails();
    }

    private void Health()
    {
        healthText.text = "Health: " + currentHealth;

        //Check If Player's Health Is Full
        if(currentHealth == 3)
        {
            healthMax = true;
        }
        //If Player's Health Below The Max Health, He/She Can Gain A Single Health Point
        else if(currentHealth <= 2)
        {
            healthMax = false;
        }
    }

    public void AddHealth()
    {
        if (healthMax == false)
        {
            currentHealth += 1;
            PlayerPrefs.SetInt("PlayerCurrentLives", currentHealth);
        }
    }

    public void MinusHealth()
    {
        currentHealth -= 1;
        PlayerPrefs.SetInt("PlayerCurrentLives", currentHealth);
    }

    //private void Coins()
    //{
    //    coinsText.text = "Coins: " + currentCoins;
    //    coinsOnScene = GameObject.FindGameObjectsWithTag("Coins");
    //}

    //public void AddCoins()
    //{
    //    currentCoins += 1;
    //    PlayerPrefs.SetInt("PlayerCurrentCoins", currentCoins);
    //}

    /*public void WinScene()
    {
        winHUD.SetActive(true);
    }

    public void LoseScene()
    {
        loseHUD.SetActive(true);
    }*/
}