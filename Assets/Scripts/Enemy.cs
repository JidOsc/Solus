using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public GameObject player;

    public float speed = 2f;
    public int damage = 10;
    public int maxHealth = 100;
    public int currentHealth;
    public float followDistance = 10f;
    
    private bool _alive;
    private bool isDealingDamage;
    private bool frameCooldown;

    //för animation
    private float currentFrame = 0;
    private const float amountOfFrames = 6;

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
                NextFrame(9 * Time.deltaTime);
            }

            if (distanceToPlayer <= 10 && !isDealingDamage)
            {
                StartCoroutine(DealDamage(2, 5));
            }
        }
    }

    private void NextFrame(float delay)
    {
        if (!frameCooldown)
        {
            //byter frame
            currentFrame++;
            currentFrame %= amountOfFrames;

            //byter frame i material
            MaterialPropertyBlock propertyBlock = new MaterialPropertyBlock();
            propertyBlock.SetVector("_BaseMap_ST", new Vector4(1 / amountOfFrames, 1, currentFrame / amountOfFrames, 0));
            GetComponent<MeshRenderer>().SetPropertyBlock(propertyBlock);

            //startar timer
            frameCooldown = true;
            Invoke("ResetFrameCooldown", delay);
        }
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
        isDealingDamage = true;
        player.GetComponent<PlayerMain>().TakeDamage(damageAmount);

        yield return new WaitForSeconds(delay);
        isDealingDamage = false;
    }
}
