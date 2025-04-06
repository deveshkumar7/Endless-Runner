using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMain1 : MonoBehaviour
{
    public Image fadeImage; // Reference to the Image component for fading
    public float fadeDuration = 1.0f; // Duration of the fade effect in seconds

    private void Start()
    {
        // Start with a faded-out screen
        fadeImage.color = new Color(0, 0, 0, 1);
        // Start the fade-in effect
        FadeIn();
    }

    public void PlayGame()
    {
        // Start the fade-out effect and load the next scene after fade-out completes
        StartCoroutine(FadeOutAndLoadScene("Main Menu"));
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

    private void FadeIn()
    {
        // Fade in by decreasing the alpha value over time
        fadeImage.CrossFadeAlpha(0, fadeDuration, false);
    }
}
