using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMain : MonoBehaviour
{

    [SerializeField] AudioClip sandClip;
    [SerializeField] AudioClip waterClip;
    public AudioClip sand;
    public AudioClip vatten;
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    public float jumpForce = 7f;
    public float gravity = 10f;
    public float health;
    public float maxHealth = 10f;

    public float backflipDuration = 1f;
    public Camera playerCamera;
    public float normalFOV = 60f;
    public float sprintFOV = 75f;
    public float fovSpeed = 5f;

    public float maxStamina = 100f;
    public float stamina;
    public float staminaDrainRate = 20f;
    public float staminaRegenRate = 15f;

    public OverlayMngr overlay;
    public Enemy enemy;
    public Slider staminaBar;

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private bool onWater = false;
    private bool isMoving = false;
    private AudioSource audioSource;



    private float currentBackflipTime = 1f;
    private bool isBackflipping = false;
    private float backflipCooldown = 1f; // 1 second cooldown
    private float backflipCooldownTimer = 0f; // Timer for tracking the cooldown

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

    public float ore = 0;
    public int damage = 20;
    public float attackRange = 4f;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        stamina = maxStamina;
        health = maxHealth;

        if (staminaBar == null)
        {
            staminaBar = GameObject.Find("StaminaBar").GetComponent<Slider>();
        }

        staminaBar.maxValue = maxStamina;
        staminaBar.value = stamina;

        audioSource = gameObject.GetComponent<AudioSource>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.B) && backflipCooldownTimer <= 0f)
        {
            StartBackflip();
        }

        if (isBackflipping)
        {
            PerformBackflip();
        }

        if (backflipCooldownTimer > 0f)
        {
            backflipCooldownTimer -= Time.deltaTime;
        }

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

        if (isSprinting)
        {
            stamina -= staminaDrainRate * Time.deltaTime;
        }
        else
        {
            stamina += staminaRegenRate * Time.deltaTime;
        }

        stamina = Mathf.Clamp(stamina, 0, maxStamina);
        staminaBar.value = stamina;

        if (Input.GetButton("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * 2f * gravity);
        }

        velocity.y -= gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        isMoving = moveX != 0 || moveZ != 0;

        /*
        if (isMoving && !audioSource.isPlaying && !onWater)
        {
            audioSource.Play();
        }
        else if (!isMoving || onWater)
        {
            audioSource.Stop();
        }
        */
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            overlay.ShowFaintedScreen();
        }
    }

    public void Reset()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        transform.position = new Vector3(0, 0, 0);
        GetComponent<PlayerInteract>().oreAmount = 0;
        health = maxHealth;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "triggerhappy")
        {
            onWater = true;
            audioSource.Stop();
            audioSource.PlayOneShot(vatten, 0.75f);
        }
        else if (other.gameObject.name == "sandhappy")
        {
            onWater = false;
            audioSource.Stop();
            audioSource.PlayOneShot(sand, 0.75f);
            
        }
    }

    public void OnTriggerExit(Collider other)
    {
         if (other.gameObject.name == "triggerhappy")
        {
            onWater = false;
            audioSource.Stop();
                audioSource.PlayOneShot(sand, 0.75f);
            
        }
        if (other.gameObject.name == "sandhappy")
        {
            onWater = true;
            audioSource.Stop();
                audioSource.PlayOneShot(vatten, 0.75f);
            

        }
    }
}