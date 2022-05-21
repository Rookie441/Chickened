using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject pauseScreen;
    public GameObject gameOverScreen;
    public GameObject congratulationsScreen;

    public bool paused;
    public bool isGameOver;
    public bool isGameComplete;

    void Update()
    {
        //Check if the user has pressed the ESC key (Pause/Unpause functionality)
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
                Unpause();
            else
                Pause();
        }

        //Check if player is dead
        if (isGameOver)
        {
            gameOverScreen.SetActive(true);
            paused = true; // Disable pausing when game is over
        }

        // If player is alive and finishes all levels
        else if (isGameComplete)
        {
            // To-do: Sound effects/cutscene for game completion
            paused = true;
            congratulationsScreen.SetActive(true);
        }
    }

    private void Pause()
    {
        paused = true;
        pauseScreen.SetActive(true);
        Time.timeScale = 0; // Pause all physics calculations
    }

    private void Unpause()
    {
        paused = false;
        pauseScreen.SetActive(false);
        Time.timeScale = 1; // Resume all physics calculations
    }

    // Pause & GameOver Canvas: Menu and Retry On Click() function
    public void ReturnToMenu()
    {
        // Unpause and load menu scene
        Unpause();
        SceneManager.LoadScene(0);
    }

    // Pause & GameOver Canvas: Menu and Retry On Click() function
    public void Retry()
    {
        // Unpause and reload current scene
        Unpause();
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
    }
}
