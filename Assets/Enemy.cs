using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public GameObject player;

    public float speed = 2f;
    public int Maxhealth = 200;
    public int currentHealth;
    public int damage = 2;
    public float obstacleRange = 5f;
    private bool _alive;
    private bool isTakingDamage;

    void Start()
    {
        _alive = true;
        currentHealth = Maxhealth;
        isTakingDamage = false;

        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
    }

    void Update()
    {
        if (_alive && player != null)
        {
            FollowPlayer();

            if (Vector3.Distance(transform.position, player.transform.position) <= 2)
            {
                player.GetComponent<PlayerMain>().TakeDamage(5);
            }
        }
    }

    public void TakeDamage(int amount)
    {
        currentHealth -= amount;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        Debug.Log("Enemy died!");
        Destroy(gameObject);
    }

    public void SetAlive(bool alive)
    {
        _alive = alive;
    }

    public void ReactHoHit()
    {
        SetAlive(false);
    }

    void OnTriggerStay(Collider player)
    {
        if (player.tag == "Player")
        {
            FollowPlayer();
            if (!isTakingDamage)
            {
                StartCoroutine(DamageOverTime(10,10));
            }
        }
    }

    void FollowPlayer()
    {
        Vector3 pos = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        GetComponent<Rigidbody>().MovePosition(pos);
    }
    
    IEnumerator DamageOverTime(int damageAmount, float interval)
    {
        isTakingDamage = true;
        while (_alive && player != null && Vector3.Distance(transform.position, player.transform.position) <= 2)
        {
            TakeDamage(damageAmount);
            yield return new WaitForSeconds(interval);
        }
        isTakingDamage = false;
    }
}