using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public GameObject player;

    public float speed = 2f;
    public int damage = 10;
    public int maxHealth = 100;
    public int currentHealth;

    public int followDistance = 100;
    public int attackDistance = 5;
    
    private bool _alive;
    public bool isDealingDamage;
    private bool frameCooldown;

    //för animation
    public short currentFrame = 0;
    public short currentRow = 0; //0 är walking, 1 är attacking
    private const short amountOfWalkingFrames = 6;
    private const short amountOfAttackFrames = 12;

    void Start()
    {
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
        if (_alive && player != null)
        {
            float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);

            if (distanceToPlayer <= followDistance)
            {
                FollowPlayer();
                NextFrame(9);
            }

            if (distanceToPlayer <= attackDistance && !isDealingDamage)
            {
                StartCoroutine(DealDamage(1, 9));
            }
        }
    }

    private void NextFrame(float delay)
    {
        if (!frameCooldown)
        {
            //byter frame
            currentFrame++;
            currentFrame %= amountOfAttackFrames;
            if (isDealingDamage)
            {
                currentRow = 0;
            }
            else
            {
                currentRow = 1;
                currentFrame %= amountOfWalkingFrames;
            }

            //byter frame i material
            MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
            propertyBlock.SetVector("_BaseMap_ST", new Vector4(
                1.0f / 12.0f, 
                1.0f / 2.0f, 
                (float)currentFrame / (float)amountOfAttackFrames, 
                (float)currentRow / 2.0f));
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
        if (currentHealth <= 0)
        {
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

        yield return new WaitForSeconds(delay);
        isDealingDamage = false;
    }
}
