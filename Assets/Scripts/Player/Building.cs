using Unity.Mathematics;
using UnityEngine;

public class Building : MonoBehaviour
{
    public GameObject stationPrefab;
    public GameObject player;
    public GameObject level;
    public GameObject whereToBuild;
    private GameObject Blob;

    public OverlayMngr overlay;

    public Camera cameran;

    public bool menu = false;

    RaycastHit hit;

    public void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void BuildMenu()
    {
        if (Input.GetKeyDown(KeyCode.X) && menu == false)
        {
            overlay.Textbox3.SetActive(true);
            overlay.Textbox2.SetActive(false);
            menu = true;

            if (Physics.Raycast(player.transform.position, cameran.transform.forward, out hit, 20))
            {
                for (int i = 0; i < 1; i++)
                {
                    Blob = Instantiate(whereToBuild, new Vector3(hit.point.x, hit.point.y, hit.point.z), transform.rotation);
                    Blob.transform.parent = player.transform;
                }
                whereToBuild.transform.position = new Vector3(hit.point.x, hit.point.y, hit.point.z);
            }
        }

        if (Input.GetKeyDown(KeyCode.Z) && menu == true)
        {
           overlay.Textbox2.SetActive(true);
           overlay.Textbox3.SetActive(false);
           menu = false;
           Destroy(Blob);
        }
    }

    public void Build()
    {
            if (Physics.Raycast(player.transform.position, cameran.transform.forward, out hit, 20))
            {
                var build = Instantiate(stationPrefab, new Vector3(hit.point.x, hit.point.y + 2.5f, hit.point.z), transform.rotation);
                build.transform.parent = level.transform;
            }
    }

}
