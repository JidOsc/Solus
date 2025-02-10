using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RotationObject : MonoBehaviour
{

    private Vector3 player_pos = new Vector3(0, 0, 0);

    public float area;

    public GameObject lighty;

    public GameObject player;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        lighty = GameObject.FindGameObjectWithTag("Lighty");

        //area = 3.14 *= 200 / 4;
    }

    public void Update()
    {
        foreach (Transform tsfm in transform)
        {
            GameObject obj = tsfm.gameObject;

            if (obj.tag == "Ore" || obj.tag == "Enemies" || obj.tag == "DroppedOre" || obj.tag == "Station")
            {
                player_pos = player.transform.position;
                player_pos.y = obj.transform.position.y;

                obj.transform.LookAt(player_pos);

            }
            if (Vector3.Distance(player.transform.position, lighty.transform.position) > 40)
            {
                player_pos = player.transform.position;

                player_pos.y = lighty.transform.position.y;

                lighty.SetActive(true);

                lighty.transform.LookAt(player_pos);
            }
            else if (Vector3.Distance(player_pos, lighty.transform.position) < 20)
            {
                lighty.SetActive(false);
            }
        }
    }
}
