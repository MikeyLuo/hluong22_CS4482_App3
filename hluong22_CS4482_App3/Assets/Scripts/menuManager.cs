using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.IO;
using UnityEngine.UI;

public class menuManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void play()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("GameScene");
    }

    public void leaderboard()
    {
        SceneManager.LoadScene("leaderboard");
    }

    public void exit()
    {
        Application.Quit();
    }
}
