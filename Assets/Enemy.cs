using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Enemy : MonoBehaviour
{
    public GameObject player;

    public float speed = 2f;
    public int Maxhealth = 100;
    public int currentHealth;
    public int damage = 10;
    public float obstacleRange = 5f;

    private bool _alive;


    void Start()
    {
        _alive = true;
        currentHealth = Maxhealth;

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
            //KeepOnGround();
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
        }
    }

    void FollowPlayer()
    {
        Vector3 pos = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        GetComponent<Rigidbody>().MovePosition(pos);
        Vector3 pos2 = player.transform.position;
        pos2.y = transform.position.y;
        transform.LookAt(pos2);
    }
   /*   void KeepOnGround()
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, Vector3.down, out hit, 1f))
        {
            transform.position = new Vector3(transform.position.x, hit.point.y, transform.position.z);
        }
    }*/

}
