using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class PlayerInteract : MonoBehaviour
{
    public int damage = 5;

    public int oreAmount = 0;
    public int stoneAmount = 0;
    public int WoodAmount = 0;

    public OverlayMngr overlay;

    public List<GameObject> interactableObjects = new List<GameObject>();
    List<GameObject> objectsToRemove = new List<GameObject>();

    public Building building;

    public bool pickedThisFrame = false;
    bool isBuilt = false;

    void Update()
    {
        pickedThisFrame = false;
        overlay.DisplayText("");

        foreach (GameObject obj in interactableObjects)
        {
            if (Input.GetMouseButtonDown(0)) //hugger
            {
                overlay.Attack();

                if (obj.tag == "Enemies")
                {
                    Enemy enemy = obj.GetComponent<Enemy>();
                    if (enemy.TakeDamage(damage)) //true om fienden dött
                    {
                        objectsToRemove.Add(obj);
                    }
                    break;
                }

                else if (obj.tag == "Ore" || obj.tag == "Stone" || obj.tag == "Tree")
                {
                    OreScript ore = obj.GetComponent<OreScript>();
                    if (ore.Mine()) //true om ore dog
                    {
                        objectsToRemove.Add(obj.gameObject);
                    }
                    break;
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
                break;
            }

            else if (obj.tag == "DroppedOre" || obj.tag == "DroppedStone" || obj.tag == "TreeDrop")
            {
                overlay.DisplayText("Press right mouse button to pick up ore");

                //if player picks up ore
                if (!pickedThisFrame && Input.GetMouseButtonDown(1))
                {
                    DroppedOreScript ore = obj.GetComponent<DroppedOreScript>();

                    pickedThisFrame = true;
                    if (obj.tag == "DroppedOre")
                    {
                        oreAmount += ore.quantity;
                        objectsToRemove.Add(obj.gameObject);
                    }
                    if (obj.tag == "DroppedStone")
                    {
                        stoneAmount += ore.quantity;
                        objectsToRemove.Add(obj.gameObject);
                    }
                    if (obj.tag == "TreeDrop")
                    {
                        WoodAmount += ore.quantity;
                        objectsToRemove.Add(obj.gameObject);
                    }
                }
                break;
            }
        }

        for(int i = 0; i < objectsToRemove.Count; i++)
        {
            while (interactableObjects.Contains(objectsToRemove[i]))
            {
                interactableObjects.Remove(objectsToRemove[i]);
            }
            if(objectsToRemove[i].tag != "Enemies")
            {
                Destroy(objectsToRemove[i]);
            }
        }
        objectsToRemove.Clear();
        if (isBuilt == false && building.menu == true && stoneAmount >= 6)
        { 
            if (Input.GetKeyDown(KeyCode.B))
            {
                building.Build();
                isBuilt = true;
                stoneAmount -= 6;
            }
        }
        building.BuildMenu();
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
