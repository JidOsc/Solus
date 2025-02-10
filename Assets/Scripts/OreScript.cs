using UnityEngine;

public class OreScript : MonoBehaviour
{
    public GameObject droppedOrePrefab;

    short timesToMine = 5;
    int orePerMine = 1;


    void DropOre(int quantity)
    {   
        Vector2 pos = Random.insideUnitCircle;
        DroppedOreScript new_ore = Instantiate(droppedOrePrefab, transform.position + new Vector3(pos.x, 0, pos.y) * 2, Quaternion.identity, transform.parent).GetComponent<DroppedOreScript>();
        new_ore.quantity = quantity;
    }

    public bool Mine()
    {
        timesToMine--;
        DropOre(orePerMine);

        if(timesToMine <= 0)
        {
            return true;
        }
        return false;
    }
}
