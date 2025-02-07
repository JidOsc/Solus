using UnityEngine;
using System.Collections.Generic;

public class PlayerInteract : MonoBehaviour
{
    public List<GameObject> interactableObjects = new List<GameObject>();
    public List<GameObject> objectsToRemove = new List<GameObject>();

    bool pickedThisFrame = false;

    void Update()
    {
        pickedThisFrame = false;

        foreach (GameObject obj in interactableObjects)
        {
            if (obj.tag == "Station")
            {
                if (Input.GetKeyDown(KeyCode.Tab))
                {
                    StationScript station = obj.GetComponent<StationScript>();
                    PlayerMain player = GetComponent<PlayerMain>();

                    if(player.ore >= station.CurrentCost())
                    {
                        station.Repair();
                    }
                }
            }

            else if (obj.tag == "Ore")
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

            else if (obj.tag == "DroppedOre")
            {
                //if player picks up ore
                if (!pickedThisFrame && Input.GetMouseButtonDown(0))
                {
                    DroppedOreScript ore = obj.GetComponent<DroppedOreScript>();

                    pickedThisFrame = true;
                    gameObject.GetComponent<PlayerMain>().AddOre(ore.quantity);
                    objectsToRemove.Add(obj.gameObject);
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
            interactableObjects.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.isTrigger)
        {
            interactableObjects.Remove(other.gameObject);
        }
    }
}
