using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public GameObject player;

    public AudioClip skadar;
    public AudioSource audioSource;
    public float speed = 2f;
    public int damage = 10;
    public int maxHealth = 100;
    public int currentHealth;

    public int followDistance = 100;
    public int attackDistance = 5;
    
    private bool _alive;
    private bool isDealingDamage;
    private bool frameCooldown;

    //för animation
    private short currentFrame = 0;
    private short currentRow = 0; //0 är walking, 1 är attacking
    private const short walkingFrames = 6;
    private const short deathFrames = 10;
    private const short maxFrames = 12;

    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();

        _alive = true;
        currentHealth = maxHealth;
        isDealingDamage = false;

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    void Update()
    {
        if (player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (distanceToPlayer <= followDistance)
            {
                if (_alive)
                {
                    int textOnlyOnce = 0;

                    if (textOnlyOnce == 0)
                    {
                        Text text = GetComponent<Text>();
                        text.textIndex = 3;
                        text.changeTextIndex = true;

                        textOnlyOnce++;
                    }
                    FollowPlayer();
                    if(distanceToPlayer <= attackDistance && !isDealingDamage)
                    {
                        StartCoroutine(DealDamage(1, 4.0f / 3.0f));
                    }
                }
                NextFrame(1.0f / 9.0f);
            }
        }
    }

    private void NextFrame(float delay)
    {
        if (!frameCooldown)
        {
            //byter frame
            currentFrame++;
            currentFrame %= maxFrames;
            if(!_alive)
            {
                currentRow = 0;
                if(currentFrame == deathFrames)
                {
                    Destroy(gameObject);
                }
            }
            else if (isDealingDamage)
            {
                currentRow = 1;
            }
            else
            {
                currentRow = 2;
                currentFrame %= walkingFrames;
            }

            //byter frame i material
            MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
            propertyBlock.SetVector("_BaseMap_ST", new Vector4(
                1.0f / 12.0f, 
                1.0f / 3.0f, 
                (float)currentFrame / (float)maxFrames, 
                (float)currentRow / 3.0f));
            GetComponent<MeshRenderer>().SetPropertyBlock(propertyBlock);

            //startar timer
            frameCooldown = true;
            Invoke("ResetFrameCooldown", delay);
        }
    }

    private void ResetFrame()
    {
        currentFrame = 0;
    }

    private void ResetFrameCooldown()
    {
        frameCooldown = false;
    }

    public bool TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (_alive && currentHealth <= 0)
        {
            _alive = false;
            ResetFrame();
            return true;
        }
        return false;
    }

    private void FollowPlayer()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > followDistance)
        {
            return;
        }

        Vector3 pos = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        GetComponent<Rigidbody>().MovePosition(pos);
    }

    IEnumerator DealDamage(int damageAmount, float delay)
    {
        ResetFrame();
        isDealingDamage = true;
        player.GetComponent<PlayerMain>().TakeDamage(damageAmount);

        if (!audioSource.isPlaying)
        {
            audioSource.PlayOneShot(skadar, 0.5f);
        }

        yield return new WaitForSeconds(delay);
        isDealingDamage = false;
    }
}
