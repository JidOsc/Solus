using UnityEngine;

public class OreScript : MonoBehaviour
{
    public GameObject droppedOrePrefab;

    short timesToMine = 5;
    float orePerMine = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DropOre()
    {
        Vector2 pos = Random.insideUnitCircle;
        Instantiate(droppedOrePrefab, transform.position + new Vector3(pos.x, 0, pos.y) * 2, Quaternion.identity, transform.parent);
    }

    public bool Mine()
    {
        timesToMine--;
        DropOre();

        if(timesToMine <= 0)
        {
            return true;
        }
        return false;
    }
}
