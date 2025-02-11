using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;


public class OverlayMngr : MonoBehaviour
{
    //OverlayMngr ska ha koll på allt i Overlay prefaben
    public GameObject player;

    public GameObject health;
    public GameObject damagedImage;
    public GameObject displayText;
    public GameObject fpImage;

    public GameObject INTERFACE;
    public GameObject FAINTED;

    private int current_health = 0;


    public GameObject stamina;
    [SerializeField] private Sprite[] images;
    public GameObject OreText;
    public GameObject StoneText;

    void Start()
    {

    }

    private void Update()
    {
        PlayerMain playerMovement = player.GetComponent<PlayerMain>();
        PlayerInteract playerInventory = player.GetComponent<PlayerInteract>();

        HealthBar healthData = health.GetComponent<HealthBar>();

        IEnumerator coroutine = ShowDamage(0.5f);
        if (playerMovement.health < current_health)
        {
            StartCoroutine(coroutine);
        }

        healthData.SetHealth((int)playerMovement.health);
        current_health = (int)playerMovement.health;

        StaminaBar staminaData = stamina.GetComponent<StaminaBar>();
        staminaData.SetStamina((int)playerMovement.stamina);

        OreText.GetComponent<TMP_Text>().text = playerInventory.oreAmount.ToString();
        StoneText.GetComponent<TMP_Text>().text = playerInventory.stoneAmount.ToString();
    }

    public void Attack()
    {
        fpImage.GetComponent<Animator>().Play("attack");
    }

    public void DisplayText(string text)
    {
        displayText.GetComponent<TMP_Text>().text = text;
    }

    public void ShowFaintedScreen()
    {
        Debug.Log("dod");
        FAINTED.SetActive(true);
        INTERFACE.SetActive(false);
    }

    public void Respawn()
    {
        FAINTED.SetActive(false);
        INTERFACE.SetActive(true);
        player.GetComponent<PlayerMain>().Reset();
    }

    public void ToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    private IEnumerator ShowDamage(float hideDelay)
    {
        damagedImage.SetActive(true);
        yield return new WaitForSeconds(hideDelay);
        damagedImage.SetActive(false);
    }
}
