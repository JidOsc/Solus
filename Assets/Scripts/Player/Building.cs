using Unity.Mathematics;
using UnityEngine;

public class Building : MonoBehaviour
{
    public GameObject stationPrefab;
    public GameObject player;
    public GameObject level;
    public GameObject Overlay;

    public Camera cameran;

    public void Start()
    {
        stationPrefab = GameObject.FindGameObjectWithTag("Station");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void BuildMenu()
    {

    }

    public void Build()
    {
        if(Physics.Raycast(player.transform.position, cameran.transform.forward, out RaycastHit hit, 20))
        {
                var build = Instantiate(stationPrefab, new Vector3(hit.point.x,hit.point.y + 2.5f, hit.point.z), transform.rotation);
                build.transform.parent = level.transform;
        }
    }

}
