using UnityEngine;

public class OverlayMngr : MonoBehaviour
{
    //OverlayMngr ska ha koll på allt i Overlay prefaben
    public GameObject player;
    public GameObject health;
    public GameObject stamina;
    [SerializeField] private Sprite[] images;


    void Start()
    {

    }

    void Update()
    {
        PlayerMovement playerData = player.transform.Find("Capsule").GetComponent<PlayerMovement>();
        HealthBar healthData = health.GetComponent<HealthBar>();
        healthData.SetHealth((int)playerData.health);

        StaminaBar staminaData = stamina.GetComponent<StaminaBar>();
        staminaData.SetStamina((int)playerData.stamina);
    }
}
