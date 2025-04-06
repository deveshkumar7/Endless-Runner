using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    public float initialSpeed = 6f; // Initial speed of the character
    public float speedIncreasePercentage = 0.02f; // Speed increase percentage per 150 meters
    public float distanceToIncreaseSpeed = 100f; // Distance required to increase speed
    public float distanceTraveled = 0f; // Total distance traveled by the player
    public float currentSpeed; // Current speed of the character

    Rigidbody rb;

    public Image staminaBar;
    public Image staminaBar1;
    public float StaTime = 40f;
    public GameObject stamina_holder;

    public Image Timer;
    public float maxTime = 10f;
    public GameObject holder;

    public bool isGrounded;
    public static bool _magnetActive = false;
    public GameObject magnetEffect;

    private bool jump = false;
    private bool slide = false;
    private bool dead = false;

    public GameObject GameOver;

    public AudioSource SoundEffects;
    public AudioClip Jump, Dead, Slide, Coins;

    float height = 1.8f;
    public Animator animator;

    CapsuleCollider playerCollider;
    float defaultHeight;
    float slideHeight = 0.3f; // Adjust this value according to your game's needs

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        GameOver.SetActive(false);
        SoundEffects = GetComponent<AudioSource>();

        holder.SetActive(false);
        magnetEffect.SetActive(false);

        StartCoroutine(StaminaTimer());

        // Initialize speed using SpeedManager
        PlayerCtrl speedManager = FindObjectOfType<PlayerCtrl>();
        if (speedManager != null)
        {
            rb.velocity = new Vector3(speedManager.initialSpeed, 0, 0);
        }

        // Get the CapsuleCollider component from the player's children
        playerCollider = GetComponentInChildren<CapsuleCollider>();
        if (playerCollider == null)
        {
            Debug.LogError("CapsuleCollider component not found in children of the player object.");
            return;
        }

        defaultHeight = playerCollider.height; // Store default height
    }

    void OnCollisionStay()
    {
        isGrounded = true;
    }

    void Update()
    {
        // Update the distance traveled based on character movement
        distanceTraveled += initialSpeed * Time.deltaTime;

        // Check if the player has traveled enough distance to increase speed
        if (distanceTraveled >= distanceToIncreaseSpeed)
        {
            // Calculate the new speed by increasing the initial speed by the speed increase percentage
            currentSpeed = initialSpeed * (1f + speedIncreasePercentage);

            // Update the initial speed to the new speed
            initialSpeed = currentSpeed;

            // Reset the distance traveled for the next speed increase
            distanceTraveled = 0f;
        }

        float translation = initialSpeed * Time.deltaTime;
        float jumpTranslation = height;

        if (dead == false)
        {
            transform.Translate(0, 0, translation);
        }
        else if (dead == true)
        {
            GameOver.SetActive(true);
            SoundEffects.clip = Dead;
            SoundEffects.Play();
            transform.Translate(0, 0, 0);
            animator.SetBool("isDead", dead);
            jumpTranslation = 0;
            translation = 0;  
            holder.SetActive(false);
            stamina_holder.SetActive(false);
        }

        // Check for right arrow key press
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector3.right * translation);
        }

        // Check for left arrow key press
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector3.left * translation);
        }

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            jump = true;
        }
        else
        {
            jump = false;
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            slide = true;
        }
        else
        {
            slide = false;
        }

        if (jump == true && isGrounded == true)
        {
            SoundEffects.clip = Jump;
            SoundEffects.Play();
            animator.SetBool("isJump", jump);
            transform.Translate(Vector3.up * jumpTranslation);
            isGrounded = false;
        }
        else if (jump == false)
        {
            animator.SetBool("isJump", jump);
        }

        if (slide == true)
        {
            SoundEffects.clip = Slide;
            SoundEffects.Play();
            animator.SetBool("isSlide", slide);
            playerCollider.height = slideHeight;
            playerCollider.center = new Vector3(playerCollider.center.x, slideHeight / 2f, playerCollider.center.z);
            StartCoroutine(ColliderTime());
        }
        else if (slide == false)
        {
            animator.SetBool("isSlide", slide);
            //playerCollider.height = defaultHeight;
            //playerCollider.center = new Vector3(playerCollider.center.x, defaultHeight / 2f, playerCollider.center.z);
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Obstacle"))
        {
            dead = true;
        }
        else if (other.gameObject.CompareTag("Magnet"))
        {
            Magnet._magnetActive = true;
            SoundEffects.clip = Coins;
            SoundEffects.Play();
            Destroy(other.gameObject);
            magnetEffect.SetActive(true);
            holder.SetActive(true);
            StartCoroutine(magnetTime());
            StartCoroutine(UpdateTimer());
        }
        else if (other.gameObject.CompareTag("Stamina"))
        {
            RefillStamina();
            Destroy(other.gameObject);
            // You might want to add some effects or sound here to indicate the refill
        }
    }

    IEnumerator magnetTime()
    {
        yield return new WaitForSeconds(10f);
        Magnet._magnetActive = false;
        magnetEffect.SetActive(false);
        holder.SetActive(false);
    }

    IEnumerator ColliderTime()
    {
        yield return new WaitForSeconds(1f);
        playerCollider.height = defaultHeight;
        playerCollider.center = new Vector3(playerCollider.center.x, defaultHeight / 2f, playerCollider.center.z);

    }
    IEnumerator UpdateTimer()
    {
        float timer = maxTime;
        while (timer > 0)
        {
            Timer.fillAmount = timer / maxTime;
            yield return new WaitForSeconds(0.008f);
            timer -= 0.008f;
        }

        // Ensure the timer reaches 0
        Timer.fillAmount = 0f;
    }
    IEnumerator StaminaTimer()
    {
        float timer = maxTime;
        while (timer > 0)
        {
            staminaBar.fillAmount = timer / maxTime;
            staminaBar1.fillAmount = timer / maxTime;
            yield return new WaitForSeconds(0.003f);
            timer -= 0.003f;
        }

        // Ensure the timer reaches 0s
        staminaBar.fillAmount = 0f;
        staminaBar1.fillAmount = 0f;
        dead = true;
    }
    private void RefillStamina()
    {
        // Reset the timer
        StopAllCoroutines();
        StartCoroutine(StaminaTimer());
       dead = false;
    }
}
