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

    void Start()
    {
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void Pause()
    {
        paused = true;
        pauseScreen.SetActive(true);
        Time.timeScale = 0; // pause all physics calculations
    }

    private void Unpause()
    {
        paused = false;
        pauseScreen.SetActive(false);
        Time.timeScale = 1; // resume all physics calculations
    }
    public void ReturnToMenu()
    {
        // Unpause and load menu scene
        Unpause();
        SceneManager.LoadScene(0);
    }

    public void Retry()
    {
        // Unpause and reload current scene
        Unpause();
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }

    void Update()
    {
        //Check if the user has pressed the ESC key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Unpause();
            }
            else
            {
                Pause();
            }
        }

        //Check if player is dead
        if (playerControllerScript.gameOver)
        {
            gameOverScreen.SetActive(true);
            paused = true; //disable pausing when game is over
        }
    }
}
