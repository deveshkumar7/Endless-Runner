using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class Pause : MonoBehaviour
{
    public Canvas pauseCanvas;

    private bool isPaused = false;

    void Start()
    {
        // Disable the pause canvas on start
        if (pauseCanvas != null)
        {
            pauseCanvas.gameObject.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            TogglePause();
        }
    }

    void TogglePause()
    {
        isPaused = !isPaused;

        if (isPaused)
        {
            Time.timeScale = 0f; // Pausing the game
            // Activate the pause canvas
            if (pauseCanvas != null)
            {
                pauseCanvas.gameObject.SetActive(true);
            }
            Debug.Log("Game Paused");
        }
        else
        {
            Time.timeScale = 1f; // Resuming the game
            // Disable the pause canvas
            if (pauseCanvas != null)
            {
                pauseCanvas.gameObject.SetActive(false);
            }
            Debug.Log("Game Resumed");
        }
    }
}
