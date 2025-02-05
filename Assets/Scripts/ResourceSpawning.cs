using NUnit.Framework;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class ResourceSpawning : MonoBehaviour
{

    public List<GameObject> stone_prefabs = new List<GameObject>();

    public List<GameObject> ore_prefabs = new List<GameObject>();

    public Random randy = new Random();

    Vector3Int pos = new Vector3Int(1, 14, 1);

    void Start()
    {
        int num; 

        for (int i = 0; i < 1; i++)
        {

            num = randy.NextInt(0,3);

            //Instantiate(stone_prefabs[num], pos);
        }
    }
}
