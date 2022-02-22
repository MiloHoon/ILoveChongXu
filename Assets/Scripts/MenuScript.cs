using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }

    public void StartPlay()
    {
        SceneManager.LoadScene("GameScene");
    }
    public void Instruction()
    {
        SceneManager.LoadScene("InstructionScene");
    }
    public void AppQuit()
    {
        UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
    public void InstructionQuit()
    {
        SceneManager.LoadScene("MainMenuScene");
    }
}
