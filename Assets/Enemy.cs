using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform player;
    public float speed = 2f;

    void Update()
    {
        if (player != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
        }
    }
}