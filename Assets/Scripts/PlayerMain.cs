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

 
}
