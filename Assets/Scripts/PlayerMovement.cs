using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed = 5f; // Normal walking speed
    public float sprintSpeed = 10f; // Sprinting speed
    public float jumpForce = 7f; // Jump force
    public float gravity = 10f; // Gravity multiplier
    public float health = 7f; 

    public float maxStamina = 100f; // Maximum stamina
    public float stamina; // Current stamina
    public float staminaDrainRate = 20f; // Stamina drain per second while sprinting
    public float staminaRegenRate = 15f; // Stamina regeneration per second

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        stamina = maxStamina; // Set stamina to full at start
    }

    void Update()
    {
        // Check if the player is on the ground
        isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Small downward force to keep grounded
        }

        // Get input
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        // Check if the player is moving
        bool isMoving = moveX != 0 || moveZ != 0;

        // Sprinting logic: only allow sprinting if moving & stamina > 0
        bool isSprinting = Input.GetKey(KeyCode.LeftShift) && isMoving && stamina > 0;
        float currentSpeed = isSprinting ? sprintSpeed : walkSpeed;

        // Move the player
        Vector3 move = transform.right * moveX + transform.forward * moveZ;
        controller.Move(move * currentSpeed * Time.deltaTime);

        // Stamina management
        if (isSprinting)
        {
            stamina -= staminaDrainRate * Time.deltaTime;
            stamina = Mathf.Clamp(stamina, 0, maxStamina); // Prevent stamina from going below 0
        }
        else if (isMoving && stamina < maxStamina) // Regenerate only while walking
        {
            stamina += staminaRegenRate * Time.deltaTime;
            stamina = Mathf.Clamp(stamina, 0, maxStamina); // Prevent stamina from going above max
        }

        // Jump
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpForce * 2f * gravity);
        }

        // Apply gravity
        velocity.y -= gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }
}
