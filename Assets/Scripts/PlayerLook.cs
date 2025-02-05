using UnityEngine;


public class PlayerLook : MonoBehaviour
{
    public float sensitivity = 100f; // Mouse sensitivity
    public Transform playerBody; // Reference to the player body (for horizontal rotation)

    private float xRotation = 0f;

    public PlayerMain playerMain; 

    void Start()
    {
        sensitivity = PlayerPrefs.GetFloat("SENSITIVITY"); 
        // Lock the cursor to the center of the screen and hide it
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        // Get mouse movement input
        float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime;

        // Rotate the player left/right
        playerBody.Rotate(Vector3.up * mouseX);

        // Rotate camera up/down (clamp to prevent flipping)
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Restrict vertical rotation
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        if (playerMain.health <= 0)
        {
            Cursor.visible = true; // Show the cursor
            Cursor.lockState = CursorLockMode.None; //
        }
        
    }
}
