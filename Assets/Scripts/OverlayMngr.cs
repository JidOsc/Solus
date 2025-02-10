using UnityEngine;
using TMPro;
using System.Collections;


public class OverlayMngr : MonoBehaviour
{
    //OverlayMngr ska ha koll på allt i Overlay prefaben
    public GameObject player;

    public GameObject health;
    public GameObject damagedImage;
    private int current_health = 0;


    public GameObject stamina;
    [SerializeField] private Sprite[] images;
    public GameObject OreText; 


    void Start()
    {

    }

    private void Update()
    {
        PlayerMain playerData = player.GetComponent<PlayerMain>();

        HealthBar healthData = health.GetComponent<HealthBar>();

        IEnumerator coroutine = ShowDamage(0.5f);
        if (playerData.health < current_health)
        {
            StartCoroutine(coroutine);
        }

        healthData.SetHealth((int)playerData.health);
        current_health = (int)playerData.health;

        StaminaBar staminaData = stamina.GetComponent<StaminaBar>();
        staminaData.SetStamina((int)playerData.stamina);

        OreText.GetComponent<TMP_Text>().text = playerData.ore.ToString();
    }

    private IEnumerator ShowDamage(float hideDelay)
    {
        damagedImage.SetActive(true);
        yield return new WaitForSeconds(hideDelay);
        damagedImage.SetActive(false);
    }
}
