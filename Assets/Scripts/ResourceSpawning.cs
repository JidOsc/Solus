using NUnit.Framework;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = Unity.Mathematics.Random;

public class ResourceSpawning : MonoBehaviour
{

    public List<GameObject> stone_prefabs = new List<GameObject>();

    public List<GameObject> ore_prefabs = new List<GameObject>();

   

    public int num;

    Vector3Int pos = new Vector3Int(1, 14, 1);

    void Start()
    {
        for (int i = 0; i < 1; i++)
        {
            Random randy = new Random((uint)UnityEngine.Random.Range(1,4));

            num = randy.NextInt();

            Instantiate(stone_prefabs[0], pos, transform.rotation, transform.parent);
        }
    }
}
