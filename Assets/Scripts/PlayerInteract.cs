using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class PlayerInteract : MonoBehaviour
{
    public int damage = 5;

    public int oreAmount = 0;
    public int stoneAmount = 0;

    public OverlayMngr overlay;

    public List<GameObject> interactableObjects = new List<GameObject>();
    List<GameObject> objectsToRemove = new List<GameObject>();

    bool pickedThisFrame = false;

    void Update()
    {
        pickedThisFrame = false;
        overlay.DisplayText("");

        foreach (GameObject obj in interactableObjects)
        {
            if (obj.tag == "Enemies")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Enemy enemy = obj.GetComponent<Enemy>();

                    if (enemy.TakeDamage(damage)) //true om fienden dog
                    {
                        objectsToRemove.Add(obj);
                    }
                }
            }

            else if (obj.tag == "Station")
            {
                StationScript station = obj.GetComponent<StationScript>();
                overlay.DisplayText("Press TAB to repair station\nRequired Amount: " + station.CurrentCost().ToString());

                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    if(oreAmount >= station.CurrentCost())
                    {
                        station.Repair();
                    }
                }
            }

            else if (obj.tag == "Ore" || obj.tag == "Stone")
            {
                //if player is mining
                if (Input.GetMouseButtonDown(1))
                {
                    OreScript ore = obj.GetComponent<OreScript>();
                    if (ore.Mine())
                    {
                        objectsToRemove.Add(obj.gameObject);
                    }
                    Debug.Log("Högg!");
                }
            }

            else if (obj.tag == "DroppedOre" || obj.tag == "DroppedStone")
            {
                overlay.DisplayText("Press Left Mouse Button to pick up ore");

                //if player picks up ore
                if (!pickedThisFrame && Input.GetMouseButtonDown(0))
                {
                    DroppedOreScript ore = obj.GetComponent<DroppedOreScript>();

                    pickedThisFrame = true;
                    if (obj.tag == "DroppedOre")
                    {
                        oreAmount += ore.quantity;
                        objectsToRemove.Add(obj.gameObject);
                    }
                    else if (obj.tag == "DroppedStone")
                    {
                        stoneAmount += ore.quantity;
                        objectsToRemove.Add(obj.gameObject);
                    }
                }
            }
        }

        for(int i = 0; i < objectsToRemove.Count; i++)
        {
            while (interactableObjects.Contains(objectsToRemove[i]))
            {
                interactableObjects.Remove(objectsToRemove[i]);
            }
            Destroy(objectsToRemove[i]);
        }
        objectsToRemove.Clear();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.isTrigger)
        {
            interactableObjects.Add(other.transform.parent.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.isTrigger)
        {
            interactableObjects.Remove(other.transform.parent.gameObject);
        }
    }
}
