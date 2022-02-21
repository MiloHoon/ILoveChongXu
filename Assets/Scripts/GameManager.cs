using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [HideInInspector] 
    public int currentHealth, currentCoins;

    [HideInInspector]
    public bool healthMax;

    //public Text healthText;
    //public Text coinsText;
    public Image GameOverBG;
    public Text gameOverTxt;
    //    public GameObject winHUD;
    //    public GameObject loseHUD;

    [SerializeField]
    [Space(10)]
    public Text goldTxt, silverTxt, bronzeTxt;
    public List<Image> LIST_HEART;
    public GameObject[] GoldCoin, SilverCoin, BronzeCoin;
    [HideInInspector]
    public int goldCount, silverCount, bronzeCount;
    public int goldCollected, silverCollected, bronzeCollected;

    //    [HideInInspector]
    //    public GameObject[] coinsOnScene;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        if (instance == null)
        {
            instance = this;
        }

        currentHealth = 3;

        GameOverBG.gameObject.SetActive(false);
        foreach (Transform child in GameOverBG.gameObject.transform)
            child.gameObject.SetActive(false);
        // Hide The Win/Lose HUD On Start
        //        winHUD.SetActive(false);
        //        loseHUD.SetActive(false);

        // Get The Variable From PlayerPref To Display On HealthText
        // currentHealth = PlayerPrefs.GetInt("PlayerCurrentLives");

        // Get The Variable From PlayerPref To Display On NailText
        // currentCoins = PlayerPrefs.GetInt("PlayerCurrentCoins");

        GoldCoin = GameObject.FindGameObjectsWithTag("Gold");
        SilverCoin = GameObject.FindGameObjectsWithTag("Silver");
        BronzeCoin = GameObject.FindGameObjectsWithTag("Bronze");


        goldCount = GoldCoin.Length;
        silverCount = SilverCoin.Length;
        bronzeCount = BronzeCoin.Length;
    }


    public void PlayerWin(bool win)
    {
        if (win == false)
        {
            GameOverBG.gameObject.SetActive(true);
            foreach (Transform child in GameOverBG.gameObject.transform)
                child.gameObject.SetActive(true);
            gameOverTxt.text = "Game Over";
            gameOverTxt.color = Color.red;
        }
        else
        {
            GameOverBG.gameObject.SetActive(true);
            foreach (Transform child in GameOverBG.gameObject.transform)
                child.gameObject.SetActive(true);
            gameOverTxt.text = "Game Win";
            gameOverTxt.color = Color.yellow;
        }

    }
    // Update is called once per frame
    void Update()
    {
        // Health();
        //        Nails();
        goldTxt.text = goldCollected + "/" + goldCount;
        silverTxt.text = silverCollected + "/" + silverCount;
        bronzeTxt.text = bronzeCollected + "/" + bronzeCount;

        if (Time.timeScale == 0)
            if (Input.GetKeyDown(KeyCode.Return))
                SceneManager.LoadScene("GameScene");

    }

    private void Health()
    {
       // healthText.text = "Health: " + currentHealth;

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
            {
                LIST_HEART[0].gameObject.SetActive(false);
                WinLoseScene(false);
            }
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

    //public void WinScene()
    //{
    //    winHUD.SetActive(true);
    //}

    public void WinLoseScene(bool playerWin)
    {
        // loseHUD.SetActive(true);
        PlayerWin(playerWin);
        Time.timeScale = 0;
    }
}