using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour
{
    public GameObject player;
    public float speed = 2f;
    public int Maxhealth = 100;
    public int currentHealth;
    public int damage = 10;
    public float obstacleRange = 5f;
    private bool _alive;
    private bool isDealingDamage;
    public float followDistance = 10f;

    void Start()
    {
        _alive = true;
        currentHealth = Maxhealth;
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
            }

            if (distanceToPlayer <= 10 && !isDealingDamage)
            {
                StartCoroutine(DealDamage(2, 5));
            }
        }
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
        }
    }

    void FollowPlayer()
    {
        if (Vector3.Distance(transform.position, player.transform.position) > followDistance)
        {
            GetComponent<Animator>().SetBool("walking", false);
            return;
        }

        GetComponent<Animator>().SetBool("walking", true);
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
