using Unity.Mathematics;
using UnityEngine;

public class rotation_objets : MonoBehaviour
{

    public GameObject player;

    private Vector3 player_pos = new Vector3(0, 0, 0);

    private Quaternion rotation; 

    private void Start()
    {

    }

    void Update()
    {
        player_pos = player.transform.position;
        player_pos.y = transform.position.y;

        transform.LookAt(player_pos);
    }
}
