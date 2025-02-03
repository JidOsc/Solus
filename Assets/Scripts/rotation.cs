using UnityEngine;

public class rotation_objets : MonoBehaviour
{

    public GameObject player;

    private void Start()
    {
        player.GetComponent<Transform>();
    }

    void Update()
    {
        transform.LookAt(player.GetComponent<Transform>());
    }
}
