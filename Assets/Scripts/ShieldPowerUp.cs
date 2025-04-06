using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ShieldPowerUp : MonoBehaviour
{
    public Image Timer;
    public float maxTime = 5.5f;
    private bool isShielded = false;
    private int playerLayer;
    private int obstacleLayer;
    public GameObject ShieldEffect;
    public GameObject holder;

    void Start()
    {
        playerLayer = LayerMask.NameToLayer("Player");
        obstacleLayer = LayerMask.NameToLayer("Obstacle");
        ShieldEffect.SetActive(false);
        holder.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Shield") && !isShielded)
        {
            ActivateShield(gameObject);
            Destroy(other.gameObject);
            holder.SetActive(true);
        }
    }

    void ActivateShield(GameObject player)
    {
        isShielded = true;
        Debug.Log("Shield activated");

        // Disable collisions with obstacles
        Physics.IgnoreLayerCollision(playerLayer, obstacleLayer, true);

        // Start a coroutine to deactivate the shield after duration
        StartCoroutine(DeactivateShield());

        ShieldEffect.SetActive(true);

        // Start the timer
        StartCoroutine(UpdateTimer());
    }

    IEnumerator DeactivateShield()
    {
        yield return new WaitForSeconds(maxTime);
        Debug.Log("Shield deactivated");

        // Re-enable collisions with obstacles
        Physics.IgnoreLayerCollision(playerLayer, obstacleLayer, false);

        isShielded = false;
        ShieldEffect.SetActive(false);
        holder.SetActive(false);
    }

    IEnumerator UpdateTimer()
    {
        float timer = maxTime;
        while (timer > 0)
        {
            Timer.fillAmount = timer / maxTime;
            yield return new WaitForSeconds(0.1f);
            timer -= 0.1f;
        }

        // Ensure the timer reaches 0
        Timer.fillAmount = 0f;
    }
}
