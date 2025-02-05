using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RotationObject : MonoBehaviour
{

    private Vector3 player_pos = new Vector3(0, 0, 0);

    public GameObject player;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Update()
    {
        foreach (Transform tsfm in transform)
        {
            GameObject obj = tsfm.gameObject;

            if (obj.tag == "Ore" || obj.tag == "Enemies" || obj.tag == "DroppedOre" || obj.tag == "RadioTower")
            {
                player_pos = player.transform.position;
                player_pos.y = obj.transform.position.y;

                obj.transform.LookAt(player_pos);
            }
        }
    }
}
