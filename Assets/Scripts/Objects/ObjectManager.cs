using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class RotationObject : MonoBehaviour
{
    private List<string> facingObjects = new List<string>() { "Ore", "Enemies", "DroppedOre", "Station", "DroppedStone", "Tree", "TreeDrop", "Jelly" };

    public Vector3 player_pos = new Vector3(0, 0, 0);

    public GameObject lighty;

    public GameObject player;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        lighty = GameObject.FindGameObjectWithTag("Lighty");
    }

    public void Update()
    {
        foreach (Transform tsfm in transform)
        {
            GameObject obj = tsfm.gameObject;

            if (facingObjects.Contains(obj.tag))
            {
                if(obj.tag != "Station")
                {
                    if (Vector2.Distance(new Vector2(player.transform.position.x, player.transform.position.z), new Vector2(obj.transform.position.x, obj.transform.position.z)) > 150)
                    {
                        obj.SetActive(false);
                    }
                    else
                    {
                        obj.SetActive(true);
                    }
                }
                //objektet kollar på spelaren
                player_pos = player.transform.position;
                player_pos.y = obj.transform.position.y;
                obj.transform.LookAt(player_pos);

            }
           /* if (Vector3.Distance(player.transform.position, lighty.transform.position) > 40)
            {
                player_pos = player.transform.position;

                player_pos.y = lighty.transform.position.y;

                lighty.SetActive(true);

                lighty.transform.LookAt(player_pos);
            }

            else if (Vector3.Distance(player_pos, lighty.transform.position) < 20)
            {
                lighty.SetActive(false);
            }*/
        }
    }
}
