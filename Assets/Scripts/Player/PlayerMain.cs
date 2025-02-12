using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMain : MonoBehaviour
{
    public AudioClip sand;
    public AudioClip vatten;
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    public float jumpForce = 7f;
    public float gravity = 10f;
    public float health;
    public float maxHealth = 10f;

    public float backflipDuration = 1f;
    private float currentBackflipTime = 1f;
    private bool isBackflipping = false;

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

        if (isMoving && !audioSource.isPlaying && !onWater)
        {
            audioSource.Play();
        }
        else if (!isMoving || onWater)
        {
            audioSource.Stop();
        }
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
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(sand, 0.75f);
            }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "triggerhappy")
        {
            onWater = false;
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(sand, 0.75f);
            }
        }
        if (other.gameObject.name == "triggerhappy")
        {
            onWater = true;
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(vatten, 0.75f);
            }

        }
    }
}