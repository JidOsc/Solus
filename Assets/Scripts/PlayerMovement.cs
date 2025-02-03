using UnityEngine;
using UnityEngine.UI; // Import UI library

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f;
    public float sprintSpeed = 10f;
    public float jumpForce = 7f;
    public float gravity = 10f;
    public float health = 7f; 

    public float maxStamina = 100f;
    private float stamina;
    public float staminaDrainRate = 20f;
    public float staminaRegenRate = 15f;

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

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * 2f * gravity);
        }

        velocity.y -= gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
