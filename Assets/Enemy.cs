using UnityEngine;

public class Enemy : MonoBehaviour
{

    public float speed = 2f;
    public int health = 100;
    public int damage = 10;
    public float obstacleRange = 5f;

    private bool _alive;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _alive = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (_alive)
        {
            transform.Translate(0, 0, speed * Time.deltaTime);
        }

        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.SphereCast(ray, 0.75f, out hit))
        {
            if (hit.distance < obstacleRange)
            {
                float angel = Random.Range(-110, 110);
                transform.Rotate(0, angel, 0);
            }
        }
    }

    public void SetAlive(bool alive)
    {
        _alive = alive;
    }

    public void ReactHoHit()
    {
        Enemy behavior = GetComponent<Enemy>();
        if (behavior != null)
        {
            behavior.SetAlive(false);
        }
        //StartCoroutine(Die());?????????????????????????
    }
}