using Unity.Mathematics;
using UnityEngine;

public class Building : MonoBehaviour
{

    public PlayerLook playerLook;

    public GameObject stationPrefab;
    public GameObject level;

    public void Start()
    {
        stationPrefab = GameObject.FindGameObjectWithTag("Station");
    }

    public void Build()
    {
        Vector3 mouse = Input.mousePosition;

        Ray casepoint = Camera.main.ScreenPointToRay(mouse);
        RaycastHit hit;

        if(Physics.Raycast(casepoint, out hit, 10))
        {
            if (Input.GetKeyDown(KeyCode.B))
            {
                var build = Instantiate(stationPrefab, new Vector3(hit.point.x,hit.point.y, hit.point.z), transform.rotation);
                build.transform.parent = level.transform;
            }
        }
    }

}
