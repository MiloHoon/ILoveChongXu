using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private int currentHealth = 3;

    //[HideInInspector]
    //public bool healthMax;

    public Image GameOverBG;
    public Text gameOverTxt;

    // ask chongxu for what add [SerializeField], why not [HideInInspector]
    [SerializeField]
    [Space(10)]
    public Text goldTxt, silverTxt, bronzeTxt;
    public List<Image> LIST_HEART;
    public GameObject[] GoldCoin, SilverCoin, BronzeCoin;

    [HideInInspector]
    public int goldCount, silverCount, bronzeCount;
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

    public Text escText;
    public void PlayerWin(bool win, int Pausing)
    {
        Time.timeScale = 0;
        if(Pausing == 0)
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
                SceneManager.LoadScene("GameScene");
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PlayerWin(true, 1);
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