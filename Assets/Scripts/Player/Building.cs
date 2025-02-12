using Unity.Mathematics;
using UnityEngine;

public class Building : MonoBehaviour
{
    public GameObject stationPrefab;
    public GameObject player;
    public GameObject level;

    public OverlayMngr overlay;

    public Camera cameran;

    public bool menu = false;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void BuildMenu()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            overlay.Textbox3.SetActive(true);
            overlay.Textbox2.SetActive(false);
            menu = true;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
           overlay.Textbox2.SetActive(true);
           overlay.Textbox3.SetActive(false);
            menu = false;
        }
    }

    public void Build()
    {
            if (Physics.Raycast(player.transform.position, cameran.transform.forward, out RaycastHit hit, 20))
            {
                var build = Instantiate(stationPrefab, new Vector3(hit.point.x, hit.point.y + 2.5f, hit.point.z), transform.rotation);
                build.transform.parent = level.transform;
            }
    }

}
