using System.Collections.Generic;
using UnityEngine;

public class Objectspawner : MonoBehaviour
{
    public GameObject level;

    public GameObject enemy;

    public List<GameObject> stones = new List<GameObject>();
    public List<GameObject> ores = new List<GameObject>();
    public List<GameObject> jelly = new List<GameObject>();

    public GameObject TreePrefab;

    void Start()
    {              
        for (int i = 0; i < 3500; i++)
        {
            int stone_num = Random.Range(0, 4);
            int x = Random.Range(-500, 500);
            int z = Random.Range(-500, 500);
            float rotY = Random.Range(-100, 50);
            
            Vector3 pos = new Vector3(x, 0, z);
            
            pos.y = Terrain.activeTerrain.SampleHeight(pos) + Terrain.activeTerrain.GetPosition().y + 3.5f;


            var spawning = Instantiate(stones[stone_num], pos, Quaternion.Euler(0, rotY, 0));
            spawning.transform.parent = level.transform;
        }

        for (int i = 0; i < 50; i++)
        {
            int x = Random.Range(-500, 500);
            int z = Random.Range(-500, 500);
            
            Vector3 pos = new Vector3(x, 0, z);
            
            pos.y = Terrain.activeTerrain.SampleHeight(pos) + Terrain.activeTerrain.GetPosition().y + 1;


            var spawning = Instantiate(ores[0], pos, transform.rotation);
            spawning.transform.parent = level.transform;
        }

        for (int i = 0; i < 100; i++)
        {
            int x = Random.Range(-500, 500);
            int z = Random.Range(-500, 500);
            
            Vector3 pos = new Vector3(x, 0, z);
            
            pos.y = Terrain.activeTerrain.SampleHeight(pos) + Terrain.activeTerrain.GetPosition().y + 2f;


            var spawning = Instantiate(enemy, pos, transform.rotation);
            spawning.transform.parent = level.transform;
        }

        for (int i = 0; i < 300; i++)
        {
            int x = Random.Range(-500, 500);
            int z = Random.Range(-500, 500);

            Vector3 pos = new Vector3(x, 0, z);

            pos.y = Terrain.activeTerrain.SampleHeight(pos) + Terrain.activeTerrain.GetPosition().y + 1.5f;


            var spawning = Instantiate(TreePrefab, pos, transform.rotation);
            spawning.transform.parent = level.transform;
        }
        for (int i = 0; i < 100; i++)
        {
            int jelly_num = Random.Range(0, 1);
            int x = Random.Range(-500, 500);
            int z = Random.Range(-500, 500);

            Vector3 pos = new Vector3(x, 0, z);

            pos.y = 11.25f;


            var spawning = Instantiate(jelly[jelly_num], pos, transform.rotation);
            spawning.transform.parent = level.transform;
        }
    }
}
