using UnityEngine;
using UnityEngine.UI; // Import UI library
using UnityEngine.SceneManagement;

public class PlayerMain : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    public float jumpForce = 7f;
    public float gravity = 10f;
    public float health = 10f;
    public float ore = 0;

    public float backflipDuration = 1f;  // Duration of the backflip effect
    private float currentBackflipTime = 0f;
    private bool isBackflipping = false;

    public Camera playerCamera; // Assign your main camera in the inspector
    public float normalFOV = 60f;
    public float sprintFOV = 75f;
    public float fovSpeed = 5f;

    public float maxStamina = 100f;
    public float stamina;
    public float staminaDrainRate = 20f;
    public float staminaRegenRate = 15f;

    public Enemy enemy;
    public int damage = 20;
    public float attackRange = 4f;

    public Slider staminaBar; // Reference to the UI Stamina Bar

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    // Cooldown timer for backflip
    private float backflipCooldown = 1f; // 1 second cooldown
    private float backflipCooldownTimer = 0f; // Timer for tracking the cooldown

    void Start()
    {
        controller = GetComponent<CharacterController>();
        stamina = maxStamina;

        // Find the stamina bar UI if not assigned
        if (staminaBar == null)
        {
            staminaBar = GameObject.Find("StaminaBar").GetComponent<Slider>();
        }

        staminaBar.maxValue = maxStamina;
        staminaBar.value = stamina;
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        bool isSprinting = Input.GetKey(KeyCode.LeftShift) && stamina > 0;
        float currentSpeed = isSprinting ? sprintSpeed : walkSpeed;

        float targetFOV = isSprinting ? sprintFOV : normalFOV;
        playerCamera.fieldOfView = Mathf.Lerp(playerCamera.fieldOfView, targetFOV, Time.deltaTime * fovSpeed);

        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * currentSpeed * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            stamina -= staminaDrainRate * Time.deltaTime;
        }
        else
        {
            stamina += staminaRegenRate * Time.deltaTime;
        }

        stamina = Mathf.Clamp(stamina, 0, maxStamina);

        // Update the stamina bar UI
        if (staminaBar != null)
        {
            staminaBar.value = stamina;
        }

        if (Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * 2f * gravity);
        }

        velocity.y -= gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.F)) // Press Space to attack
        {
            Attack();
        }

        // Handle backflip input
        if (Input.GetKeyDown(KeyCode.B) && backflipCooldownTimer <= 0f) // If the player presses B and cooldown is over
        {
            StartBackflip();
        }

        if (isBackflipping)
        {
            PerformBackflip();
        }

        // Handle backflip cooldown timer
        if (backflipCooldownTimer > 0f)
        {
            backflipCooldownTimer -= Time.deltaTime; // Countdown the cooldown timer
        }
    }

    public void AddOre(float quantity)
    {
        ore += quantity;
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            SceneManager.LoadScene(0);
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void DealDamage(Enemy enemy)
    {
        if (enemy != null)
        {
            enemy.TakeDamage(damage); // Call the enemy's TakeDamage method
        }
    }

    void Attack()
    {
        Collider[] hitEnemies = Physics.OverlapSphere(transform.position, attackRange);

        foreach (Collider enemyCollider in hitEnemies)
        {
            if (enemyCollider.CompareTag("Enemies"))
            {
                Enemy enemy = enemyCollider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    DealDamage(enemy); // Deal damage to the enemy in range
                }
            }
        }
    }

    void StartBackflip()
    {
        isBackflipping = true;
        currentBackflipTime = 0f; // Reset timer

        // Start the cooldown timer
        backflipCooldownTimer = backflipCooldown;
    }

    void PerformBackflip()
    {
        // Update the backflip time
        currentBackflipTime += Time.deltaTime;

        // Calculate the backflip angle (this goes from 0 to 360 degrees)
        float backflipAngle = Mathf.Lerp(0f, -360f, currentBackflipTime / backflipDuration);

        // Apply the rotation to the camera (rotate around the x-axis)
        playerCamera.transform.localRotation = Quaternion.Euler(backflipAngle, 0f, 0f);

        // Stop the backflip once the duration is over
        if (currentBackflipTime >= backflipDuration)
        {
            isBackflipping = false;
        }
    }
}

