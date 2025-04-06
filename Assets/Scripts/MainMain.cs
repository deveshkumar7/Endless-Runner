using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class MainMain : MonoBehaviour
{
    public Image fadeImage; // Reference to the Image component for fading
    public float fadeDuration = 1.0f; // Duration of the fade effect in seconds

    void Start()
    {
        // Start with a faded-out screen
        fadeImage.color = new Color(0, 0, 0, 1);
        // Start the fade-in effect
        StartCoroutine(FadeIn());
    }

    public void PlayGame()
    {
        // Start the fade-out effect and load the next scene after fade-out completes
        StartCoroutine(FadeOutAndLoadScene("ForestWorld"));
    }

    public void QuitGame()
    {
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void Options()
    {
        Debug.Log("worldloaded");
        SceneManager.LoadScene("options");

    }

    IEnumerator FadeIn()
    {
        // Fade in by decreasing the alpha value over time
        fadeImage.CrossFadeAlpha(0, fadeDuration, false);
        // Wait for the fade-in duration
        yield return new WaitForSeconds(fadeDuration);
    }

    IEnumerator FadeOutAndLoadScene(string sceneName)
    {
        // Fade out by increasing the alpha value over time
        fadeImage.CrossFadeAlpha(1, fadeDuration, false);
        // Wait for the fade-out duration
        yield return new WaitForSeconds(fadeDuration);
        // Load the next scene
        SceneManager.LoadScene(sceneName);
    }
}
