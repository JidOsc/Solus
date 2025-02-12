using Unity.Mathematics;
using UnityEngine;

public class Building : MonoBehaviour
{

    public PlayerLook playerLook;

    public void Start()
    {
        
    }

    public void build()
    {
        Vector3 mouse = Input.mousePosition;

        Ray casepoint = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;

        if(Physics.Raycast(casepoint, out hit, 10))
        {
            
        }
    }

}
