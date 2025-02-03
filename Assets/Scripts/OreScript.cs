using UnityEngine;

public class OreScript : MonoBehaviour
{
    public GameObject droppedOrePrefab;

    float oreLeft = 1.0f;

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
        Instantiate(droppedOrePrefab, transform.position + new Vector3(pos.x, 0, pos.y) * 2, Quaternion.identity, transform);
    }

    public void Mine()
    {
        oreLeft -= 0.2f;
        DropOre();

        if(oreLeft < 0.2f)
        {
            //Destroy(gameObject);
        }
    }
}
