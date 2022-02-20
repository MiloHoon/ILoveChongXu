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

    [SerializeField]
    [Space(10)]
    public Text goldTxt, silverTxt, bronzeTxt;
    public List<Image> LIST_HEART;
    [HideInInspector]
    public int goldCount, silverCount, bronzeCount;

    //    [HideInInspector]
    //    public GameObject[] coinsOnScene;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        currentHealth = 3;
        // Hide The Win/Lose HUD On Start
        //        winHUD.SetActive(false);
        //        loseHUD.SetActive(false);

        // Get The Variable From PlayerPref To Display On HealthText
        // currentHealth = PlayerPrefs.GetInt("PlayerCurrentLives");

        // Get The Variable From PlayerPref To Display On NailText
        // currentCoins = PlayerPrefs.GetInt("PlayerCurrentCoins");
    }

    // Update is called once per frame
    void Update()
    {
       // Health();
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
        for (int i = 0; i < LIST_HEART.Count; i++)
        {
            if (currentHealth == 3)
                return;
            else if (currentHealth == 2)
                LIST_HEART[2].gameObject.SetActive(false);
            else if (currentHealth == 1)
                LIST_HEART[1].gameObject.SetActive(false);
            else
                LIST_HEART[0].gameObject.SetActive(false);
        }
        // currentHealth -= 1;
        // PlayerPrefs.SetInt("PlayerCurrentLives", currentHealth);
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