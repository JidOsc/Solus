using UnityEngine;
using TMPro; 


public class OverlayMngr : MonoBehaviour
{
    //OverlayMngr ska ha koll på allt i Overlay prefaben
    public GameObject player;
    public GameObject health;
    public GameObject stamina;
    [SerializeField] private Sprite[] images;
    public GameObject OreText; 


    void Start()
    {

    }

    void Update()
    {
        PlayerMain playerData = player.GetComponent<PlayerMain>();

        HealthBar healthData = health.GetComponent<HealthBar>();
        healthData.SetHealth((int)playerData.health);

        StaminaBar staminaData = stamina.GetComponent<StaminaBar>();
        staminaData.SetStamina((int)playerData.stamina);

        OreText.GetComponent<TMP_Text>().text = playerData.ore.ToString();
        
    }
}
