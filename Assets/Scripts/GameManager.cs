using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int currentHealth = 3;
    private bool playerEnd = false;

    public Image GameOverBG;
    public Text gameOverTxt;
    public PlayerController PLAYER_CONTROLLER;

    [SerializeField]
    [Space(10)]
    public Text goldTxt, silverTxt, bronzeTxt;

    [SerializeField]
    [Space(10)]
    public List<Image> LIST_HEART;

    [HideInInspector]
    public GameObject[] GoldCoin, SilverCoin, BronzeCoin;

    [HideInInspector]
    public int goldCount, silverCount, bronzeCount;
    [HideInInspector]
    public int goldCollected, silverCollected, bronzeCollected;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1;

        if (instance == null)
        {
            instance = this;
        }

        GameOverBG.gameObject.SetActive(false);
        escText.gameObject.SetActive(false);
        foreach (Transform child in GameOverBG.gameObject.transform)
            child.gameObject.SetActive(false);

        GoldCoin = GameObject.FindGameObjectsWithTag("Gold");
        SilverCoin = GameObject.FindGameObjectsWithTag("Silver");
        BronzeCoin = GameObject.FindGameObjectsWithTag("Bronze");

        goldCount = GoldCoin.Length;
        silverCount = SilverCoin.Length;
        bronzeCount = BronzeCoin.Length;
    }

    [Space(10)]
    public Text escText;
    public void PlayerWin(bool win, int Pausing)
    {
        Time.timeScale = 0;
        if(Pausing == 0)
        {
            if (win == false)
            {
                playerEnd = true;
                GameOverBG.gameObject.SetActive(true);
                foreach (Transform child in GameOverBG.gameObject.transform)
                    child.gameObject.SetActive(true);

                if (PLAYER_CONTROLLER.audioSource.isPlaying)
                    PLAYER_CONTROLLER.audioSource.Stop();

                PLAYER_CONTROLLER.audioSource.clip = PLAYER_CONTROLLER.loseClip;
                PLAYER_CONTROLLER.audioSource.Play();
                PLAYER_CONTROLLER.audioSource.loop = false;

                gameOverTxt.text = "Game Over";
                gameOverTxt.color = Color.red;
            }
            else
            {
                playerEnd = true;
                GameOverBG.gameObject.SetActive(true);
                foreach (Transform child in GameOverBG.gameObject.transform)
                    child.gameObject.SetActive(true);


                gameOverTxt.text = "Game Win";
                gameOverTxt.color = Color.yellow;
            }
        }
        else
        {
            if (win == false)
            {
                GameOverBG.gameObject.SetActive(true);
                escText.gameObject.SetActive(true);
                foreach (Transform child in GameOverBG.gameObject.transform)
                    child.gameObject.SetActive(true);

                gameOverTxt.text = "Pausing";
                gameOverTxt.color = Color.white;
            }
            else
            {
                GameOverBG.gameObject.SetActive(false);
                escText.gameObject.SetActive(false);
                foreach (Transform child in GameOverBG.gameObject.transform)
                    child.gameObject.SetActive(false);

                Time.timeScale = 1;
            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        goldTxt.text = goldCollected + "/" + goldCount;
        silverTxt.text = silverCollected + "/" + silverCount;
        bronzeTxt.text = bronzeCollected + "/" + bronzeCount;

        if (Time.timeScale == 0)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                SceneManager.LoadScene("MainMenuScene");
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (playerEnd == false)
                    PlayerWin(true, 1);
                else
                    SceneManager.LoadScene("MainMenuScene");
            }
        }
        else if (Time.timeScale == 1)
            if (Input.GetKeyDown(KeyCode.Escape))
                PlayerWin(false, 1);
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
    }

    public void WinLoseScene(bool playerWin)
    {
        PlayerWin(playerWin,0);
    }
}