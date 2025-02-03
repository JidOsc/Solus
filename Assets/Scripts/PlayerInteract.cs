using UnityEngine;
using System.Collections.Generic;

public class PlayerInteract : MonoBehaviour
{
    List<GameObject> interactableObjects = new List<GameObject>();

    void Update()
    {
        foreach(GameObject obj in interactableObjects)
        {
            if (obj.tag == "Ore")
            {
                //if player is mining
                if (Input.GetMouseButtonDown(0))
                {
                    OreScript ore = obj.GetComponent<OreScript>();
                    ore.Mine();
                    Debug.Log("Högg!");
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        interactableObjects.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        interactableObjects.Remove(other.gameObject);
    }
}
