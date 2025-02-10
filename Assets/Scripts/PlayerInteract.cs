using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class PlayerInteract : MonoBehaviour
{
    public int damage = 5;

    public GameObject input_text;

    List<GameObject> interactableObjects = new List<GameObject>();
    List<GameObject> objectsToRemove = new List<GameObject>();

    bool pickedThisFrame = false;

    void Update()
    {
        pickedThisFrame = false;
        input_text.GetComponent<TMP_Text>().text = "";

        foreach (GameObject obj in interactableObjects)
        {
            if (obj.tag == "Enemies")
            {
                if (Input.GetMouseButtonDown(0))
                {
                    Enemy enemy = obj.GetComponent<Enemy>();

                    enemy.TakeDamage(damage);
                }
            }

            else if (obj.tag == "Station")
            {
                StationScript station = obj.GetComponent<StationScript>();    
                input_text.GetComponent<TMP_Text>().text = "Press TAB to repair station\nRequired Amount: " + station.CurrentCost().ToString();

                if (Input.GetKeyDown(KeyCode.Tab))
                {
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
                input_text.GetComponent<TMP_Text>().text = "Press Left Mouse Button to pick up ore";

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
