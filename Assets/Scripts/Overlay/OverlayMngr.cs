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
    public GameObject TreeText;
    public GameObject Textbox2;
    public GameObject Textbox3;

    private bool hasShownEnemyText = false;

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
        TreeText.GetComponent<TMP_Text>().text = playerInventory.WoodAmount.ToString();
    }

    public void Attack()
    {
        fpImage.GetComponent<Animator>().SetTrigger("Attack");
    }

    public void SetText(int number)
    {
        if(number == 3 && !hasShownEnemyText)
        {
            //text.textIndex = 3;
            //text.changeTextIndex = true;
            //hasShownEnemyText = true;
        }
    }

    public void DisplayText(string text)
    {
        displayText.GetComponent<TMP_Text>().text = text;
    }

    public void FaintIn(float seconds)
    {
        fpImage.GetComponent<Animator>().SetTrigger("Dead");
        Invoke("ShowFaintedScreen", seconds);
    }

    private void ShowFaintedScreen()
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
