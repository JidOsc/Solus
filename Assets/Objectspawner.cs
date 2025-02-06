using System.Collections.Generic;
using UnityEngine;

public class Objectspawner : MonoBehaviour
{
    public List<GameObject> stones = new List<GameObject>();
    public List<GameObject> ores = new List<GameObject>();

    void Start()
    {              
        for (int i = 0; i < 200; i++)
        {
            int stone_num = Random.Range(0, 4);
            float x = Random.Range(-200, 200);
            float z = Random.Range(-200, 200);
//            Terrain.activeTerrain.SampleHeight(transform.position)

            Instantiate(stones[stone_num], new Vector3(x, 15, z), transform.rotation);
        }
    }
}
