using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PortalTeleport1 : MonoBehaviour
{
    public Image fadeImage; // Reference to the Image component
    public float fadeDuration = 0.7f; // Duration of the fade effect in seconds
    public string PortalTag = "Portal"; // Tag of the portal objects to collide with
    public string SceneName = "DesertWorld";

    private void Start()
    {
        // Start with a faded-out screen
        fadeImage.color = new Color(0, 0, 0, 1);
        // Start the fade-in effect
        FadeIn();
    }

    public void FadeOutAndLoadScene()
    {
        // Start the fade-out effect
        FadeOut();
        // Load the scene after the fade-out completes
        Invoke("LoadScene", fadeDuration);
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(SceneName);
    }

    private void FadeIn()
    {
        // Fade in by decreasing the alpha value over time
        fadeImage.CrossFadeAlpha(0, fadeDuration, false);
    }

    private void FadeOut()
    {
        // Fade out by increasing the alpha value over time
        fadeImage.CrossFadeAlpha(1, fadeDuration, false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has the specified tag
        if (collision.gameObject.CompareTag(PortalTag))
        {
            FadeOutAndLoadScene();
        }
    }
}
