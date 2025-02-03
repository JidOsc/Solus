using UnityEngine;

public class OreScript : MonoBehaviour
{
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

    }

    public void Mine()
    {
        oreLeft -= 0.2f;

        if(oreLeft < 0.2f)
        {
            Destroy(gameObject);
        }
    }
}
