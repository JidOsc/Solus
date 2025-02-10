using System.Collections.Generic;
using UnityEngine;

public class Objectspawner : MonoBehaviour
{
    public GameObject level;

    public GameObject enemy;

    public List<GameObject> stones = new List<GameObject>();
    public List<GameObject> ores = new List<GameObject>();

    void Start()
    {              
        for (int i = 0; i < 10000; i++)
        {
            int stone_num = Random.Range(0, 4);
            int x = Random.Range(-900, 900);
            int z = Random.Range(-900, 900);
            float rotY = Random.Range(-100, 50);
            //float rotZ = Random.Range(0, 360);
            Vector3 pos = new Vector3(x, 0, z);
            //int hight = (int)Terrain.activeTerrain.terrainData.GetHeight(x, z) + 8;
            pos.y = Terrain.activeTerrain.SampleHeight(pos) + Terrain.activeTerrain.GetPosition().y + 3.5f;


            var spawning = Instantiate(stones[stone_num], pos, Quaternion.Euler(0, rotY, 0));
            spawning.transform.parent = level.transform;
        }

        for (int i = 0; i < 50; i++)
        {
            int x = Random.Range(-500, 500);
            int z = Random.Range(-500, 500);
            //float rotZ = Random.Range(0, 360);
            Vector3 pos = new Vector3(x, 0, z);
            //int hight = (int)Terrain.activeTerrain.terrainData.GetHeight(x, z) + 8;
            pos.y = Terrain.activeTerrain.SampleHeight(pos) + Terrain.activeTerrain.GetPosition().y + 0.25f;


            var spawning = Instantiate(ores[0], pos, transform.rotation);
            spawning.transform.parent = level.transform;
        }

        for (int i = 0; i < 200; i++)
        {
            int x = Random.Range(-1000, 1000);
            int z = Random.Range(-1000, 1000);
            //float rotZ = Random.Range(0, 360);
            Vector3 pos = new Vector3(x, 0, z);
            //int hight = (int)Terrain.activeTerrain.terrainData.GetHeight(x, z) + 8;
            pos.y = Terrain.activeTerrain.SampleHeight(pos) + Terrain.activeTerrain.GetPosition().y + 0.25f;


            var spawning = Instantiate(enemy, pos, transform.rotation);
            spawning.transform.parent = level.transform;
        }
    }
}
