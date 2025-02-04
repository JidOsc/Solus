using UnityEngine;
using System.Collections.Generic;

public class PlayerInteract : MonoBehaviour
{
    List<GameObject> interactableObjects = new List<GameObject>();
    List<GameObject> objectsToRemove = new List<GameObject>();

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
                    //if player interacts with the building
                }
            }

            if (obj.tag == "Ore")
            {
                //if player is mining
                if (Input.GetMouseButtonDown(1))
                {
                    OreScript ore = obj.GetComponent<OreScript>();
                    if (ore.Mine())
                    {
                        objectsToRemove.Add(obj);
                    }
                    Debug.Log("Högg!");
                }
            }

            if (obj.tag == "DroppedOre")
            {
                //if player picks up ore
                if (!pickedThisFrame && Input.GetMouseButtonDown(0))
                {
                    pickedThisFrame = true;
                    

                    GetComponent<PlayerMain>().AddOre();
                    objectsToRemove.Add(obj);
                }
            }
        }

        for(int i = 0; i < objectsToRemove.Count; i++)
        {
            interactableObjects.Remove(objectsToRemove[i]);
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
