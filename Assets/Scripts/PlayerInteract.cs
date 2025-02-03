using UnityEngine;
using System.Collections.Generic;

public class PlayerInteract : MonoBehaviour
{
    List<GameObject> interactableObjects = new List<GameObject>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach(GameObject obj in interactableObjects)
        {
            if (obj.tag == "Ore")
            {
                //if player is mining
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
