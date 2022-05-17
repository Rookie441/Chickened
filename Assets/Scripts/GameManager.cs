using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject pauseScreen;
    public GameObject gameOverScreen;
    public bool paused;
    private PlayerController playerControllerScript;
    public static int currentLevel;

    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }
    void Update()
    {
        //Check if the user has pressed the P key
        if (Input.GetKeyDown(KeyCode.P))
        {
            ChangePaused();
        }
        //Check if player is dead
        if (playerControllerScript.gameOver)
        {
            gameOverScreen.SetActive(true);
            paused = true; //disable pausing when game is over
        }
    }

    void ChangePaused()
    {
        if (!paused)
        {
            paused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0; // pause all physics calculations
        }
        else
        {
            paused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;

        }
    }

    public void ReturnToMenu()
    {
        paused = false;
        pauseScreen.SetActive(false);
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void IncreaseLevel()
    {
        currentLevel++;
    }
}
